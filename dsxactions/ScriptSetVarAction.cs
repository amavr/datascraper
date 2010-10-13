using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace DataScraper
{
    [Serializable]
    public class ScriptSetVarAction : ScriptAction
    {
        public ScriptSetVarAction()
        {
            ImageIndex = 4;
            Label = "VarName";
        }

        public ScriptSetVarAction(SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
            ImageIndex = 4;
            LoadProps(Info);
        }

        protected override void InternalExecute()
        {

            // По умолчанию, к потомкам передается все то, что узел получил
            OutputFlow = InputFlow;

            // Если сохраняем нисходящий поток, 
            // то делаем это до работы потомков
            if (ChildrenFlow == false)
            {
                VarTable.Instance[Label] = InputFlow;
                // Если указано, что поток надо остановить, то передаем далее пустую строку
                if(BreakFlow) OutputFlow = String.Empty;
            }

            ExecuteChild(OutputFlow);

            // Если сохраняем восходящий поток, 
            // то делаем это после работы потомков
            if (ChildrenFlow)
            {
                VarTable.Instance[Label] = BackFlow;
                // Если указано, что поток надо остановить, то передаем далее пустую строку
                if (BreakFlow) BackFlow = String.Empty;
            }

        }

        protected override void SaveProps(SerializationInfo Info)
        {
            Info.AddValue("ascending", ChildrenFlow);
            Info.AddValue("break", BreakFlow);
        }

        protected override void LoadProps(SerializationInfo Info)
        {
            ChildrenFlow = Info.GetBoolean("ascending");
            BreakFlow = Info.GetBoolean("break");
        }

        public override void SetAttributes(System.Xml.XmlElement Element)
        {
            Element.SetAttribute("type", ScriptConsts.TYPE_SET_VAR);
            Element.SetAttribute("ascending", ChildrenFlow.ToString());
            Element.SetAttribute("break", BreakFlow.ToString());
        }

        public override void GetAttributes(System.Xml.XmlElement Element)
        {
            ChildrenFlow = Element.GetAttribute("ascending") == true.ToString();
            BreakFlow = Element.GetAttribute("break") == true.ToString();
        }

        [DisplayName("Children flow")]
        public bool ChildrenFlow { get; set; }

        [DisplayName("Break flow")]
        public bool BreakFlow { get; set; }

    }
}
