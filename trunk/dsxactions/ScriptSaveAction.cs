using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace DataScraper
{
    [Serializable]
    public class ScriptSaveAction : ScriptAction
    {
        private string _encoding = "utf-8";

        public ScriptSaveAction()
        {
            ImageIndex = 6;
            Label = "Save action";
        }

        public ScriptSaveAction(SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
            ImageIndex = 6;
            LoadProps(Info);
        }

        private void Save(string Data)
        {
            // В имени файла могут использователься SetVarAction
            FileName = VarTable.ParseVariables(FileName);
            FileName = IncTable.ParseVariables(FileName);

            string path = Path.GetDirectoryName(FileName);

            if(String.IsNullOrEmpty(path) ==  false)
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

            StreamWriter writer = new StreamWriter(FileName, Append, Encoding.GetEncoding(DefaulEncoding));
            writer.Write(Data);
            writer.Close();
        }

        protected override void InternalExecute()
        {
            // Сквозной поток
            OutputFlow = InputFlow;

            // Сохраняем его в файле
            if (!ChildrenFlow)
            {
                Save(InputFlow);
                // Если указано, что поток надо остановить, то передаем далее пустую строку
                if (BreakFlow) OutputFlow = String.Empty;
            }

            ExecuteChild(OutputFlow);

            if (ChildrenFlow)
            {
                Save(BackFlow);
                // Если указано, что поток надо остановить, то передаем далее пустую строку
                if (BreakFlow) BackFlow = String.Empty;
            }
        }

        protected override void SaveProps(SerializationInfo Info)
        {
            Info.AddValue("file", FileName);
            Info.AddValue("encoding", DefaulEncoding);
            Info.AddValue("append", Append);
            Info.AddValue("ascending", ChildrenFlow);
            Info.AddValue("break", BreakFlow);
        }

        protected override void LoadProps(SerializationInfo Info)
        {
            FileName = Info.GetString("file");
            DefaulEncoding = Info.GetString("encoding");
            Append = Info.GetBoolean("append");
            ChildrenFlow = Info.GetBoolean("ascending");
            BreakFlow = Info.GetBoolean("break");
        }

        public override void SetAttributes(System.Xml.XmlElement Element)
        {
            Element.SetAttribute("type", ScriptConsts.TYPE_SAVE);
            Element.SetAttribute("file", FileName);
            Element.SetAttribute("encoding", DefaulEncoding);
            Element.SetAttribute("append", Append.ToString());
            Element.SetAttribute("ascending", ChildrenFlow.ToString());
            Element.SetAttribute("break", BreakFlow.ToString());
        }

        public override void GetAttributes(System.Xml.XmlElement Element)
        {
            FileName = Element.GetAttribute("file");
            DefaulEncoding = Element.GetAttribute("encoding");
            Append = Element.GetAttribute("append") == true.ToString();
            ChildrenFlow = Element.GetAttribute("ascending") == true.ToString();
            BreakFlow = Element.GetAttribute("break") == true.ToString();
        }

        public bool Append { get; set; }

        [DisplayName("Children flow")]
        public bool ChildrenFlow { get; set; }

        [DisplayName("Break flow")]
        public bool BreakFlow { get; set; }

        //[EditorAttribute(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string FileName { get; set; }

        [TypeConverter(typeof(EncodingTypeConverter))]
        public string DefaulEncoding
        {
            get { return _encoding; }
            set { _encoding = value; }
        }

    }
}
