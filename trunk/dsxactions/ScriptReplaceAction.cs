using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Drawing.Design;
using System.Xml;
using System.Runtime.Serialization;

namespace DataScraper
{
    [Serializable]
    public class ScriptReplaceAction : ScriptAction
    {
        private string _pattern = String.Empty;
        private string _replace = String.Empty;

        public ScriptReplaceAction()
        {
            ImageIndex = 3;
            Label = "Replace action";
        }

        public ScriptReplaceAction(SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
            ImageIndex = 3;
            LoadProps(Info);
        }

        protected override void InternalExecute()
        {
            // Замена переменных значениями в шаблоне
            string prepared = VarTable.ParseVariables(Replacement);

            // if (InputFlow.Length < 300) Log("..::INP::.. {0}", InputFlow);
            OutputFlow = Regex.Replace(InputFlow, Pattern, prepared, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            // if (OutputFlow.Length < 300) Log("..::OUT::.. {0}", OutputFlow);

            // Сбор результатов выполнения из акций-детей
            ExecuteChild(OutputFlow);
        }

        protected override void SaveProps(SerializationInfo Info)
        {
            Info.AddValue("pattern", Pattern);
            Info.AddValue("replacement", Replacement);
        }

        protected override void LoadProps(SerializationInfo Info)
        {
            Pattern = Info.GetString("pattern");
            Replacement = Info.GetString("replacement");
        }

        public override void SetAttributes(XmlElement Element)
        {
            Element.SetAttribute("type", ScriptConsts.TYPE_REPL);
            Element.SetAttribute("pattern", Pattern);
            Element.SetAttribute("replacement", Replacement);
        }

        public override void GetAttributes(XmlElement Element)
        {
            Pattern = Element.GetAttribute("pattern");
            Replacement = Element.GetAttribute("replacement");
        }

        #region Properties

        public string Pattern
        {
            get { return _pattern; }
            set { _pattern = value; }
        }

        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string Replacement
        {
            get { return _replace; }
            set { _replace = value; }
        }

        #endregion
    }
}
