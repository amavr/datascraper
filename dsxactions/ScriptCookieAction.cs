using System;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;

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
            CookieStorage.Cookies = VarTable.ParseVariables(Text);
            
            OutputFlow = InputFlow;
            ExecuteChild(OutputFlow);
        }

        [DisplayName("Cookie")]
        public string Text { get; set; }

    }
}
