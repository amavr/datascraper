using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections;
using System.IO;

namespace DataScraper
{
    public class ProxyStorage
    {
        private static ProxyStorage instance = null;

        private ArrayList proxies = null;
        private int nextIndex = 0;

        public static ProxyStorage Instance
        {
            get
            {
                if (instance == null) 
                    instance = new ProxyStorage();
                return instance;
            }
        }

        private ProxyStorage()
        {
            proxies = new ArrayList();
            nextIndex = 0;
        }

        public static void Load(string FileName)
        {
            Instance.proxies.Clear();
            using (StreamReader reader = new StreamReader(FileName, true))
            {
                string line = String.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    int port = 0;
                    if (String.IsNullOrEmpty(line)) continue;

                    string[] parts = line.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2)
                    {
                        if (int.TryParse(parts[1], out port))
                            Instance.proxies.Add(new WebProxy(parts[0], port));
                    }
                }
            }
        }

        public static bool ChangeProxy()
        {
            return ChangeProxy(true);
        }

        public static bool ChangeProxy(bool ShowProxy)
        {
            string px_old = (Proxy == null) ? "null" : Proxy.Address.ToString();
            Instance.nextIndex++;
            string px_new = (Proxy == null) ? "null" : Proxy.Address.ToString();
            
            if(ShowProxy)
                Console.WriteLine("PROXY change {0}->{1}", px_old, px_new);

            return Instance.nextIndex < Instance.proxies.Count;
        }

        public static WebProxy Proxy
        {
            get
            {
                if (Instance.nextIndex < Instance.proxies.Count)
                    return (WebProxy)Instance.proxies[Instance.nextIndex];
                else
                    return null;
            }
        }
    }
}
