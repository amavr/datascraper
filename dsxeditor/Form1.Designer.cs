namespace DataScraper
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.slabTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.programmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miNewProject = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenProject = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveProject = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveAsProject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miRun = new System.Windows.Forms.ToolStripMenuItem();
            this.miRunAt = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.miNewText = new System.Windows.Forms.ToolStripMenuItem();
            this.miNewLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.miNewFind = new System.Windows.Forms.ToolStripMenuItem();
            this.miNewReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.miNewVarSet = new System.Windows.Forms.ToolStripMenuItem();
            this.miNewVarGet = new System.Windows.Forms.ToolStripMenuItem();
            this.newSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNewProject = new System.Windows.Forms.ToolStripButton();
            this.btnOpenProject = new System.Windows.Forms.ToolStripButton();
            this.btnSaveProject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMoveDown = new System.Windows.Forms.ToolStripButton();
            this.btnMoveUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCut = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnPaste = new System.Windows.Forms.ToolStripButton();
            this.btnClone = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNewText = new System.Windows.Forms.ToolStripButton();
            this.btnNewLoad = new System.Windows.Forms.ToolStripButton();
            this.btnNextPage = new System.Windows.Forms.ToolStripButton();
            this.btnDownload = new System.Windows.Forms.ToolStripButton();
            this.btnNewSearch = new System.Windows.Forms.ToolStripButton();
            this.btnNewReplace = new System.Windows.Forms.ToolStripButton();
            this.btnNewSetVar = new System.Windows.Forms.ToolStripButton();
            this.btnNewGetVar = new System.Windows.Forms.ToolStripButton();
            this.btnNewSave = new System.Windows.Forms.ToolStripButton();
            this.btnNewCookie = new System.Windows.Forms.ToolStripButton();
            this.btnNewDate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRun = new System.Windows.Forms.ToolStripButton();
            this.btnRunBranch = new System.Windows.Forms.ToolStripButton();
            this.btnRunNode = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeScript = new System.Windows.Forms.TreeView();
            this.ilActions = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panBox = new System.Windows.Forms.Panel();
            this.props = new System.Windows.Forms.PropertyGrid();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabSour = new System.Windows.Forms.TabPage();
            this.tboxSour = new System.Windows.Forms.TextBox();
            this.tabDest = new System.Windows.Forms.TabPage();
            this.tboxDest = new System.Windows.Forms.TextBox();
            this.tabBack = new System.Windows.Forms.TabPage();
            this.tboxBack = new System.Windows.Forms.TextBox();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.newCookieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBar.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panBox.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tabSour.SuspendLayout();
            this.tabDest.SuspendLayout();
            this.tabBack.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slabTime});
            this.statusBar.Location = new System.Drawing.Point(0, 345);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(852, 22);
            this.statusBar.TabIndex = 0;
            // 
            // slabTime
            // 
            this.slabTime.Name = "slabTime";
            this.slabTime.Size = new System.Drawing.Size(89, 17);
            this.slabTime.Text = "Time: 0 min 0 sec";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programmToolStripMenuItem,
            this.actionToolStripMenuItem,
            this.miAbout});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(852, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // programmToolStripMenuItem
            // 
            this.programmToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNewProject,
            this.miOpenProject,
            this.miSaveProject,
            this.miSaveAsProject,
            this.toolStripMenuItem1,
            this.miExit});
            this.programmToolStripMenuItem.Name = "programmToolStripMenuItem";
            this.programmToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.programmToolStripMenuItem.Text = "Project";
            // 
            // miNewProject
            // 
            this.miNewProject.Name = "miNewProject";
            this.miNewProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.miNewProject.Size = new System.Drawing.Size(151, 22);
            this.miNewProject.Text = "New";
            this.miNewProject.Click += new System.EventHandler(this.btnNewProject_Click);
            // 
            // miOpenProject
            // 
            this.miOpenProject.Name = "miOpenProject";
            this.miOpenProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.miOpenProject.Size = new System.Drawing.Size(151, 22);
            this.miOpenProject.Text = "Open";
            this.miOpenProject.Click += new System.EventHandler(this.btnOpenProject_Click);
            // 
            // miSaveProject
            // 
            this.miSaveProject.Name = "miSaveProject";
            this.miSaveProject.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.miSaveProject.Size = new System.Drawing.Size(151, 22);
            this.miSaveProject.Text = "Save";
            this.miSaveProject.Click += new System.EventHandler(this.btnSaveProject_Click);
            // 
            // miSaveAsProject
            // 
            this.miSaveAsProject.Name = "miSaveAsProject";
            this.miSaveAsProject.Size = new System.Drawing.Size(151, 22);
            this.miSaveAsProject.Text = "Save as";
            this.miSaveAsProject.Click += new System.EventHandler(this.miSaveAsProject_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(148, 6);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(151, 22);
            this.miExit.Text = "Exit";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // actionToolStripMenuItem
            // 
            this.actionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miRun,
            this.miRunAt,
            this.toolStripMenuItem2,
            this.miNewText,
            this.miNewLoad,
            this.miNewFind,
            this.miNewReplace,
            this.miNewVarSet,
            this.miNewVarGet,
            this.newSaveToolStripMenuItem,
            this.newCookieToolStripMenuItem,
            this.newDateToolStripMenuItem});
            this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
            this.actionToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.actionToolStripMenuItem.Text = "Action";
            // 
            // miRun
            // 
            this.miRun.Name = "miRun";
            this.miRun.Size = new System.Drawing.Size(180, 22);
            this.miRun.Text = "Run";
            this.miRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // miRunAt
            // 
            this.miRunAt.Name = "miRunAt";
            this.miRunAt.Size = new System.Drawing.Size(180, 22);
            this.miRunAt.Text = "Run at";
            this.miRunAt.Click += new System.EventHandler(this.btnRunBranch_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // miNewText
            // 
            this.miNewText.Name = "miNewText";
            this.miNewText.Size = new System.Drawing.Size(180, 22);
            this.miNewText.Text = "New text";
            this.miNewText.Click += new System.EventHandler(this.btnNewText_Click);
            // 
            // miNewLoad
            // 
            this.miNewLoad.Name = "miNewLoad";
            this.miNewLoad.Size = new System.Drawing.Size(180, 22);
            this.miNewLoad.Text = "New loading";
            this.miNewLoad.Click += new System.EventHandler(this.btnNewLoad_Click);
            // 
            // miNewFind
            // 
            this.miNewFind.Name = "miNewFind";
            this.miNewFind.Size = new System.Drawing.Size(180, 22);
            this.miNewFind.Text = "New finding";
            this.miNewFind.Click += new System.EventHandler(this.btnNewSearch_Click);
            // 
            // miNewReplace
            // 
            this.miNewReplace.Name = "miNewReplace";
            this.miNewReplace.Size = new System.Drawing.Size(180, 22);
            this.miNewReplace.Text = "New replacing";
            this.miNewReplace.Click += new System.EventHandler(this.btnNewReplace_Click);
            // 
            // miNewVarSet
            // 
            this.miNewVarSet.Name = "miNewVarSet";
            this.miNewVarSet.Size = new System.Drawing.Size(180, 22);
            this.miNewVarSet.Text = "New variable setter";
            this.miNewVarSet.Click += new System.EventHandler(this.btnNewSetVar_Click);
            // 
            // miNewVarGet
            // 
            this.miNewVarGet.Name = "miNewVarGet";
            this.miNewVarGet.Size = new System.Drawing.Size(180, 22);
            this.miNewVarGet.Text = "New variable getter";
            this.miNewVarGet.Click += new System.EventHandler(this.btnNewGetVar_Click);
            // 
            // newSaveToolStripMenuItem
            // 
            this.newSaveToolStripMenuItem.Name = "newSaveToolStripMenuItem";
            this.newSaveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newSaveToolStripMenuItem.Text = "New save";
            this.newSaveToolStripMenuItem.Click += new System.EventHandler(this.btnNewSave_Click);
            // 
            // newDateToolStripMenuItem
            // 
            this.newDateToolStripMenuItem.Name = "newDateToolStripMenuItem";
            this.newDateToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newDateToolStripMenuItem.Text = "New date";
            this.newDateToolStripMenuItem.Click += new System.EventHandler(this.btnNewDate_Click);
            // 
            // miAbout
            // 
            this.miAbout.Name = "miAbout";
            this.miAbout.Size = new System.Drawing.Size(48, 20);
            this.miAbout.Text = "About";
            this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewProject,
            this.btnOpenProject,
            this.btnSaveProject,
            this.toolStripSeparator4,
            this.btnMoveDown,
            this.btnMoveUp,
            this.toolStripSeparator2,
            this.btnCut,
            this.btnCopy,
            this.btnPaste,
            this.btnClone,
            this.toolStripSeparator1,
            this.btnDelete,
            this.toolStripSeparator5,
            this.btnNewText,
            this.btnNewLoad,
            this.btnNextPage,
            this.btnDownload,
            this.btnNewSearch,
            this.btnNewReplace,
            this.btnNewSetVar,
            this.btnNewGetVar,
            this.btnNewSave,
            this.btnNewCookie,
            this.btnNewDate,
            this.toolStripSeparator3,
            this.btnRun,
            this.btnRunBranch,
            this.btnRunNode});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(8, 0, 1, 0);
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(852, 31);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNewProject
            // 
            this.btnNewProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewProject.Image = ((System.Drawing.Image)(resources.GetObject("btnNewProject.Image")));
            this.btnNewProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewProject.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnNewProject.Name = "btnNewProject";
            this.btnNewProject.Size = new System.Drawing.Size(28, 28);
            this.btnNewProject.Text = "New project";
            this.btnNewProject.Click += new System.EventHandler(this.btnNewProject_Click);
            // 
            // btnOpenProject
            // 
            this.btnOpenProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpenProject.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenProject.Image")));
            this.btnOpenProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenProject.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnOpenProject.Name = "btnOpenProject";
            this.btnOpenProject.Size = new System.Drawing.Size(28, 28);
            this.btnOpenProject.Text = "toolStripButton9";
            this.btnOpenProject.ToolTipText = "Open project";
            this.btnOpenProject.Click += new System.EventHandler(this.btnOpenProject_Click);
            // 
            // btnSaveProject
            // 
            this.btnSaveProject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveProject.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveProject.Image")));
            this.btnSaveProject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveProject.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnSaveProject.Name = "btnSaveProject";
            this.btnSaveProject.Size = new System.Drawing.Size(28, 28);
            this.btnSaveProject.Text = "toolStripButton10";
            this.btnSaveProject.ToolTipText = "Save project";
            this.btnSaveProject.Click += new System.EventHandler(this.btnSaveProject_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 28);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveDown.Image")));
            this.btnMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveDown.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(28, 28);
            this.btnMoveDown.Text = "toolStripButton2";
            this.btnMoveDown.ToolTipText = "Move slected action down";
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveUp.Image")));
            this.btnMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveUp.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(28, 28);
            this.btnMoveUp.Text = "toolStripButton2";
            this.btnMoveUp.ToolTipText = "Move slected action up";
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // btnCut
            // 
            this.btnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCut.Image = ((System.Drawing.Image)(resources.GetObject("btnCut.Image")));
            this.btnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(28, 28);
            this.btnCut.Text = "Cut selected";
            this.btnCut.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(28, 28);
            this.btnCopy.Text = "Copy selected";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPaste.Image = ((System.Drawing.Image)(resources.GetObject("btnPaste.Image")));
            this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(28, 28);
            this.btnPaste.Text = "Paste selected";
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnClone
            // 
            this.btnClone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClone.Image = ((System.Drawing.Image)(resources.GetObject("btnClone.Image")));
            this.btnClone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClone.Name = "btnClone";
            this.btnClone.Size = new System.Drawing.Size(28, 28);
            this.btnClone.Text = "toolStripButton17";
            this.btnClone.ToolTipText = "Clone selected action";
            this.btnClone.Click += new System.EventHandler(this.btnClone_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(28, 28);
            this.btnDelete.Text = "toolStripButton11";
            this.btnDelete.ToolTipText = "Delete selected action";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 28);
            // 
            // btnNewText
            // 
            this.btnNewText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewText.Image = ((System.Drawing.Image)(resources.GetObject("btnNewText.Image")));
            this.btnNewText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewText.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnNewText.Name = "btnNewText";
            this.btnNewText.Size = new System.Drawing.Size(28, 28);
            this.btnNewText.Text = "toolStripButton5";
            this.btnNewText.ToolTipText = "New text action";
            this.btnNewText.Click += new System.EventHandler(this.btnNewText_Click);
            // 
            // btnNewLoad
            // 
            this.btnNewLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnNewLoad.Image")));
            this.btnNewLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewLoad.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnNewLoad.Name = "btnNewLoad";
            this.btnNewLoad.Size = new System.Drawing.Size(28, 28);
            this.btnNewLoad.Text = "toolStripButton2";
            this.btnNewLoad.ToolTipText = "New load action";
            this.btnNewLoad.Click += new System.EventHandler(this.btnNewLoad_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNextPage.Image = ((System.Drawing.Image)(resources.GetObject("btnNextPage.Image")));
            this.btnNextPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(28, 28);
            this.btnNextPage.Text = "toolStripButton1";
            this.btnNextPage.ToolTipText = "New next page action";
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDownload.Image = ((System.Drawing.Image)(resources.GetObject("btnDownload.Image")));
            this.btnDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(28, 28);
            this.btnDownload.ToolTipText = "New binary load action";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnNewSearch
            // 
            this.btnNewSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnNewSearch.Image")));
            this.btnNewSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewSearch.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnNewSearch.Name = "btnNewSearch";
            this.btnNewSearch.Size = new System.Drawing.Size(28, 28);
            this.btnNewSearch.Text = "toolStripButton3";
            this.btnNewSearch.ToolTipText = "New search action";
            this.btnNewSearch.Click += new System.EventHandler(this.btnNewSearch_Click);
            // 
            // btnNewReplace
            // 
            this.btnNewReplace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewReplace.Image = ((System.Drawing.Image)(resources.GetObject("btnNewReplace.Image")));
            this.btnNewReplace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewReplace.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnNewReplace.Name = "btnNewReplace";
            this.btnNewReplace.Size = new System.Drawing.Size(28, 28);
            this.btnNewReplace.Text = "toolStripButton4";
            this.btnNewReplace.ToolTipText = "New replace action";
            this.btnNewReplace.Click += new System.EventHandler(this.btnNewReplace_Click);
            // 
            // btnNewSetVar
            // 
            this.btnNewSetVar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewSetVar.Image = ((System.Drawing.Image)(resources.GetObject("btnNewSetVar.Image")));
            this.btnNewSetVar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewSetVar.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnNewSetVar.Name = "btnNewSetVar";
            this.btnNewSetVar.Size = new System.Drawing.Size(28, 28);
            this.btnNewSetVar.Text = "toolStripButton6";
            this.btnNewSetVar.ToolTipText = "New variable setter";
            this.btnNewSetVar.Click += new System.EventHandler(this.btnNewSetVar_Click);
            // 
            // btnNewGetVar
            // 
            this.btnNewGetVar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewGetVar.Image = ((System.Drawing.Image)(resources.GetObject("btnNewGetVar.Image")));
            this.btnNewGetVar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewGetVar.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnNewGetVar.Name = "btnNewGetVar";
            this.btnNewGetVar.Size = new System.Drawing.Size(28, 28);
            this.btnNewGetVar.Text = "toolStripButton7";
            this.btnNewGetVar.ToolTipText = "New variable getter";
            this.btnNewGetVar.Click += new System.EventHandler(this.btnNewGetVar_Click);
            // 
            // btnNewSave
            // 
            this.btnNewSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewSave.Image = ((System.Drawing.Image)(resources.GetObject("btnNewSave.Image")));
            this.btnNewSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewSave.Name = "btnNewSave";
            this.btnNewSave.Size = new System.Drawing.Size(28, 28);
            this.btnNewSave.Text = "toolStripButton1";
            this.btnNewSave.ToolTipText = "New save action";
            this.btnNewSave.Click += new System.EventHandler(this.btnNewSave_Click);
            // 
            // btnNewCookie
            // 
            this.btnNewCookie.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewCookie.Image = ((System.Drawing.Image)(resources.GetObject("btnNewCookie.Image")));
            this.btnNewCookie.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewCookie.Name = "btnNewCookie";
            this.btnNewCookie.Size = new System.Drawing.Size(28, 28);
            this.btnNewCookie.Text = "New Cookie";
            this.btnNewCookie.ToolTipText = "New Cookie Action";
            this.btnNewCookie.Click += new System.EventHandler(this.btnNewCookie_Click);
            // 
            // btnNewDate
            // 
            this.btnNewDate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewDate.Image = ((System.Drawing.Image)(resources.GetObject("btnNewDate.Image")));
            this.btnNewDate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewDate.Name = "btnNewDate";
            this.btnNewDate.Size = new System.Drawing.Size(28, 28);
            this.btnNewDate.Text = "toolStripButton1";
            this.btnNewDate.ToolTipText = "New date action";
            this.btnNewDate.Click += new System.EventHandler(this.btnNewDate_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 28);
            // 
            // btnRun
            // 
            this.btnRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRun.Image = ((System.Drawing.Image)(resources.GetObject("btnRun.Image")));
            this.btnRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRun.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(28, 28);
            this.btnRun.Text = "toolStripButton12";
            this.btnRun.ToolTipText = "Run project";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnRunBranch
            // 
            this.btnRunBranch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRunBranch.Image = ((System.Drawing.Image)(resources.GetObject("btnRunBranch.Image")));
            this.btnRunBranch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRunBranch.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnRunBranch.Name = "btnRunBranch";
            this.btnRunBranch.Size = new System.Drawing.Size(28, 28);
            this.btnRunBranch.Text = "toolStripButton13";
            this.btnRunBranch.ToolTipText = "Execute selected action with it\'s childs";
            this.btnRunBranch.Click += new System.EventHandler(this.btnRunBranch_Click);
            // 
            // btnRunNode
            // 
            this.btnRunNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRunNode.Image = ((System.Drawing.Image)(resources.GetObject("btnRunNode.Image")));
            this.btnRunNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRunNode.Margin = new System.Windows.Forms.Padding(2, 1, 2, 2);
            this.btnRunNode.Name = "btnRunNode";
            this.btnRunNode.Size = new System.Drawing.Size(28, 28);
            this.btnRunNode.ToolTipText = "Execute selected action";
            this.btnRunNode.Click += new System.EventHandler(this.btnRunNode_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 55);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeScript);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(852, 290);
            this.splitContainer1.SplitterDistance = 282;
            this.splitContainer1.TabIndex = 3;
            // 
            // treeScript
            // 
            this.treeScript.AllowDrop = true;
            this.treeScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeScript.HideSelection = false;
            this.treeScript.ImageIndex = 0;
            this.treeScript.ImageList = this.ilActions;
            this.treeScript.Location = new System.Drawing.Point(0, 0);
            this.treeScript.Name = "treeScript";
            this.treeScript.SelectedImageIndex = 0;
            this.treeScript.ShowNodeToolTips = true;
            this.treeScript.Size = new System.Drawing.Size(282, 290);
            this.treeScript.TabIndex = 0;
            this.treeScript.DragLeave += new System.EventHandler(this.treeScript_DragLeave);
            this.treeScript.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeScript_DragDrop);
            this.treeScript.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeScript_AfterSelect);
            this.treeScript.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeScript_DragEnter);
            this.treeScript.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeScript_ItemDrag);
            this.treeScript.DragOver += new System.Windows.Forms.DragEventHandler(this.treeScript_DragOver);
            // 
            // ilActions
            // 
            this.ilActions.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilActions.ImageStream")));
            this.ilActions.TransparentColor = System.Drawing.Color.Transparent;
            this.ilActions.Images.SetKeyName(0, "document.png");
            this.ilActions.Images.SetKeyName(1, "document_down.png");
            this.ilActions.Images.SetKeyName(2, "view.gif");
            this.ilActions.Images.SetKeyName(3, "replace.gif");
            this.ilActions.Images.SetKeyName(4, "set-var.gif");
            this.ilActions.Images.SetKeyName(5, "get-var.gif");
            this.ilActions.Images.SetKeyName(6, "disk_yellow.png");
            this.ilActions.Images.SetKeyName(7, "next16.png");
            this.ilActions.Images.SetKeyName(8, "import16.png");
            this.ilActions.Images.SetKeyName(9, "calendar_16.gif");
            this.ilActions.Images.SetKeyName(10, "cookie-16.png");
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panBox);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabs);
            this.splitContainer2.Size = new System.Drawing.Size(566, 290);
            this.splitContainer2.SplitterDistance = 138;
            this.splitContainer2.TabIndex = 0;
            // 
            // panBox
            // 
            this.panBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panBox.Controls.Add(this.props);
            this.panBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panBox.Location = new System.Drawing.Point(0, 0);
            this.panBox.Name = "panBox";
            this.panBox.Size = new System.Drawing.Size(566, 138);
            this.panBox.TabIndex = 0;
            // 
            // props
            // 
            this.props.Dock = System.Windows.Forms.DockStyle.Fill;
            this.props.HelpVisible = false;
            this.props.LargeButtons = true;
            this.props.Location = new System.Drawing.Point(0, 0);
            this.props.Margin = new System.Windows.Forms.Padding(0, 1, 4, 2);
            this.props.Name = "props";
            this.props.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.props.Size = new System.Drawing.Size(566, 138);
            this.props.TabIndex = 0;
            this.props.ToolbarVisible = false;
            this.props.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.props_PropertyValueChanged);
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabSour);
            this.tabs.Controls.Add(this.tabDest);
            this.tabs.Controls.Add(this.tabBack);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(566, 148);
            this.tabs.TabIndex = 0;
            // 
            // tabSour
            // 
            this.tabSour.Controls.Add(this.tboxSour);
            this.tabSour.Location = new System.Drawing.Point(4, 22);
            this.tabSour.Name = "tabSour";
            this.tabSour.Padding = new System.Windows.Forms.Padding(3);
            this.tabSour.Size = new System.Drawing.Size(558, 122);
            this.tabSour.TabIndex = 0;
            this.tabSour.Text = "Input";
            this.tabSour.UseVisualStyleBackColor = true;
            // 
            // tboxSour
            // 
            this.tboxSour.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tboxSour.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tboxSour.Location = new System.Drawing.Point(3, 3);
            this.tboxSour.Multiline = true;
            this.tboxSour.Name = "tboxSour";
            this.tboxSour.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tboxSour.Size = new System.Drawing.Size(552, 116);
            this.tboxSour.TabIndex = 0;
            // 
            // tabDest
            // 
            this.tabDest.Controls.Add(this.tboxDest);
            this.tabDest.Location = new System.Drawing.Point(4, 22);
            this.tabDest.Name = "tabDest";
            this.tabDest.Padding = new System.Windows.Forms.Padding(3);
            this.tabDest.Size = new System.Drawing.Size(558, 122);
            this.tabDest.TabIndex = 1;
            this.tabDest.Text = "Output";
            this.tabDest.UseVisualStyleBackColor = true;
            // 
            // tboxDest
            // 
            this.tboxDest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tboxDest.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tboxDest.Location = new System.Drawing.Point(3, 3);
            this.tboxDest.Multiline = true;
            this.tboxDest.Name = "tboxDest";
            this.tboxDest.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tboxDest.Size = new System.Drawing.Size(552, 116);
            this.tboxDest.TabIndex = 1;
            this.tboxDest.WordWrap = false;
            // 
            // tabBack
            // 
            this.tabBack.Controls.Add(this.tboxBack);
            this.tabBack.Location = new System.Drawing.Point(4, 22);
            this.tabBack.Name = "tabBack";
            this.tabBack.Padding = new System.Windows.Forms.Padding(3);
            this.tabBack.Size = new System.Drawing.Size(558, 122);
            this.tabBack.TabIndex = 2;
            this.tabBack.Text = "Result";
            this.tabBack.UseVisualStyleBackColor = true;
            // 
            // tboxBack
            // 
            this.tboxBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tboxBack.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tboxBack.Location = new System.Drawing.Point(3, 3);
            this.tboxBack.Multiline = true;
            this.tboxBack.Name = "tboxBack";
            this.tboxBack.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tboxBack.Size = new System.Drawing.Size(552, 116);
            this.tboxBack.TabIndex = 2;
            this.tboxBack.WordWrap = false;
            // 
            // dlgOpen
            // 
            this.dlgOpen.DefaultExt = "dsx";
            this.dlgOpen.Filter = "Script files|*.dsx|Xml files|*.xml|All files|*.*";
            // 
            // dlgSave
            // 
            this.dlgSave.DefaultExt = "dsx";
            this.dlgSave.Filter = "Script files|*.dsx|Xml files|*.xml|All files|*.*";
            // 
            // newCookieToolStripMenuItem
            // 
            this.newCookieToolStripMenuItem.Name = "newCookieToolStripMenuItem";
            this.newCookieToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newCookieToolStripMenuItem.Text = "New cookie";
            this.newCookieToolStripMenuItem.Click += new System.EventHandler(this.btnNewCookie_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 367);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "DSX Editor";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.panBox.ResumeLayout(false);
            this.tabs.ResumeLayout(false);
            this.tabSour.ResumeLayout(false);
            this.tabSour.PerformLayout();
            this.tabDest.ResumeLayout(false);
            this.tabDest.PerformLayout();
            this.tabBack.ResumeLayout(false);
            this.tabBack.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem programmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miNewProject;
        private System.Windows.Forms.ToolStripMenuItem miOpenProject;
        private System.Windows.Forms.ToolStripMenuItem miSaveProject;
        private System.Windows.Forms.ToolStripMenuItem miSaveAsProject;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miRun;
        private System.Windows.Forms.ToolStripMenuItem miRunAt;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem miNewLoad;
        private System.Windows.Forms.ToolStripMenuItem miNewFind;
        private System.Windows.Forms.ToolStripMenuItem miNewReplace;
        private System.Windows.Forms.ToolStripMenuItem miNewText;
        private System.Windows.Forms.ToolStripMenuItem miNewVarSet;
        private System.Windows.Forms.ToolStripMenuItem miNewVarGet;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeScript;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panBox;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabSour;
        private System.Windows.Forms.TabPage tabDest;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.TextBox tboxSour;
        private System.Windows.Forms.TextBox tboxDest;
        private System.Windows.Forms.PropertyGrid props;
        private System.Windows.Forms.ToolStripButton btnNewLoad;
        private System.Windows.Forms.ToolStripButton btnNewSearch;
        private System.Windows.Forms.ToolStripButton btnNewReplace;
        private System.Windows.Forms.ToolStripButton btnNewText;
        private System.Windows.Forms.ToolStripButton btnNewSetVar;
        private System.Windows.Forms.ToolStripButton btnNewGetVar;
        private System.Windows.Forms.ToolStripButton btnNewProject;
        private System.Windows.Forms.ToolStripButton btnOpenProject;
        private System.Windows.Forms.ToolStripButton btnSaveProject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnRun;
        private System.Windows.Forms.ToolStripButton btnRunBranch;
        private System.Windows.Forms.ToolStripButton btnRunNode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnMoveUp;
        private System.Windows.Forms.ToolStripButton btnMoveDown;
        private System.Windows.Forms.ImageList ilActions;
        private System.Windows.Forms.ToolStripButton btnClone;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.TabPage tabBack;
        private System.Windows.Forms.TextBox tboxBack;
        private System.Windows.Forms.ToolStripButton btnNewSave;
        private System.Windows.Forms.ToolStripButton btnNextPage;
        private System.Windows.Forms.ToolStripStatusLabel slabTime;
        private System.Windows.Forms.ToolStripMenuItem miAbout;
        private System.Windows.Forms.ToolStripButton btnDownload;
        private System.Windows.Forms.ToolStripButton btnCut;
        private System.Windows.Forms.ToolStripButton btnCopy;
        private System.Windows.Forms.ToolStripButton btnPaste;
        private System.Windows.Forms.ToolStripButton btnNewDate;
        private System.Windows.Forms.ToolStripMenuItem newSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newDateToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnNewCookie;
        private System.Windows.Forms.ToolStripMenuItem newCookieToolStripMenuItem;
    }
}

