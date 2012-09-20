using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Runtime.Serialization;
using System.Threading;

namespace DataScraper
{
    [Serializable]
    public class ActionTreeNode: TreeNode
    {
        // Входной поток текста
        private string _sour = String.Empty;
        // Выходной поток текста (к детям)
        private string _dest = String.Empty;
        // Обратный (восходящий) поток данных обратного присвоения (от детей), поток конечного результата
        private string _back = String.Empty;

        private ScriptAction _action = null;

        public ActionTreeNode(ScriptAction Action)
            : base(Action.Label)
        {
            this.Action = Action;

            ImageIndex = Action.ImageIndex;
            SelectedImageIndex = ImageIndex;
        }

        public ActionTreeNode(SerializationInfo si, StreamingContext context)
            : base(si, context)
        {
            string type = si.GetValue("ActionType", typeof(String)) as string;
            ScriptAction a = si.GetValue("Action", Type.GetType(type)) as ScriptAction;
            Action = a;
        }

        protected override void Serialize(SerializationInfo si, StreamingContext context)
        {
            base.Serialize(si, context);
            si.AddValue("ActionType", Action.GetType().ToString());
            si.AddValue("Action", Action, Action.GetType());
        }

        public ScriptAction BuildActionTree(bool Deep)
        {
            // Очищаем список потомков у акции, 
            // от предыдущих назначений
            Action.Actions.Clear();

            if (Deep && Nodes.Count > 0)
                foreach (TreeNode node in Nodes)
                    Action.Actions.Add((node as ActionTreeNode).BuildActionTree(Deep));

            return Action;
        }

        public void ApplyResults(bool Deep)
        {
            BackData = Action.BackFlow;
            InputData = Action.InputFlow;
            OutputData = Action.OutputFlow;

            if(Deep && Nodes.Count > 0)
                foreach (TreeNode node in Nodes)
                    (node as ActionTreeNode).ApplyResults(Deep);
        }

        public ActionTreeNode CopyTo(TreeNode NewParent)
        {
            ActionTreeNode node = Clone() as ActionTreeNode;
            if (NewParent == null)
                node.TreeView.Nodes.Add(node);
            else
                NewParent.Nodes.Add(node);

            return node;
        }

        public ActionTreeNode MoveTo(TreeNode NewParent)
        {
            ActionTreeNode node = CopyTo(NewParent);
            Remove();
            return node;
        }

        public bool IsDescendant(TreeNode Node)
        {
            foreach (TreeNode node in Nodes)
            {
                if (node == Node) return true;
                if((node as ActionTreeNode).IsDescendant(Node)) return true;
            }
            return false;
        }

        private void SetAction(ScriptAction Action)
        {
            _action = Action;
            _action.Node = this;
            ImageIndex = _action.ImageIndex;
            SelectedImageIndex = ImageIndex;
        }

        public XmlElement ToXmlElement(XmlDocument Document)
        {
            XmlElement element = Document.CreateElement(ScriptConsts.TAG_NAME);
            element.SetAttribute(ScriptConsts.ATTR_NAME, Text);
            Action.Save(element);
            foreach (TreeNode node in Nodes)
                element.AppendChild((node as ActionTreeNode).ToXmlElement(Document));

            return element;
        }

        public static ActionTreeNode ParseXmlElement(XmlElement Element)
        {
            ScriptAction action = null;
            switch (Element.GetAttribute(ScriptConsts.ATTR_TYPE))
            {
                case ScriptConsts.TYPE_TEXT:
                    action = new ScriptTextAction();
                    break;

                case ScriptConsts.TYPE_COOK:
                    action = new ScriptCookieAction();
                    break;

                case ScriptConsts.TYPE_DATE:
                    action = new ScriptDateAction();
                    break;

                case ScriptConsts.TYPE_LOAD:
                    action = new ScriptLoadAction();
                    break;

                case ScriptConsts.TYPE_DOWNLOAD:
                    action = new ScriptDownloadAction();
                    break;

                case ScriptConsts.TYPE_SAVE:
                    action = new ScriptSaveAction();
                    break;

                case ScriptConsts.TYPE_FIND:
                    action = new ScriptFindAction();
                    break;

                case ScriptConsts.TYPE_REPL:
                    action = new ScriptReplaceAction();
                    break;

                case ScriptConsts.TYPE_SET_VAR:
                    action = new ScriptSetVarAction();
                    break;

                case ScriptConsts.TYPE_GET_VAR:
                    action = new ScriptGetVarAction();
                    break;

                case ScriptConsts.TYPE_NEXT_PAGE:
                    action = new ScriptNextURLAction();
                    break;

                case ScriptConsts.TYPE_INC:
                    action = new ScriptIncAction();
                    break;

            }
            action.Load(Element);

            action.OnLogMessage += new ActionCallback(LogAction);

            ActionTreeNode node = new ActionTreeNode(action);

            foreach (XmlNode child in Element.SelectNodes(ScriptConsts.TAG_NAME))
                node.Nodes.Add(ParseXmlElement(child as XmlElement));

            return node;
        }

        private static void LogAction(Object Sender, string Msg)
        {
            Console.WriteLine("{0} T:{1} [{2}] {3}", DateTime.Now.ToString("HH:mm:ss"), Thread.CurrentThread.ManagedThreadId, (Sender as ScriptAction).Label, Msg);
        }

        #region Overreded

        public override object Clone()
        {
            // Копируем через сохранение узла в XmlElement'e
            XmlDocument doc = new XmlDocument();
            XmlElement element = ToXmlElement(doc);
            ActionTreeNode cloned = ActionTreeNode.ParseXmlElement(element);

            return cloned;
        }

        #endregion

        #region Properties

        public ScriptAction Action
        {
            get { return _action; }
            set { SetAction(value); }
        }

        [System.ComponentModel.Browsable(false)]
        public string InputData
        {
            get
            {
                return _sour;
            }
            set
            {
                _sour = value;
            }
        }

        [System.ComponentModel.Browsable(false)]
        public string OutputData
        {
            get
            {
                return _dest;
            }
            set
            {
                _dest = value;
            }
        }

        [System.ComponentModel.Browsable(false)]
        public string BackData
        {
            get
            {
                return _back;
            }
            set
            {
                _back = value;
            }
        }
        #endregion
    }
}
