using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DataScraper
{
    class IncTable
    {
        private static IncTable _instance = null;
        private Dictionary<String,ScriptIncAction> _vars = null;

        public static IncTable Instance
        {
            get
            {
                if (_instance == null) _instance = new IncTable();
                return _instance;
            }
        }

        public static string ParseVariables(string Sour)
        {
            Dictionary<String,ScriptIncAction> vars = Instance._vars;

            string dest = Sour;
            Regex re = new Regex(@"\{(\D[^\}]*)\}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            MatchCollection mc = re.Matches(Sour);

            // В отличие от простой подстановки текста, здесь требуется устанавливать последовательные значения инкрементатора
            // поэтому строка разбивается на части, из которых потом собирается результат
            if (mc.Count > 0)
            {

                List<string> ss = new List<string>();
                string rest = dest;
                int beg_pos = 0;
                for (int i = 0; i < mc.Count; i++)
                {
                    // поиск икремента
                    Match m = mc[i];
                    string var_name = m.Groups[1].Value;
                    ScriptIncAction action = vars[var_name];

                    // если инкремент не найден, то ничего не меняем
                    if (action == null)
                    {
                        ss.Add(dest.Substring(beg_pos, m.Index - beg_pos) + m.Value);
                    }
                    else
                    {
                        ss.Add(dest.Substring(beg_pos, m.Index - beg_pos) + action.Value.ToString());
                        action.Inc();
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

        private IncTable()
        {
            _vars = new Dictionary<string, ScriptIncAction>();
        }

        public ScriptIncAction this[string VarName]
        {
            get
            {
                try
                {
                    return _vars[VarName];
                }
                catch (Exception)
                {
                    return null;
                }
            }
            set
            {
                _vars[VarName] = value;
            }
        }
    }
}
