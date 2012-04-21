using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;


namespace DataScraper
{
    class Program
    {
        const string PN_PRC = "processed";
        const string PN_PRX = "proxy";
        const string PN_PRS = "proxies";
        const string PN_ENC = "enc";
        const string PN_CYC = "cyclic";
        const string INSTR = "DSX";

        static string[] Parts = null;
        static string DataFile = String.Empty;
        static string ProxyFile = String.Empty;
        static Encoding Enc = Encoding.UTF8;
        static DateTime StartDT;
        static bool Cyclic = false;

        private static bool breakRunning = false;

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Dictionary<string, string> arguments = new Dictionary<string, string>();

                foreach (string s in args)
                {
                    string[] ss = s.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                    if (ss.Length == 1)
                        arguments.Add(PN_PRC, ss[0]);
                    else if (ss.Length == 2)
                        arguments.Add(ss[0].ToLower(), ss[1]);
                    else
                    {
                        Console.WriteLine("Wrong parameter {0}", ss[0]);
                        ShowUsage();
                        return;
                    }
                }

                ProxyFile = CheckParam(arguments, PN_PRS);
                if(!String.IsNullOrEmpty(ProxyFile))
                    if (!File.Exists(ProxyFile))
                    {
                        Console.WriteLine("Proxy file not exists");
                        ShowUsage();
                        return;
                    }

                try
                {
                    Enc = CheckEncoding(arguments);
                }
                catch (Exception)
                {
                    Console.WriteLine("Encoding not recognize");
                    ShowUsage();
                    return;
                }

                DataFile = CheckParam(arguments, PN_PRC);
                if (String.IsNullOrEmpty(DataFile))
                {
                    Console.WriteLine("Processed file parameter not found");
                    ShowUsage();
                    return;
                }

                if (File.Exists(DataFile))
                {
                    StartDT = DateTime.Now;
                    Console.WriteLine("Started at " + StartDT.ToString("HH:mm:ss"));
                    try
                    {
                        Console.TreatControlCAsInput = true;
                        // Establish an event handler to process key press events.
                        Console.CancelKeyPress += new ConsoleCancelEventHandler(CtrlC);
                        
                        StartProcess();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        ShowExecutingTime();
                        // Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Processed file not exists");
                    ShowUsage();
                    return;
                }

            }
            else
                ShowUsage();
        }

