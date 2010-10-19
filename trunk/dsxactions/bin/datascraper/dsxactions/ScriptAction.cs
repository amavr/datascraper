using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Drawing.Design;
using System.Threading;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataScraper
{
    public delegate void ActionCallback(Object Sender, String Info);

    public abstract class ScriptAction: ISerializable, ICloneable
    {
        private string _label = "Abstract";
        private string _desc = String.Empty;
        private volatile string _out = String.Empty;

        public event ActionCallback OnLogMessage;

        public ScriptAction()
        {
            Label = "Abstract action";
            Actions = new List<ScriptAction>();
        }

        public ScriptAction(SerializationInfo Info, StreamingContext Context)
        {
            Label = Info.GetString("label");
            Description = Info.GetString("desc");
            Actions = new List<ScriptAction>();
        }

        abstract protected void SaveProps(SerializationInfo Info);
        abstract protected void LoadProps(SerializationInfo Info);

        abstract protected void InternalExecute();

        abstract public void SetAttributes(XmlElement Element);
        abstract public void GetAttributes(XmlElement Element);

        public void Execute()
        {
            try
            {
                // Log("T:{0} I:{1}", Thread.CurrentThread.ManagedThreadId, (InputFlow.Length > 500) ? InputFlow.Substring(0, 500) : InputFlow);
                BackFlow = String.Empty;
                OutputFlow = String.Empty;
                InternalExecute();
            }
            catch (Exception e)
            {
                // Если исключение было обработан ранее
                if (e.Data.Contains(ScriptConsts.EXCEPT_PROCESSED_KEY))
                {
                    // поднимаем его выше
                    throw e;
                }
                // ранее не обрабатывалось
                else
                {
                    string msg = "Action [" + Label + "] has error " + e.Message + "\n\nStop the execution?";
                    // Прервать выполнение сценария?
                    if (MessageBox.Show(msg, "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // указываем, что мы его обработали
                        e.Data.Add(ScriptConsts.EXCEPT_PROCESSED_KEY, true);
                        // поднимаем его выше
                        throw e;
                    }
                }
            }
        }

        protected void ExecuteChild(string ParentFlow)
        {
            // Если нет потомков, 
            // тогда добавляем в обратный поток данные выходного потока
            if (Actions.Count == 0)
                BackFlow += ParentFlow;
            else
                foreach (ScriptAction action in Actions)
                {
                    action.InputFlow = ParentFlow;
                    action.Execute();
                    BackFlow += action.BackFlow;
                }
        }

        public void Save(XmlElement Element)
        {
            Element.SetAttribute("name", Label);
            Element.SetAttribute("desc", Description);
            SetAttributes(Element);
        }

        public void Load(XmlElement Element)
        {
            Label = Element.GetAttribute("name");
            Description = Element.GetAttribute("desc");
            GetAttributes(Element);
        }

        public void Log(string FormatString, params object[] Args)
        {
            // Console.WriteLine(FormatString, Args);

            if (OnLogMessage == null) return;

            string msg = String.Format(FormatString, Args);
            OnLogMessage(this, msg);
        }

        protected string CheckUncorrectChar(string Val)
        {
            string x = "";
            x = Val.Replace("\t", "&#x09;");
            return x;
        }

        protected int GetPropInt(XmlElement Element, string AttrName, int Default)
        {
            int x = 0;
            if (Int32.TryParse(Element.GetAttribute(AttrName), out x))
                return x;
            else
                return Default;
        }

        protected bool GetPropBool(XmlElement Element, string AttrName, bool Default)
        {
            bool x = Default;
            if (Boolean.TryParse(Element.GetAttribute(AttrName), out x))
                return x;
            else
                return Default;
        }

        #region ISerializable Members

        void ISerializable.GetObjectData(SerializationInfo Info, StreamingContext Context)
        {
            Info.SetType(this.GetType());
            Info.AddValue("label", Label);
            Info.AddValue("desc", Description);
            SaveProps(Info);
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            ScriptAction action;
            BinaryFormatter bin = new BinaryFormatter();
            using (MemoryStream mem = new MemoryStream())
            {
                bin.Serialize(mem, this);
                mem.Seek(0, SeekOrigin.Begin);
                action = (ScriptAction)bin.Deserialize(mem);
            }
            return action;
            //return (ScriptAction)this.MemberwiseClone();
        }

        public object Clone(bool Deep)
        {
            ScriptAction action = (ScriptAction)Clone();
            if (Deep)
                foreach (ScriptAction child in Actions)
                    action.Actions.Add((ScriptAction)child.Clone(Deep));

            return action;
        }

        #endregion

        #region Properties

        [System.ComponentModel.Browsable(false)]
        public int ImageIndex = -1;

        /*
         */
        [NonSerialized]
        [System.ComponentModel.Browsable(false)]
        public ActionTreeNode Node = null;

        [NonSerialized]
        [System.ComponentModel.Browsable(false)]
        public List<ScriptAction> Actions = null;

        [NonSerialized]
        [System.ComponentModel.Browsable(false)]
        public String BackFlow = String.Empty;

        [NonSerialized]
        [System.ComponentModel.Browsable(false)]
        public String InputFlow = String.Empty;

        // [NonSerialized]
        [System.ComponentModel.Browsable(false)]
        public String OutputFlow
        {
            get { return _out; }
            set { _out = value; }
        }
        // public String OutputFlow = String.Empty;

        [System.Xml.Serialization.XmlAttribute("label")]
        public string Label 
        {
            get { return _label; }
            set
            {
                _label = value;
                if (Node != null) 
                    Node.Text = _label;
            }
        }

        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, IsDropDownResizable=true", typeof(UITypeEditor))]
        public string Description
        {
            get { return _desc; }
            set 
            { 
                _desc = value;
                if (Node != null)
                    Node.ToolTipText = _desc;
            }
        }

        #endregion

    }
}
