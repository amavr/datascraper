using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DataScraper
{
    [Serializable]
    public class ScriptGetVarAction : ScriptAction
    {
        public ScriptGetVarAction()
        {
            ImageIndex = 5;
            Label = "VarName";
        }

        public ScriptGetVarAction(SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
            ImageIndex = 5;
            LoadProps(Info);
        }

        protected override void InternalExecute()
        {
            // Вход "Node.InputData" не используем
            // отдаем на выход значение из памяти
            OutputFlow = VarTable.Instance[Label];
            ExecuteChild(OutputFlow);
        }

        protected override void SaveProps(SerializationInfo Info)
        {
        }

        protected override void LoadProps(SerializationInfo Info)
        {
        }

        public override void SetAttributes(System.Xml.XmlElement Element)
        {
            Element.SetAttribute("type", ScriptConsts.TYPE_GET_VAR);
        }

        public override void GetAttributes(System.Xml.XmlElement Element)
        {
            // Nothing
        }
    }
}
