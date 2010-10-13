using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DataScraper
{
    class VarTable
    {
        private static VarTable _instance = null;
        private Dictionary<String,String> _vars = null;

        public static VarTable Instance
        {
            get
            {
                if (_instance == null) _instance = new VarTable();
                return _instance;
            }
        }

        public static string ParseVariables(string Sour)
        {
            Dictionary<String,String> vars = Instance._vars;

            string dest = Sour;
            Regex re = new Regex(@"\{(\D[^\}]*)\}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            MatchCollection mc = re.Matches(Sour);

            for (int i = 0; i < mc.Count; i++)
            {
                Match m = mc[i];

                // Если группы указаны
                int num = m.Groups.Count;
                if (num > 0)
                {
                    string var_name = m.Groups[1].Value;
                    if(vars.ContainsKey(var_name))
                        dest = dest.Replace(@"{"+ var_name +"}", vars[var_name]);
                }
            }
            return dest;
        }

        private VarTable()
        {
            _vars = new Dictionary<string, string>();
        }

        public string this[string VarName]
        {
            get
            {
                try
                {
                    return _vars[VarName];
                }
                catch (Exception)
                {
                    return "";
                }
            }
            set
            {
                _vars[VarName] = value;
            }
        }
    }
}
