using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Drawing.Design;
using System.Xml;

namespace DataScraper
{
    [Serializable]
    public class ScriptIncAction: ScriptAction
    {
        public ScriptIncAction()
        {
            ImageIndex = 11;
            Label = "Increment action";
            InitValue = 0;
            Increment = 1;
            Value = 0;
        }

        public ScriptIncAction(SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
            ImageIndex = 11;
            LoadProps(Info);
        }

        protected override void InternalExecute()
        {
            // Инициализация при прохождении потока
            Value = InitValue;

            // Сквозной поток
            OutputFlow = InputFlow;

            IncTable.Instance[Label] = this;

            ExecuteChild(OutputFlow);
        }

        public override void SetAttributes(XmlElement Element)
        {
            Element.SetAttribute("type", ScriptConsts.TYPE_INC);
            Element.SetAttribute("init", InitValue.ToString());
            Element.SetAttribute("increment", Increment.ToString());
        }

        public override void GetAttributes(XmlElement Element)
        {
            try
            {
                InitValue = Int32.Parse(Element.GetAttribute("init"));
                Increment = Int32.Parse(Element.GetAttribute("increment"));
            }
            catch 
            {
                InitValue = 0;
            }
        }

        public void Inc()
        {
            Value += Increment;
        }

        protected override void SaveProps(SerializationInfo Info)
        {
            Info.AddValue("init", InitValue);
            Info.AddValue("increment", Increment);
        }

        protected override void LoadProps(SerializationInfo Info)
        {
            InitValue = Info.GetInt32("init");
            Increment = Info.GetInt32("increment");
        }

        public int InitValue { get; set; }

        public int Value;

        public int Increment { get; set; }

    }
}
