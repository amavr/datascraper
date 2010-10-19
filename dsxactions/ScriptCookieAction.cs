using System;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml;

namespace DataScraper
{
    [Serializable]
    public class ScriptCookieAction : ScriptTextAction
    {
        public ScriptCookieAction()
        {
            ImageIndex = 10;
            Label = "Cookie";
        }

        public ScriptCookieAction(SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
            ImageIndex = 10;
            LoadProps(Info);
        }

        protected override void InternalExecute()
        {
            if (String.IsNullOrEmpty(Text)) Text = String.Empty;
            CookieStorage.Cookies = VarTable.ParseVariables(Text);
            
            OutputFlow = InputFlow;
            ExecuteChild(OutputFlow);
        }

        public override void SetAttributes(XmlElement Element)
        {
            Element.SetAttribute("type", ScriptConsts.TYPE_COOK);
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

        [DisplayName("Cookie")]
        public string Text { get; set; }

    }
}
