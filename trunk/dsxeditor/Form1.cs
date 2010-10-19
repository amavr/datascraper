using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using DataScraper.Properties;
using System.Diagnostics;
using System.Xml;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading;

using DataScraper;

namespace DataScraper
{

    public partial class frmMain : Form
    {
        private bool _modified = false;
        private string _project = String.Empty;
        private const string TYPE_DRAG = "DataScraper.ActionTreeNode";
        private TreeNode MarkedNode = null;

        public frmMain()
        {
            InitializeComponent();

            ThreadPool.SetMaxThreads(5, 5);

            string[] list = Environment.GetCommandLineArgs();
            if (list.Length > 1)
                LoadProject(list[1]);
            else
                NewProject();
        }

        [DllImport("user32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);

        private void BringTop()
        {
            SetForegroundWindow(this.Handle.ToInt32());
        }

        private void MoveSplitter(PropertyGrid propertyGrid, int x)
        {
            BindingFlags gwFlags = BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance;
            object propertyGridView = typeof(PropertyGrid).InvokeMember("gridView", gwFlags, null, propertyGrid, null);
            BindingFlags msFlags = BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance;
            propertyGridView.GetType().InvokeMember("MoveSplitterTo", msFlags, null, propertyGridView, new object[] { x });
        }

        private string GetPubName()
        {
            return (_project == String.Empty) ? Names.NewProject : _project;
        }

        private void SetCaption()
        {
            string project = GetPubName();
            Text = String.Format(Names.CaptionTemplate, project, _modified ? "*" : "");
        }

        private void ClearTree()
        {
            treeScript.Nodes.Clear();
            props.SelectedObject = null;
        }

        private void NewProject()
        {
            if (Modified)
            {
                DialogResult res = MessageBox.Show("Save changes to " + GetPubName() + "?", Names.ProgrammName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Cancel) return;
                if (res == DialogResult.Yes) 
                {
                    Save();
                }
            }
            FileName = String.Empty;
            ClearTree();
            ShowNodeData(treeScript.SelectedNode as ActionTreeNode);
            SetCaption();
            Modified = false;
            MoveSplitter(props, 150);
        }

        private void LoadProject()
        {
            if (Modified)
            {
                DialogResult res = MessageBox.Show("Save changes to " + GetPubName() + "?", Names.ProgrammName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Cancel) return;
                if (res == DialogResult.Yes)
                {
                    Save();
                }
            }

            if(!String.IsNullOrEmpty(FileName))
                dlgOpen.InitialDirectory = Path.GetDirectoryName(FileName);

            if (dlgOpen.ShowDialog() == DialogResult.OK)
                LoadProject(dlgOpen.FileName);

        }

        private void LoadProject(string ProjectName)
        {
            ClearTree();

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(ProjectName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Loading project error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            XmlElement root = doc.DocumentElement;

            FileName = ProjectName;
            TreeNode parent = null;

            foreach (XmlNode element in root.SelectNodes(ScriptConsts.TAG_NAME))
            {
                ActionTreeNode node = ActionTreeNode.ParseXmlElement(element as XmlElement);
                if (parent == null)
                    treeScript.Nodes.Add(node);
                else
                    parent.Nodes.Add(node);
            }
            SetCaption();
            Modified = false;

            if (treeScript.Nodes.Count > 0)
            {
                treeScript.SelectedNode = treeScript.Nodes[0];
                treeScript.SelectedNode.ExpandAll();
            }
            ShowNodeData(treeScript.SelectedNode as ActionTreeNode);
            MoveSplitter(props, 150);
        }

        private void SaveProject()
        {
            if (treeScript.Nodes.Count == 0) return;

            XmlDocument doc = new XmlDocument();
            
            XmlElement root = doc.CreateElement(ScriptConsts.DOC_TAG_NAME);
            doc.AppendChild(root);

            ActionTreeNode node = treeScript.Nodes[0] as ActionTreeNode;
            root.AppendChild(node.ToXmlElement(doc));

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(FileName, settings);
            doc.Save(writer);
            writer.Close();
            //doc.Save(FileName);

            SetCaption();
            Modified = false;
        }

        private bool Save()
        {
            if (_project == String.Empty)
                if (SaveAs() == false) return false;

            SaveProject();

            return true;
        }

        private bool SaveAs()
        {
            dlgSave.FileName = FileName;

            if (dlgSave.ShowDialog() == DialogResult.Cancel) return false;

            FileName = dlgSave.FileName;
            return Save();
        }

        public bool Modified
        {
            get { return _modified; }
            set 
            { 
                _modified = value;
                SetCaption();
            }
        }

        public string FileName
        {
            get
            {
                return _project;
            }

            set
            {
                _project = value;
                SetCaption();
            }
        }

        #region GridSplitter
        /// <summary>
        /// Сохранение положения разделителя в гриде
        /// </summary>
        private void SaveGridSplitterPos()
        {
            Type type = props.GetType();
            FieldInfo field = type.GetField("gridView",
              BindingFlags.NonPublic | BindingFlags.Instance);

            object valGrid = field.GetValue(props);
            Type gridType = valGrid.GetType();
            Settings.Default.GridSplitterPos = (int)gridType.InvokeMember(
              "GetLabelWidth",
              BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Instance,
              null,
              valGrid, new object[] { });

            Trace.WriteLine("SaveGridSplitterPos(): "+ Settings.Default.GridSplitterPos);
        }

        /// <summary>
        /// Восстановление положения разделителя в гриде
        /// </summary>
        private void RestoreGridSplitterPos()
        {
            try
            {
                Type type = props.GetType();
                FieldInfo field = type.GetField("gridView",
                  BindingFlags.NonPublic | BindingFlags.Instance);

                object valGrid = field.GetValue(props);
                Type gridType = valGrid.GetType();
                gridType.InvokeMember("MoveSplitterTo",
                  BindingFlags.NonPublic | BindingFlags.InvokeMethod
                    | BindingFlags.Instance,
                  null,
                  valGrid, new object[] { Settings.Default.GridSplitterPos });

                Trace.WriteLine("RestoreGridSplitterPos(): "+ Settings.Default.GridSplitterPos);
            }
            catch
            {
                Trace.WriteLine("MainForm::RestoreGridSplitterPos() exception");
            }
        }
        #endregion

        private void AddActionNode(TreeNode Node)
        {

            if (treeScript.SelectedNode == null)
                treeScript.Nodes.Add(Node);
            else
                treeScript.SelectedNode.Nodes.Add(Node);

            treeScript.SelectedNode = Node;

            Modified = true;
        }

        private void DeleteAction(TreeNode Node)
        {
            if (treeScript.SelectedNode == null) return;

            treeScript.SelectedNode.Remove();
            Modified = true;
        }

        private void CopyAction(TreeNode Node)
        {
            if (Node != null)
            {
                Clipboard.SetData("ActionTreeNode", Serialize(Node));
                /*
                XmlDocument doc = new XmlDocument();
                ActionTreeNode anode = Node as ActionTreeNode;
                doc.AppendChild(anode.ToXmlElement(doc));
                Clipboard.SetData("Text", doc.InnerXml);
                 */
            }
        }

        private void PasteAction(TreeNode Node)
        {
            if (Clipboard.ContainsData("ActionTreeNode"))
            {
                TreeNodeCollection nodes = (Node == null) ? treeScript.Nodes : treeScript.SelectedNode.Nodes;
                ActionTreeNode anode = DeSerialize(Clipboard.GetData("ActionTreeNode").ToString()) as ActionTreeNode;
                nodes.Add(anode);
                anode.ExpandAll();

                Modified = true;
            }
        }

        private void Run(ActionTreeNode Node, bool Deep)
        {
            DateTime dt = DateTime.Now;

            if (Node == null) return;

            try
            {
                Cursor = Cursors.WaitCursor;
                RunActions(Node, Deep);
                ShowNodeData(treeScript.SelectedNode as ActionTreeNode);
            }
            catch (Exception)
            {
                // Nothing to do
                // MessageBox.Show(e.Message);
            }
            finally
            {
                TimeSpan ts = DateTime.Now.Subtract(dt);
                slabTime.Text = String.Format("Time: {0} min {1} sec", ts.Minutes, ts.Seconds);
                Cursor = Cursors.Default;
            }

            BringTop();
        }

        private void RunActions(ActionTreeNode Node, bool Deep)
        {
            // Строим дерево акций для исполнения
            ScriptAction action = Node.BuildActionTree(Deep);
            
            // Инициализируем входные данные корневого элемента
            action.InputFlow = Node.InputData;
            
            // Запускаем исполнение корневой акции
            // Важно! Параметра вложенного исполнения - Deep нет!
            // т.к. выпоняем всю созданную иерархию
            action.Execute();
            
            // Данные из акций переносим в узлы для отображения на экране
            Node.ApplyResults(Deep);
            ShowNodeData(treeScript.SelectedNode as ActionTreeNode);
        }

        private void RunProject()
        {
            Run((treeScript.Nodes[0] as ActionTreeNode), true);
        }

        private void RunSelectedBranch()
        {
            Run((treeScript.SelectedNode as ActionTreeNode), true);
        }

        private void RunSelectedNode()
        {
            Run((treeScript.SelectedNode as ActionTreeNode), false);
        }

        private void CloneSelected()
        {
            if (treeScript.SelectedNode == null) return;

            ActionTreeNode node1 = treeScript.SelectedNode as ActionTreeNode;
            ActionTreeNode node2 = node1.Clone() as ActionTreeNode;
            if(node1.Parent == null)
                treeScript.Nodes.Add(node2);
            else
                node1.Parent.Nodes.Add(node2);

            treeScript.SelectedNode = node2;

            ShowNodeData(treeScript.SelectedNode as ActionTreeNode);
        }

        private void SwapNodes(TreeNode SourNode, TreeNode DestNode)
        {
            if (SourNode == null || DestNode == null) return;

            int index = DestNode.Index;
            SourNode.Remove();
            DestNode.Parent.Nodes.Insert(index, SourNode);
            treeScript.SelectedNode = SourNode;

            Modified = true;
        }

        private void Mark(TreeNode Node)
        {
            if (Node == null) return;
            Node.NodeFont = new Font(treeScript.Font, FontStyle.Underline);
        }

        private void Unmark(TreeNode Node)
        {
            if (Node == null) return;
            Node.NodeFont = new Font(treeScript.Font, FontStyle.Regular);
        }

        private void ShowNodeData(ActionTreeNode treeNode)
        {
            if (treeNode == null)
            {
                props.SelectedObject = null;
                tboxSour.Text = String.Empty;
                tboxDest.Text = String.Empty;
                tboxBack.Text = String.Empty;
            }
            else
            {
                props.SelectedObject = treeNode.Action;
                tboxSour.Text = treeNode.InputData;
                tboxDest.Text = treeNode.OutputData;
                tboxBack.Text = treeNode.BackData;
            }
        }

        private void props_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Modified = true;
        }

        private void treeScript_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowNodeData(e.Node as ActionTreeNode);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Modified)
            {
                DialogResult res = MessageBox.Show("Save changes to " + GetPubName() + "?", Names.ProgrammName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                if (res == DialogResult.Yes) Save();
            }

            SaveGridSplitterPos();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            RestoreGridSplitterPos();
        }

        private void btnNewProject_Click(object sender, EventArgs e)
        {
            NewProject();
        }

        private void btnOpenProject_Click(object sender, EventArgs e)
        {
            LoadProject();
        }

        private void btnSaveProject_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void miSaveAsProject_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            RunProject();
        }

