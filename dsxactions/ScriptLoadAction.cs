using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.ComponentModel;
using System.Xml;
using System.Threading;
using System.Runtime.Serialization;
using System.Drawing.Design;
using System.Web;

namespace DataScraper
{
    public enum LoadMetod
    {
        [Description("GET")]
        GET,
        [Description("POST")]
        POST
    }

    [Serializable]
    public class ScriptLoadAction : ScriptAction
    {
        private string _encoding = "utf-8";
        private readonly object finishedLock = new object();

        public ScriptLoadAction()
        {
            ImageIndex = 1;
            Label = "Loading action";
            SuppressErrors = true;
        }

        public ScriptLoadAction(SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
            ImageIndex = 1;
            LoadProps(Info);
        }

        private string GetResponseText(WebResponse Response)
        {
            string s = "";
            using (StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.GetEncoding(DefaulEncoding)))
                s = sr.ReadToEnd();

            s = s.Replace("\r", "").Replace("\n", Environment.NewLine);
            s = s.Trim();
            return s;
        }

        private string SimpleRequest(WebRequest Request)
        {
            WebResponse resp = Request.GetResponse();
            return GetResponseText(resp);
        }

        protected string Request(string Url)
        {
            string post = String.Empty;
            if (Method == LoadMetod.POST)
            {
                string[] ss = Url.Split(new char[] { '?' }, StringSplitOptions.RemoveEmptyEntries);
                if (ss.Length > 1)
                {
                    Url = ss[0];
                    post = HttpUtility.UrlEncode(ss[1]);
                }
            }


            Uri uri = new Uri(Url);
            WebRequest req = WebRequest.Create(uri);
            // HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);

            //req.Proxy = new WebProxy("127.0.0.1", 8888); 
            req.ContentType = "application/x-www-form-urlencoded";

            if ((req is HttpWebRequest) == false)
                return SimpleRequest(req);

            HttpWebRequest wreq = (HttpWebRequest)req;
            wreq.AllowAutoRedirect = false;

            if (Proxy != null)
                wreq.Proxy = Proxy;
            
            if (!String.IsNullOrEmpty(UserAgent)) wreq.UserAgent = UserAgent;


            if (Method == LoadMetod.POST)
            {
                wreq.Method = "POST";
                wreq.ContentLength = post.Length;
                using (Stream requestStream = wreq.GetRequestStream())
                using (StreamWriter writer = new StreamWriter(requestStream))
                    writer.Write(post);
            }
            else
            {
                wreq.Method = "GET";
            }

            
            CookieStorage.Charge(wreq);
            HttpWebResponse wresp = (HttpWebResponse)wreq.GetResponse();
            CookieStorage.Accept(wresp);

            if (wresp == null) return null;
            
            while ((wresp.StatusCode == HttpStatusCode.Moved) || (wresp.StatusCode == HttpStatusCode.Found))
            {

                string url2 = wresp.Headers["Location"];
                if (url2.Substring(0, 1) == "/")
                    url2 = String.Format("{0}://{1}{2}", uri.Scheme, uri.Host, url2);
                Uri uri2 = new Uri(url2);

                wreq = (HttpWebRequest)WebRequest.Create(uri2);
                wreq.ContentType = "application/x-www-form-urlencoded";
                wreq.Method = "GET";
                wreq.AllowAutoRedirect = false;

                if (!String.IsNullOrEmpty(UserAgent)) wreq.UserAgent = UserAgent;

                CookieStorage.Charge(wreq);
                wresp = wreq.GetResponse() as HttpWebResponse;
                CookieStorage.Accept(wresp);

                if (wresp == null) return null;
            }

            return GetResponseText(wresp);
        }

