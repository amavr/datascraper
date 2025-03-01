﻿using System;
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

            if (mc.Count > 0)
            {
                List<string> ss = new List<string>();
                string rest = dest;
                int beg_pos = 0;
                for (int i = 0; i < mc.Count; i++)
                {
                    Match m = mc[i];
                    string var_name = m.Groups[1].Value;

                    if (vars.ContainsKey(var_name))
                    {
                        ss.Add(dest.Substring(beg_pos, m.Index - beg_pos) + vars[var_name]);
                    }
                    else
                    {
                        ss.Add(dest.Substring(beg_pos, m.Index - beg_pos) + m.Value);
                    }
                    // сохраняем остаток
                    beg_pos = m.Index + m.Length;
                    rest = dest.Substring(beg_pos);
                }
                // добавляем остаток
                ss.Add(rest);

                // собираем строку
                dest = String.Empty;
                foreach (string s in ss) dest += s;
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