        private void btnRunBranch_Click(object sender, EventArgs e)
        {
            RunSelectedBranch();
        }

        private void btnRunNode_Click(object sender, EventArgs e)
        {
            RunSelectedNode();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteAction(treeScript.SelectedNode);
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnClone_Click(object sender, EventArgs e)
        {
            CloneSelected();
        }

        private void treeScript_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(TYPE_DRAG, false))
            {
                // Определение нового родителя узла
                Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                TreeNode parent = ((TreeView)sender).GetNodeAt(pt);

                // Определение перетакскиваемых Tree & XML узлов
                ActionTreeNode moved = e.Data.GetData(TYPE_DRAG) as ActionTreeNode;
                moved = moved.MoveTo(parent);
                parent.ExpandAll();

                moved.TreeView.SelectedNode = moved;

                Unmark(MarkedNode);

                Modified = true;
            }
        }

        private void treeScript_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void treeScript_DragLeave(object sender, EventArgs e)
        {
            Unmark(MarkedNode);
        }

        private void treeScript_DragOver(object sender, DragEventArgs e)
        {
            Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            ActionTreeNode under = ((sender as TreeView).GetNodeAt(pt)) as ActionTreeNode;
            ActionTreeNode over = e.Data.GetData(TYPE_DRAG) as ActionTreeNode;

            if ((under == null) || (under == over) || (over.IsDescendant(under)))
            {
                e.Effect = DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.Move;

                if (MarkedNode != under)
                {
                    Unmark(MarkedNode);
                    Mark(under);
                    MarkedNode = under;
                }
            }
        }