        protected string WrappedRequest(string URI)
        {
            string response = String.Empty;
            int i = ScriptConsts.MAX_LOAD_ATTEMPT;

            while (i > 0)
            {
                try
                {
                    i--;
                    Log("url: {0}", URI);
                    Console.WriteLine("{0}[{1:000}]->{2}", Label, Thread.CurrentThread.ManagedThreadId, URI);
                    response = Request(URI);
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

            return response;
        }

        protected override void InternalExecute()
        {
            // Если ссылки нет, то и выполнять нечего
            if (String.IsNullOrEmpty(InputFlow)) return;

            int tid = Thread.CurrentThread.ManagedThreadId;
            // string tname = Thread.CurrentThread.Name;
            // Console.WriteLine("{0}[{1:000}]->{2}", Label, tid, InputFlow);
            // Log("Thread #{0}.{1} Action \"{2}\" loads data from: {3}", tid, tname, Label, InputFlow);

            try
            {
                OutputFlow = WrappedRequest(InputFlow);
            }
            catch (Exception e)
            {
                // Если ошибка появилась и мы должны ее подавить
                if (SuppressErrors)
                {
                    // то заменяем ее значением по умолчанию 
                    // и возвращаем управление родителю, 
                    // без передачи его потомкам

                    // не забываем проверить подстановочные переменные
                    OutputFlow = VarTable.ParseVariables(TextOnError);
                    BackFlow = OutputFlow;
                    return;
                }
                else
                    // иначе мы поднимаем исключение до корневого обработчика
                    throw e;
            }

            ExecuteChild(OutputFlow);
        }

        protected override void SaveProps(SerializationInfo Info)
        {
            Info.AddValue("encoding", DefaulEncoding);
            Info.AddValue("suppress-errors", SuppressErrors);
            Info.AddValue("method", (Method == LoadMetod.GET) ? "GET" : "POST");
            Info.AddValue("agent", UserAgent);
            Info.AddValue("def-text", TextOnError);
            if (Proxy != null)
            {
                Info.AddValue("proxy-host", Proxy.Address.Host);
                Info.AddValue("proxy-port", Proxy.Address.Port);
            }
            else
            {
                Info.AddValue("proxy-port", 0);
            }
        }

        protected override void LoadProps(SerializationInfo Info)
        {
            DefaulEncoding = Info.GetString("encoding");
            SuppressErrors = Info.GetBoolean("suppress-errors");
            Method = (Info.GetString("method") == "POST") ? LoadMetod.POST : LoadMetod.GET;
            UserAgent = Info.GetString("agent");
            TextOnError = Info.GetString("def-text");
            int port = Info.GetInt32("proxy-port");
            if(port > 0)
                Proxy = new WebProxy(Info.GetString("proxy-host"), port);
        }

        public override void SetAttributes(XmlElement Element)
        {
            Element.SetAttribute("type", ScriptConsts.TYPE_LOAD);
            Element.SetAttribute("method", (Method == LoadMetod.GET) ? "GET" : "POST");
            Element.SetAttribute("encoding", DefaulEncoding);
            Element.SetAttribute("suppress-errors", SuppressErrors.ToString());
            Element.SetAttribute("agent", UserAgent);
            Element.SetAttribute("def-text", TextOnError);
        }

        public override void GetAttributes(XmlElement Element)
        {
            Method = (Element.GetAttribute("method") == "POST") ? LoadMetod.POST : LoadMetod.GET;
            DefaulEncoding = Element.GetAttribute("encoding");
            UserAgent = Element.GetAttribute("agent");
            TextOnError = Element.GetAttribute("def-text");
            bool val = SuppressErrors;
            if (Boolean.TryParse(Element.GetAttribute("suppress-errors"), out val))
                SuppressErrors = val;
            else
                SuppressErrors = false;
        }

        [DisplayName("Method")]
        public LoadMetod Method { get; set; }

        [DisplayName("Suppress errors")]
        public bool SuppressErrors { get; set; }

        [DisplayName("Encoding")]
        [TypeConverter(typeof(EncodingTypeConverter))]
        public string DefaulEncoding
        {
            get { return _encoding; }
            set { _encoding = value; }
        }

        public string UserAgent { get; set; }

        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, IsDropDownResizable=true", typeof(UITypeEditor))]
        public string TextOnError { get; set; }

        [System.ComponentModel.Browsable(false)]
        public WebProxy Proxy { get; set; }

        // Не использую, но и удалять пока не хочу 
        private string Request3(string URL)
        {
            WebClient wc = new WebClient();

            return wc.DownloadString(URL);
        }

        private string Request2(string URL)
        {
            WebRequest req = System.Net.WebRequest.Create(URL);
            RequestResponseState state = new RequestResponseState();
            state.request = req;

            req.ContentType = "application/x-www-form-urlencoded";
            if (Method == LoadMetod.POST)
            {
                req.Method = "POST";
                req.ContentLength = req.RequestUri.Query.Length;
            }
            else
            {
                req.Method = "GET";
            }

            lock (finishedLock)
            {
                req.BeginGetResponse(new AsyncCallback(GetResponseCallback), state);
                Monitor.Wait(finishedLock);
            }

            return state.text.ToString();
        }

        private void GetResponseCallback(IAsyncResult ar)
        {
            // Fetch our state information
            RequestResponseState state = (RequestResponseState)ar.AsyncState;

            // Fetch the response which has been generated
            state.response = state.request.EndGetResponse(ar);

            // Store the response stream in the state
            state.remote_stream = state.response.GetResponseStream();

            // Stash an Encoding for the text. I happen to know that
            // my web server returns text in ISO-8859-1 - which is
            // handy, as we don't need to worry about getting half
            // a character in one read and the other half in another.
            // (Use a Decoder if you want to cope with that.)
            state.encoding = Encoding.GetEncoding(DefaulEncoding);

            // Now start reading from it asynchronously
            state.remote_stream.BeginRead(state.buffer, 0, state.buffer.Length,
                                   new AsyncCallback(ReadCallback), state);
        }

        private void ReadCallback(IAsyncResult ar)
        {
            // Fetch our state information
            RequestResponseState state = (RequestResponseState)ar.AsyncState;

            // Find out how much we've read
            int len = state.remote_stream.EndRead(ar);

            // Have we finished now?
            if (len == 0)
            {
                // Dispose of things we can get rid of
                ((IDisposable)state.response).Dispose();
                ((IDisposable)state.remote_stream).Dispose();

                lock (finishedLock)
                    Monitor.Pulse(finishedLock);

                return;
            }

            // Nope - so decode the text and then call BeginRead again
            state.text.Append(state.encoding.GetString(state.buffer, 0, len));
            state.remote_stream.BeginRead(state.buffer, 0, state.buffer.Length,
                                   new AsyncCallback(ReadCallback), state);
        }


    }

    class RequestResponseState
    {
        // In production code, you may well want to make these properties,
        // particularly if it's not a private class as it is in this case.
        internal WebRequest request;
        internal WebResponse response;
        internal Stream remote_stream;
        // internal Stream local_stream;
        internal byte[] buffer = new byte[16384];
        internal Encoding encoding;
        internal StringBuilder text = new StringBuilder();
    }
}
