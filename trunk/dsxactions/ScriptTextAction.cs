using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Xml;
using System.Runtime.Serialization;

namespace DataScraper
{
    [Serializable]
    public class ScriptTextAction : ScriptAction
    {
        public ScriptTextAction()
        {
            ImageIndex = 0;
            Label = "Text action";
        }

        public ScriptTextAction(SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
            ImageIndex = 0;
            LoadProps(Info);
        }

        protected override void InternalExecute()
        {
            if (String.IsNullOrEmpty(Text))
                Text = String.Empty;

            OutputFlow = VarTable.ParseVariables(Text);
            OutputFlow = IncTable.ParseVariables(OutputFlow);

            ExecuteChild(OutputFlow);
        }

        public override void SetAttributes(XmlElement Element)
        {
            Element.SetAttribute("type", ScriptConsts.TYPE_TEXT);
            Element.SetAttribute("text", Text);
        }

        public override void GetAttributes(XmlElement Element)
        {
            Text = Element.GetAttribute("text");
        }

        protected override void SaveProps(SerializationInfo Info)
        {
            Info.AddValue("text", Text);
        }

        protected override void LoadProps(SerializationInfo Info)
        {
            Text = Info.GetString("text");
        }

        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, IsDropDownResizable=true", typeof(UITypeEditor))]
        public string Text { get; set; }

    }
}