        private string Serialize(object objectToSerialize)
        {
            string serialString = null;
            using (System.IO.MemoryStream ms1 = new System.IO.MemoryStream())
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(ms1, objectToSerialize);
                byte[] arrayByte = ms1.ToArray();
                serialString = Convert.ToBase64String(arrayByte);
            }
            return serialString;
        }

        private object DeSerialize(string serializationString)
        {
            object deserialObject = null;
            byte[] arrayByte = Convert.FromBase64String(serializationString);
            using (System.IO.MemoryStream ms1 = new System.IO.MemoryStream(arrayByte))
            {
                BinaryFormatter b = new BinaryFormatter();
                deserialObject = b.Deserialize(ms1);
            }
            return deserialObject;
        }

        private void treeScript_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            TreeNode node = treeScript.SelectedNode;
            SwapNodes(node, node.PrevNode);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            TreeNode node = treeScript.SelectedNode;
            SwapNodes(node, node.NextNode);
        }

        private void btnNewText_Click(object sender, EventArgs e)
        {
            AddActionNode(new ActionTreeNode(new ScriptTextAction()));
        }

        private void btnNewLoad_Click(object sender, EventArgs e)
        {
            AddActionNode(new ActionTreeNode(new ScriptLoadAction()));
        }

        private void btnNewSearch_Click(object sender, EventArgs e)
        {
            AddActionNode(new ActionTreeNode(new ScriptFindAction()));
        }

        private void btnNewReplace_Click(object sender, EventArgs e)
        {
            AddActionNode(new ActionTreeNode(new ScriptReplaceAction()));
        }

        private void btnNewSetVar_Click(object sender, EventArgs e)
        {
            AddActionNode(new ActionTreeNode(new ScriptSetVarAction()));
        }

        private void btnNewGetVar_Click(object sender, EventArgs e)
        {
            AddActionNode(new ActionTreeNode(new ScriptGetVarAction()));
        }

        private void btnNewSave_Click(object sender, EventArgs e)
        {
            AddActionNode(new ActionTreeNode(new ScriptSaveAction()));
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            AddActionNode(new ActionTreeNode(new ScriptNextURLAction()));
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            AddActionNode(new ActionTreeNode(new ScriptDownloadAction()));
        }

        private void btnNewCookie_Click(object sender, EventArgs e)
        {
            AddActionNode(new ActionTreeNode(new ScriptCookieAction()));
        }

        private void btnNewDate_Click(object sender, EventArgs e)
        {
            AddActionNode(new ActionTreeNode(new ScriptDateAction()));
        }

        private void miAbout_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            CopyAction(treeScript.SelectedNode);
            DeleteAction(treeScript.SelectedNode);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            CopyAction(treeScript.SelectedNode);
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            PasteAction(treeScript.SelectedNode);
        }

    }
}