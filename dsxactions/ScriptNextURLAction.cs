using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Drawing.Design;
using System.Threading;
using System.Runtime.Serialization;

namespace DataScraper
{
    /// <summary>
    /// Служит для организации цикла загрузки "следующей" страницы
    /// 
    /// Загружает страницу, адрес которой указан в Node.InputData
    /// передает ее на разбор потомкам (в Node.OutputData),
    /// после этого пытается найти (регулярное выражение из Pattern) 
    /// адрес следующей страницы для загрузки. 
    /// Если адрес найден, то страница по этому адресу 
    /// загружается в Node.DataOutput и снова передается на разбор потомкам.
    /// Цикл повоторяется до тех пор, пока находится адрес следующе страницы.
    /// 
    /// Параметр bool EqualEnabled разрешает цикл загрузки страницы 
    /// по одному и тому же адресу
    /// </summary>
    [Serializable]
    public class ScriptNextURLAction:ScriptLoadAction
    {
        // TODO Сделать ограничение переходов

        private const int DEF_MAX_NUMBER = 10;

        public ScriptNextURLAction()
        {
            ImageIndex = 7;
            Label = "Next page action";
            SuppressErrors = true;
            DefaulEncoding = "utf-8";
            MaxNumber = DEF_MAX_NUMBER;
        }

        public ScriptNextURLAction(SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
            ImageIndex = 7;
            LoadProps(Info);
        }

        protected override void InternalExecute()
        {
            int pageNum = MaxNumber;
            bool stop = false;
            string url = InputFlow;
            string old = "";
            BackFlow = String.Empty;
            do
            {
                int tid = Thread.CurrentThread.ManagedThreadId;
                try
                {
                    Console.WriteLine("{0}[{1:000}]->{2}", Label, tid, url);
                    //Log("Action \"{0}\" loads data from: {1}", Label, url);

                    OutputFlow = WrappedRequest(url);
                }
                catch (Exception e)
                {
                    // Если ошибка появилась и мы не должны ее подавлять
                    // то мы поднимаем исключение выше до корневого обработчика
                    if (!SuppressErrors) throw e;
                    OutputFlow = String.Empty;
                    Console.WriteLine("{0}[{1:000}]->{2}", Label, tid, e.Message);
                }

                // поиск следующей страницы происходит ДО! 
                // обработки ее содержимого
                old = url;
                url = FindURL().Replace("&amp;", "&");
                // Console.WriteLine("{0}[{1}] has next: {2}", Label, tid, url);

                ExecuteChild(OutputFlow);

                if(MaxNumber > 0) pageNum--;
                stop = (MaxNumber > 0 && pageNum == 0) || (url == String.Empty) || ((url == old) && EqualEnabled);
            }
            while(!stop);
        }

        private string FindURL()
        {
            // Замена переменных значениями в шаблоне
            string prepared = VarTable.ParseVariables(FormatString);

            MatchCollection mc = Regex.Matches(OutputFlow, Pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            foreach (Match m in mc)
            {
                // Если группы указаны
                int num = m.Groups.Count;
                if (num > 0)
                {
                    // формируем массив строк для явно указанных групп
                    string[] groups = new string[num];
                    for (int i = 0; i < num; i++)
                        groups[i] = m.Groups[i].Value;

                    // форматируем результат
                    return String.Format(prepared, groups);
                }
                else
                {
                    return String.Format(prepared, m.Value);
                }
            }
            return String.Empty;
        }

        protected override void SaveProps(SerializationInfo Info)
        {
            Info.AddValue("encoding", DefaulEncoding);
            Info.AddValue("suppress-errors", SuppressErrors);
            Info.AddValue("pattern", Pattern);
            Info.AddValue("format-string", FormatString);
            Info.AddValue("equal-enabled", EqualEnabled);
            Info.AddValue("max", MaxNumber);
            Info.AddValue("agent", UserAgent);
        }

        protected override void LoadProps(SerializationInfo Info)
        {
            DefaulEncoding = Info.GetString("encoding");
            SuppressErrors = Info.GetBoolean("suppress-errors");
            Pattern = Info.GetString("pattern");
            FormatString = Info.GetString("format-string");
            EqualEnabled = Info.GetBoolean("equal-enabled");
            MaxNumber = Info.GetInt32("max");
            UserAgent = Info.GetString("agent");
        }

        public override void SetAttributes(System.Xml.XmlElement Element)
        {
            Element.SetAttribute("type", ScriptConsts.TYPE_NEXT_PAGE);
            Element.SetAttribute("encoding", DefaulEncoding);
            Element.SetAttribute("pattern", Pattern);
            Element.SetAttribute("format-string", FormatString);
            Element.SetAttribute("equal-enabled", EqualEnabled.ToString());
            Element.SetAttribute("suppress-errors", SuppressErrors.ToString());
            Element.SetAttribute("max", MaxNumber.ToString());
            Element.SetAttribute("agent", UserAgent);
        }

        public override void GetAttributes(System.Xml.XmlElement Element)
        {
            DefaulEncoding = Element.GetAttribute("encoding");
            UserAgent = Element.GetAttribute("agent");
            Pattern = Element.GetAttribute("pattern");
            FormatString = Element.GetAttribute("format-string");
            EqualEnabled = GetPropBool(Element, "equal-enabled", false);
            MaxNumber = GetPropInt(Element, "max", DEF_MAX_NUMBER);
            SuppressErrors = GetPropBool(Element, "suppress-errors", false);
        }

        public bool EqualEnabled { get; set; }

        // [DisplayName("Suppress errors")]
        // public bool SuppressErrors { get; set; }

        [DisplayName("Page limit")]
        public int MaxNumber { get; set; }

        public string Pattern { get; set; }

        // [DisplayName("Encoding")]
        // [TypeConverter(typeof(EncodingTypeConverter))]
        // public string DefaulEncoding { get; set; }

        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string FormatString { get; set; }

        // public string UserAgent { get; set; }

    }
}
