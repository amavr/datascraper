using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace DataScraper
{
    public class CookieStorage
    {
        private static CookieStorage instance = null;
        private string cookies = null;

        public static CookieStorage Instance
        {
            get
            {
                if (instance == null) 
                    instance = new CookieStorage();
                return instance;
            }
        }

        private CookieStorage()
        {
            cookies = String.Empty;
        }

        public static void Accept(HttpWebResponse Response)
        {
            Cookies = String.Empty;
            
            if (Response == null) return;
            if (String.IsNullOrEmpty(Response.Headers["Set-Cookie"])) return;

            Cookies = Response.Headers["Set-Cookie"];
        }

        public static void Charge(HttpWebRequest Request)
        {
            if (Request == null) return;

            Request.Headers.Add(HttpRequestHeader.Cookie, Cookies);
        }

        public static String Cookies
        {
            get
            {
                return Instance.cookies;
            }

            set
            {
                Instance.cookies = value;
            }
        }
    }
}