        private static void StartProcess()
        {
            string dir = Path.GetDirectoryName(DataFile);
            if (String.IsNullOrEmpty(dir) == false)
                Directory.SetCurrentDirectory(Path.GetDirectoryName(DataFile));

            // Перед началом обработки делаем резервную копию файла данных
            BackUp(DataFile);

            while (true)
            {
                Console.WriteLine("Loading data file");
                string content = String.Empty;
                using (StreamReader reader = new StreamReader(DataFile, Enc))
                    content = reader.ReadToEnd();

                Console.WriteLine("Parsing instructions in data file");
                Parts = SplitText(content);
                if (Parts.Length == 1) break;

                try
                {
                    for (int i = 0; i < Parts.Length; i++)
                    {
                        // если нечетный индекс, значит - инструкция
                        if (i / 2.0 > i / 2)
                            Parts[i] = ProcessInstruction(Parts[i]);
                        if (Program.breakRunning) break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    Save();
                }

                if (Cyclic)
                    Console.WriteLine("Go to next cycle");
                else
                    break;
            }

            // после окончания работы, сохраняем остаток прокси серверов,
            // чтобы при последующем запуске начать с активного сервера
            SaveProxyRest();
            ShowExecutingTime();
        }

        private static void SaveProxyRest()
        {
            if (ProxyStorage.Proxy == null) return;

            BackUp(ProxyFile);

            using (StreamWriter writer = new StreamWriter(ProxyFile, false))
            {
                while (ProxyStorage.Proxy != null)
                {
                    Uri addr = ProxyStorage.Proxy.Address;
                    writer.WriteLine(String.Format("{0}:{1}", addr.Host, addr.Port));
                    ProxyStorage.ChangeProxy(false);
                }
            }

            Console.WriteLine("Proxy list save to {0}", ProxyFile);
        }

        private static void BackUp(string FileName)
        {
            string ext = Path.GetExtension(FileName);
            string name = Path.GetFileNameWithoutExtension(FileName);
            string backup = String.Empty;
            int i = 0;
            do
            {
                backup = String.Format("{0}-{1}{2}", name, i, ext);
                i++;
            }
            while (File.Exists(backup));

            // string backup = Path.ChangeExtension(FileName, "back");
            // if (File.Exists(backup)) File.Delete(backup);
            File.Copy(FileName, backup);
            Console.WriteLine("Backup file {0} to {1}", FileName, backup);
        }

        private static void Save()
        {
            using (StreamWriter writer = new StreamWriter(DataFile, false, Enc))
                foreach (string part in Parts)
                    writer.Write(part);

            Console.WriteLine("Results saved to {0}", DataFile);
        }

        private static string ProcessInstruction(string Instruction)
        {
            string res = Instruction;
            try
            {
                string[] parts = SplitInstruction(Instruction);
                ScriptAction root = ActionFactory.Load(parts[0], ProxyFile);
                root.InputFlow = parts[1];
                root.Execute();
                string back = root.BackFlow.Trim();
                if (String.IsNullOrEmpty(back) == false) res = back;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return res;
        }

        private static string[] SplitInstruction(string Instruction)
        {
            string pat = String.Format(@"<!--\s*{0}:([^:]+):(.*?)\s*-->", INSTR);
            Regex re = new Regex(pat, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            MatchCollection mc = re.Matches(Instruction);
            string[] parts = new string[2];
            parts[0] = mc[0].Groups[1].Value;
            parts[1] = mc[0].Groups[2].Value;
            return parts;
        }

        private static string[] SplitText(string Text)
        {
            List<string> text_parts = new List<string>();

            string pat = String.Format(@"<!--\s*{0}:([^:]+):(.*?)\s*-->", INSTR);
            Regex re = new Regex(pat, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            MatchCollection mc = re.Matches(Text);
            int num = mc.Count;
            int pos = 0;
            string rest = Text;
            for (int i = 0; i < num; i++)
            {
                Match m = mc[i];
                // Console.WriteLine("Index: {0}, Length: {1}", m.Index, m.Length);
                pos = m.Index - pos;
                string part = rest.Substring(0, pos);

                rest = rest.Remove(0, pos + m.Length);
                pos = m.Index + m.Length;

                // обычный текст не требующий обработки
                text_parts.Add(part);
                // Вставляем инструкцию тоже
                text_parts.Add(m.Value);
            }
            text_parts.Add(rest);
            return text_parts.ToArray();
        }

        private static bool CheckBool(string ParamVal)
        {
            ParamVal = ParamVal.ToUpper();
            string[] yes = new string[] { "Y", "YES", "1" };
            foreach (string s in yes)
                if (ParamVal == s) return true;

            return false;
        }

        private static bool CheckCyclic(Dictionary<string, string> arguments)
        {
            bool res = Cyclic;
            if (arguments.ContainsKey(PN_CYC))
                res = CheckBool(arguments[PN_ENC]);
            return res;
        }

        private static Encoding CheckEncoding(Dictionary<string, string> arguments)
        {
            Encoding res = Encoding.UTF8;
            if (arguments.ContainsKey(PN_ENC))
                res = Encoding.GetEncoding(arguments[PN_ENC]);
            return res;
        }

        private static string CheckParam(Dictionary<string, string> arguments, string ParamName)
        {
            string res = string.Empty;
            if (arguments.ContainsKey(ParamName))
                res = arguments[ParamName];
            return res;
        }

        private static void ShowExecutingTime()
        {
            TimeSpan ts = DateTime.Now.Subtract(StartDT);
            Console.WriteLine("Time: {0} hour {1} min {2} sec", ts.Hours, ts.Minutes, ts.Seconds);
        }

        private static void ShowUsage()
        {
            Console.WriteLine("usage: dsbh.exe [{0}=path] [{1}=windows-1251|utf-8|etc] [{2}=Y|N] processed_file", PN_PRS, PN_ENC, PN_CYC);
        }

        /*
           When you press CTRL+C, the read operation is interrupted and the 
           console cancel event handler, myHandler, is invoked. Upon entry 
           to the event handler, the Cancel property is false, which means 
           the current process will terminate when the event handler terminates. 
           However, the event handler sets the Cancel property to true, which 
           means the process will not terminate and the read operation will resume.
        */
        protected static void CtrlC(object sender, ConsoleCancelEventArgs args)
        {
            Program.breakRunning = true;
            args.Cancel = false;
            Save();
            SaveProxyRest();
            ShowExecutingTime();
        }
    }
}
