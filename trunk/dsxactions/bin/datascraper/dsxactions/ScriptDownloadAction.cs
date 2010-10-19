using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.ComponentModel;
using System.Xml;
using System.Runtime.Serialization;
using System.Threading;
using System.Text.RegularExpressions;

namespace DataScraper
{
    [Serializable]
    public class ScriptDownloadAction : ScriptAction
    {
        private static CookieCollection _cookies = null;

        private readonly object finishedLock = new object();

        public ScriptDownloadAction()
        {
            if (ScriptDownloadAction._cookies == null)
                _cookies = new CookieCollection();
            ImageIndex = 8;
            SuppressErrors = true;
            OriginalName = true;
            Label = "Download action";
        }

        public ScriptDownloadAction(SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
            ImageIndex = 8;
            LoadProps(Info);
        }

        private string ExtractFileNameFromURL(string URL)
        {
            // избавляемся от пути и параметров
            return Regex.Replace(URL, @"^(.*/)([^/\?]+)(.*)$", "$2");
        }

        private string GenerateFileName(string ContentType)
        {
            string[] parts = ContentType.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            string fname = Path.GetRandomFileName();
            //fname = Path.GetFileNameWithoutExtension(fname);
            fname = Path.ChangeExtension(fname, parts[parts.Length - 1].ToLower());
            return fname;
        }

        private string Download(string URL)
        {
            // Замена переменных значениями в пути
            string dir_name = VarTable.ParseVariables(TargetFolder);
            string url_file = ExtractFileNameFromURL(URL);
            string filename = Path.Combine(dir_name, url_file);

            // Если нужно сохранить оригинальное название файла 
            if (OriginalName)
                // такой файл уже существует и его не следует перезаписать? 
                if (File.Exists(filename) && (OverrideFile == false))
                    // Ничего не делаем - возвращаем имя файла
                    return filename;

            // Если такой папки не существует,
            if (!Directory.Exists(dir_name))
                // создаем ее
                Directory.CreateDirectory(dir_name);

            WebRequest req = System.Net.WebRequest.Create(URL);

            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "GET";

            if (req is HttpWebRequest)
                if (Proxy != null)
                    (req as HttpWebRequest).Proxy = Proxy;

            WebResponse resp = req.GetResponse();
            if (resp == null) return String.Empty;

            Stream remoteStream = resp.GetResponseStream();
            if (OriginalName == false)
                filename = Path.Combine(dir_name, GenerateFileName(resp.ContentType));

            Stream localStream = File.Create(filename);

            byte[] buffer = new byte[1024];
            int bytesRead;
            int bytesProcessed = 0;

            do
            {
                bytesRead = remoteStream.Read(buffer, 0, buffer.Length);
                localStream.Write(buffer, 0, bytesRead);
                bytesProcessed += bytesRead;
            } while (bytesRead > 0);

            remoteStream.Close();
            localStream.Close();

            resp.Close();
            return filename;
        }

        protected string WrappedDownload(string URL)
        {
            string filename = String.Empty;
            int i = ScriptConsts.MAX_LOAD_ATTEMPT;

            while (i > 0)
            {
                try
                {
                    i--;
                    Log("url: {0}", URL);
                    filename = Download(URL);
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0}[{1:000}]->{2}", Label, Thread.CurrentThread.ManagedThreadId, e.Message);
                    Log("Action \"{0}\" error: {1}", Label, e.Message);

                    if (i == 0)
                        throw e;
                    else
                    {
                        // if action hav't proxy, then we don't set it in future
                        if (Proxy == null)
                        {
                            Thread.Sleep(ScriptConsts.LOAD_INTERVAL_SEC * 1000);
                        }
                        else
                        {
                            // if new proxy exists, then set it
                            if (ProxyStorage.ChangeProxy()) 
                                Proxy = ProxyStorage.Proxy;
                        }
                    }
                }
            }

            return filename;
        }

        protected override void InternalExecute()
        {
            // Если ссылки нет, то и выполнять нечего
            if (String.IsNullOrEmpty(InputFlow)) return;

            int tid = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("{0}[{1:000}]->{2}", Label, tid, InputFlow);

            try
            {
                // OutputFlow содержит имя файла (без пути)
                OutputFlow = WrappedDownload(InputFlow);
            }
            catch (Exception e)
            {
                // Если ошибка появилась и мы должны ее подавить
                if (SuppressErrors)
                    // то просто возвращаем управление родителю, 
                    // а не передаем его потомкам
                    return;
                else
                    // иначе мы поднимаем исключение до корневого обработчика
                    throw e;
            }

            ExecuteChild(OutputFlow);
        }

        protected override void SaveProps(SerializationInfo Info)
        {
            Info.AddValue("dir", TargetFolder);
            Info.AddValue("suppress-errors", SuppressErrors);
            Info.AddValue("original-name", OriginalName);
            Info.AddValue("override", OverrideFile);
        }

        protected override void LoadProps(SerializationInfo Info)
        {
            TargetFolder = Info.GetString("dir");
            SuppressErrors = Info.GetBoolean("suppress-errors");
            OriginalName = Info.GetBoolean("original-name");
            OverrideFile = Info.GetBoolean("override");
        }

        public override void SetAttributes(XmlElement Element)
        {
            Element.SetAttribute("type", ScriptConsts.TYPE_DOWNLOAD);
            Element.SetAttribute("dir", TargetFolder);
            Element.SetAttribute("suppress-errors", SuppressErrors.ToString());
            Element.SetAttribute("original-name", OriginalName.ToString());
            Element.SetAttribute("override", OverrideFile.ToString());
        }

        public override void GetAttributes(XmlElement Element)
        {
            TargetFolder = Element.GetAttribute("dir");
            SuppressErrors = GetPropBool(Element, "suppress-errors", false);
            OriginalName = GetPropBool(Element, "original-name", false);
            OverrideFile = GetPropBool(Element, "override", false);
        }

        [DisplayName("Suppress errors")]
        public bool SuppressErrors { get; set; }

        [DisplayName("Original name")]
        public bool OriginalName { get; set; }

        [DisplayName("Override file")]
        public bool OverrideFile { get; set; }

        [DisplayName("Target directory")]
        public string TargetFolder { get; set; }

        [System.ComponentModel.Browsable(false)]
        public WebProxy Proxy { get; set; }

    }

}
