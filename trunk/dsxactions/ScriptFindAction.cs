using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections;
using System.Runtime.Serialization;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataScraper
{
    [Serializable]
    public class ScriptFindAction : ScriptAction
    {
        private const int MAX_THREAD_COUNT = 5;
        private string _pattern = String.Empty;
        private string _text = String.Empty;
        private bool _multithreading = false;

        // Временное хранилище данных для результатов работы потомков
        // при их асинхронном (многопоточном) запуске 
        private readonly Object key = null;
        private volatile int ThreadCount = 0;
        private int thread_num = 0;

        public ScriptFindAction()
        {
            ImageIndex = 2;
            UniqueOnly = false;
            Label = "Search action";
            key = new Object();
        }

        public ScriptFindAction(SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
            ImageIndex = 2;
            LoadProps(Info);
        }

        protected override void InternalExecute()
        {
            List<string> found = new List<string>();
            ArrayList uniq_values = new ArrayList();

            string output = String.Empty;

            if (String.IsNullOrEmpty(Pattern)) return;

            // Многопоточная обработка имеет смысл, только если есть потомки
            bool OneThread = (!MultiThreading || Actions.Count == 0);

            // MatchCollection mc = Regex.Matches(InputFlow, Pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            Regex re = new Regex(Pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            MatchCollection mc = re.Matches(InputFlow);
            ThreadCount = mc.Count;
            for (int i = 0; i < ThreadCount; i++)
            {
                Match m = mc[i];

                // Если группы указаны
                int num = m.Groups.Count;
                if (num > 0)
                {
                    // формируем массив строк для явно указанных групп
                    string[] groups = new string[num];
                    for (int gnum = 0; gnum < num; gnum++)
                        groups[gnum] = m.Groups[gnum].Value;

                    // форматируем результат
                    output = String.Format(FormatString, groups);
                }
                else
                {
                    output = String.Format(FormatString, m.Value);
                }

                // output = VarTable.ParseVariables(output);


                // Если включен режим обработки только уникальных значений
                // то нужно их накапливать для проверки
                if (UniqueOnly)
                    // То проверяем не было ли найдено подобной строки ранее
                    if (uniq_values.Contains(output.ToUpper()))
                        // не даем выполняться потомкам, идем на следующий цикл
                        continue;
                    else
                        uniq_values.Add(output.ToUpper());


                // Если нет многопоточной обработки, 
                // то исполнение потомков идет на каждой итерации
                //if (OneThread)
                //ExecuteChild();
                // иначе мы накапливаем результаты, 
                // которые будут переданы в параллельную обработку
                //else
                found.Add(output);
                OutputFlow += output;
            }

            BackFlow = String.Empty;
            if (OneThread)
            {
                // Запускаем потомков в этом же потоке
                foreach (string s in found)
                    ExecuteChild(s);
            }
            // Если включена многопоточность, 
            else
            {
                ExecuteByPacks(found);
                // ExecuteAllThreads(found);
            }
        }

        private void ExecuteByPacks(List<string> Found)
        {
            ThreadPool.SetMaxThreads(5, 5);

            List<Thread> threads = new List<Thread>();
            Hashtable thread_results = new Hashtable();
            ThreadCount = Found.Count;

            thread_num = Found.Count;

            // Запускаем потомков в отдельных потоках
            for (int i = 0; i < Found.Count; i++)
            {
                ThreadData item = new ThreadData();
                item.index = i;
                item.input = Found[i];
                item.clone = (ScriptAction)Clone(true);

                thread_results.Add(i, item);

                ThreadPool.QueueUserWorkItem(new WaitCallback(ExecuteInThread), item);
            }

            string back = String.Empty;

            // Ждем исполнения всех запущенных потоков
            lock (key)
            {
                while (thread_num > 0)
                // while (ThreadCount > 0)
                {
                    Log("Threads in work: {0}", ThreadCount);
                    Monitor.Wait(key);
                }
            }

            // Складываем реультаты выполнения потоков в BackFlow
            lock (thread_results.SyncRoot)
            {
                int n = thread_results.Count;
                for (int i = 0; i < n; i++)
                    back += ((ThreadData)thread_results[i]).back;
            }

            BackFlow = back;
        }

        private void ExecuteAllThreads(List<string> Found)
        {
            List<Thread> threads = new List<Thread>();
            Hashtable thread_results = new Hashtable();

            ThreadCount = Found.Count;
            // Запускаем потомков в отдельных потоках
            for (int i = 0; i < Found.Count; i++)
            {
                ThreadData item = new ThreadData();

                item.index = i;
                item.input = Found[i];
                item.clone = (ScriptAction)Clone(true);

                thread_results.Add(i, item);
                Thread t = new Thread(new ParameterizedThreadStart(ExecuteInThread));
                threads.Add(t);

                // ThreadPool.QueueUserWorkItem(ExecuteInThread, item);
                t.Start(item);
            }

            string back = String.Empty;

            // Ждем исполнения всех запущенных потоков
            lock (key)
            {
                while (ThreadCount > 0)
                {
                    Log("Threads in work: {0}", ThreadCount);
                    Monitor.Wait(key);
                }
            }

            // Складвыаем реультаты выполнения потоков в BackFlow
            lock (thread_results.SyncRoot)
            {
                int n = thread_results.Count;
                for (int i = 0; i < n; i++)
                    back += ((ThreadData)thread_results[i]).back;
            }

            BackFlow = back;
        }

        private void ExecuteInThread(Object ThreadContext)
        {
            ThreadData item = (ThreadData)ThreadContext;
            int i = item.index;
            string src = item.input;
            item.back = String.Empty;

            Log("Started");

            try
            {
                foreach (ScriptAction action in item.clone.Actions)
                {
                    action.InputFlow = src;
                    action.Execute();
                    item.back += action.BackFlow;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Log("Done");
                lock (key)
                {
                    Interlocked.Decrement(ref thread_num);
                    // ThreadCount--;
                    Monitor.Pulse(key);
                }
            }
        }

        protected override void SaveProps(SerializationInfo Info)
        {
            Info.AddValue("pattern", Pattern);
            Info.AddValue("format-string", FormatString);
            Info.AddValue("unique", UniqueOnly);
            Info.AddValue("multithread", MultiThreading);
        }

        protected override void LoadProps(SerializationInfo Info)
        {
            Pattern = Info.GetString("pattern");
            FormatString = Info.GetString("format-string");
            UniqueOnly = Info.GetBoolean("unique");
            try
            {
                MultiThreading = Info.GetBoolean("multithread");
            }
            catch (Exception)
            {
                MultiThreading = false;
            }

        }

        public override void SetAttributes(XmlElement Element)
        {
            Element.SetAttribute("type", ScriptConsts.TYPE_FIND);
            Element.SetAttribute("pattern", Pattern);
            Element.SetAttribute("format-string", FormatString);
            Element.SetAttribute("unique", UniqueOnly.ToString());
            Element.SetAttribute("multithread", MultiThreading.ToString());
        }

        public override void GetAttributes(XmlElement Element)
        {
            Pattern = Element.GetAttribute("pattern");
            FormatString = Element.GetAttribute("format-string");
            bool b = false;
            if (Boolean.TryParse(Element.GetAttribute("unique"), out b))
                UniqueOnly = b;
            else
                UniqueOnly = false;

            if (Boolean.TryParse(Element.GetAttribute("multithread"), out b))
                MultiThreading = b;
            else
                MultiThreading = false;
        }

        public string Pattern
        {
            get { return _pattern; }
            set { _pattern = value; }
        }

        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string FormatString
        {
            get { return _text; }
            set { _text = value; }
        }

        public bool MultiThreading
        {
            get { return _multithreading; }
            set { _multithreading = value; }
        }

        [DisplayName("Unique only")]
        public bool UniqueOnly { get; set; }

    }

    class ThreadData
    {
        internal string input = String.Empty;
        internal string back = String.Empty;
        internal int index = -1;
        internal ScriptAction clone = null;
    }

    class RegexSyncObj
    {
        public object Root = new object();
        private static RegexSyncObj instance = null;
        private static readonly object padlock = new object();

        private RegexSyncObj()
        {
            Root = new Object();
        }

        public static RegexSyncObj Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new RegexSyncObj();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
