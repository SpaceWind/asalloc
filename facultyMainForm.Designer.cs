namespace ASAlloc
{
    partial class facultyMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(facultyMainForm));
            this.tabCtrlFaculty = new System.Windows.Forms.TabControl();
            this.tabStudents = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.studentListComboBox = new System.Windows.Forms.ComboBox();
            this.treeStudents = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.plannedInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.plannedOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offenseButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.currentStudentList = new System.Windows.Forms.DataGridView();
            this.tabPlaceFund = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.treePlaces = new System.Windows.Forms.TreeView();
            this.placeComboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.currentPlaceList = new System.Windows.Forms.DataGridView();
            this.tabMessages = new System.Windows.Forms.TabPage();
            this.contxtTabMenu1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.редактироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteStudent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tabCtrlFaculty.SuspendLayout();
            this.tabStudents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentStudentList)).BeginInit();
            this.tabPlaceFund.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentPlaceList)).BeginInit();
            this.contxtTabMenu1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCtrlFaculty
            // 
            this.tabCtrlFaculty.AccessibleName = "tabCtrlFaculty";
            this.tabCtrlFaculty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCtrlFaculty.Controls.Add(this.tabStudents);
            this.tabCtrlFaculty.Controls.Add(this.tabPlaceFund);
            this.tabCtrlFaculty.Controls.Add(this.tabMessages);
            this.tabCtrlFaculty.Location = new System.Drawing.Point(2, 3);
            this.tabCtrlFaculty.Name = "tabCtrlFaculty";
            this.tabCtrlFaculty.SelectedIndex = 0;
            this.tabCtrlFaculty.Size = new System.Drawing.Size(1199, 576);
            this.tabCtrlFaculty.TabIndex = 0;
            this.tabCtrlFaculty.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabCtrlFaculty_DrawItem);
            // 
            // tabStudents
            // 
            this.tabStudents.AccessibleName = "tabStudents";
            this.tabStudents.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabStudents.Controls.Add(this.splitContainer1);
            this.tabStudents.Location = new System.Drawing.Point(4, 22);
            this.tabStudents.Name = "tabStudents";
            this.tabStudents.Padding = new System.Windows.Forms.Padding(3);
            this.tabStudents.Size = new System.Drawing.Size(1191, 550);
            this.tabStudents.TabIndex = 0;
            this.tabStudents.Text = " Списки студентов  ";
            this.tabStudents.Enter += new System.EventHandler(this.tabStudents_Enter);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(-4, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AccessibleName = "studentListLeftPnel";
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AccessibleName = "studentListRightPanel";
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(1195, 554);
            this.splitContainer1.SplitterDistance = 186;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.studentListComboBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.treeStudents, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(184, 550);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // studentListComboBox
            // 
            this.studentListComboBox.AccessibleName = "";
            this.studentListComboBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.studentListComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.studentListComboBox.FormattingEnabled = true;
            this.studentListComboBox.Items.AddRange(new object[] {
            "Студенты",
            "Списки студентов",
            "Приказы"});
            this.studentListComboBox.Location = new System.Drawing.Point(3, 3);
            this.studentListComboBox.Name = "studentListComboBox";
            this.studentListComboBox.Size = new System.Drawing.Size(178, 21);
            this.studentListComboBox.TabIndex = 0;
            this.studentListComboBox.TextChanged += new System.EventHandler(this.tabStudents_Enter);
            // 
            // treeStudents
            // 
            this.treeStudents.AccessibleName = "treeStudents";
            this.treeStudents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeStudents.Location = new System.Drawing.Point(3, 28);
            this.treeStudents.Name = "treeStudents";
            this.treeStudents.Size = new System.Drawing.Size(178, 519);
            this.treeStudents.TabIndex = 1;
            this.treeStudents.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeStudents_NodeMouseClick);
            this.treeStudents.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeStudents_NodeMouseDoubleClick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.toolStrip1);
            this.tableLayoutPanel2.Controls.Add(this.tabControl1);
            this.tableLayoutPanel2.Controls.Add(this.currentStudentList);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(-1, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1087, 550);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.toolStripButton3,
            this.toolStripButton6,
            this.toolStripButton4,
            this.toolStripButton7,
            this.toolStripButton5,
            this.toolStripButton8,
            this.toolStripSeparator2,
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2,
            this.offenseButton,
            this.toolStripButton10});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1087, 35);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::ASAlloc.Properties.Resources.list_add_user;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(28, 32);
            this.toolStripButton2.Text = "Добавить студента";
            this.toolStripButton2.Click += new System.EventHandler(this.addStudentButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripSeparator1.Size = new System.Drawing.Size(20, 35);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::ASAlloc.Properties.Resources.price_list_icon_76;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(28, 32);
            this.toolStripButton3.Text = "Создать новый список студентов";
            this.toolStripButton3.Click += new System.EventHandler(this.addNewListButton_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = global::ASAlloc.Properties.Resources.top_authors_list;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(28, 32);
            this.toolStripButton6.Text = "Добавить студентов в список";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::ASAlloc.Properties.Resources.System_Save_as_icon;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(28, 32);
            this.toolStripButton4.Text = "Сохранить приватный список как";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Image = global::ASAlloc.Properties.Resources.save__1_;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(28, 32);
            this.toolStripButton7.Text = "Сохранить приватный список";
            this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Enabled = false;
            this.toolStripButton5.Image = global::ASAlloc.Properties.Resources.close_2;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(28, 32);
            this.toolStripButton5.Text = "Удалить список";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Image = global::ASAlloc.Properties.Resources.rename;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(28, 32);
            this.toolStripButton8.Text = "Переименовать список";
            this.toolStripButton8.Click += new System.EventHandler(this.toolStripButton8_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.AutoSize = false;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(20, 35);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plannedInToolStripMenuItem,
            this.inToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::ASAlloc.Properties.Resources.home;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(37, 32);
            this.toolStripDropDownButton1.Text = "Заселение";
            // 
            // plannedInToolStripMenuItem
            // 
            this.plannedInToolStripMenuItem.Name = "plannedInToolStripMenuItem";
            this.plannedInToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.plannedInToolStripMenuItem.Text = "Плановое заселение";
            this.plannedInToolStripMenuItem.Click += new System.EventHandler(this.plannedInToolStripMenuItem_Click);
            // 
            // inToolStripMenuItem
            // 
            this.inToolStripMenuItem.Name = "inToolStripMenuItem";
            this.inToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.inToolStripMenuItem.Text = "Внеплановое заселение";
            this.inToolStripMenuItem.Click += new System.EventHandler(this.inToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plannedOutToolStripMenuItem,
            this.outToolStripMenuItem});
            this.toolStripDropDownButton2.Image = global::ASAlloc.Properties.Resources.exile;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(37, 32);
            this.toolStripDropDownButton2.Text = "Выселение";
            // 
            // plannedOutToolStripMenuItem
            // 
            this.plannedOutToolStripMenuItem.Name = "plannedOutToolStripMenuItem";
            this.plannedOutToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.plannedOutToolStripMenuItem.Text = "Плановое выселение";
            this.plannedOutToolStripMenuItem.Click += new System.EventHandler(this.plannedOutToolStripMenuItem_Click);
            // 
            // outToolStripMenuItem
            // 
            this.outToolStripMenuItem.Name = "outToolStripMenuItem";
            this.outToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.outToolStripMenuItem.Text = "Внеплановое выселение";
            this.outToolStripMenuItem.Click += new System.EventHandler(this.outToolStripMenuItem_Click);
            // 
            // offenseButton
            // 
            this.offenseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.offenseButton.Image = global::ASAlloc.Properties.Resources.violation;
            this.offenseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.offenseButton.Name = "offenseButton";
            this.offenseButton.Size = new System.Drawing.Size(28, 32);
            this.offenseButton.Text = "Добавить нарушение";
            this.offenseButton.Click += new System.EventHandler(this.offenseButton_Click);
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton10.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton10.Image")));
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(28, 32);
            this.toolStripButton10.Text = "Опубликовать";
            this.toolStripButton10.Click += new System.EventHandler(this.toolStripButton10_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.ItemSize = new System.Drawing.Size(40, 18);
            this.tabControl1.Location = new System.Drawing.Point(3, 38);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(11, 3);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1081, 19);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabCtrlFaculty_DrawItem);
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
            // 
            // currentStudentList
            // 
            this.currentStudentList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currentStudentList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.currentStudentList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.currentStudentList.Location = new System.Drawing.Point(3, 63);
            this.currentStudentList.Name = "currentStudentList";
            this.currentStudentList.ReadOnly = true;
            this.currentStudentList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.currentStudentList.Size = new System.Drawing.Size(1081, 484);
            this.currentStudentList.TabIndex = 2;
            this.currentStudentList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.currentStudentList_CellMouseRClick);
            // 
            // tabPlaceFund
            // 
            this.tabPlaceFund.AccessibleName = "tabPlaceFund";
            this.tabPlaceFund.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPlaceFund.Controls.Add(this.splitContainer2);
            this.tabPlaceFund.Location = new System.Drawing.Point(4, 22);
            this.tabPlaceFund.Name = "tabPlaceFund";
            this.tabPlaceFund.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlaceFund.Size = new System.Drawing.Size(1191, 550);
            this.tabPlaceFund.TabIndex = 1;
            this.tabPlaceFund.Text = "Жилой фонд";
            this.tabPlaceFund.Enter += new System.EventHandler(this.tabPlaceFund_Enter);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(-4, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tableLayoutPanel4);
            this.splitContainer2.Size = new System.Drawing.Size(1195, 550);
            this.splitContainer2.SplitterDistance = 244;
            this.splitContainer2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.treePlaces, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.placeComboBox, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(242, 550);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // treePlaces
            // 
            this.treePlaces.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treePlaces.Location = new System.Drawing.Point(3, 28);
            this.treePlaces.Name = "treePlaces";
            this.treePlaces.Size = new System.Drawing.Size(236, 519);
            this.treePlaces.TabIndex = 1;
            this.treePlaces.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treePlaces_NodeMouseDoubleClick);
            // 
            // placeComboBox
            // 
            this.placeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.placeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.placeComboBox.FormattingEnabled = true;
            this.placeComboBox.Items.AddRange(new object[] {
            "Свободные",
            "Доступные",
            "Все"});
            this.placeComboBox.Location = new System.Drawing.Point(3, 3);
            this.placeComboBox.Name = "placeComboBox";
            this.placeComboBox.Size = new System.Drawing.Size(236, 21);
            this.placeComboBox.TabIndex = 0;
            this.placeComboBox.TextChanged += new System.EventHandler(this.tabPlaceFund_Enter);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.toolStrip2, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tabControl2, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.currentPlaceList, 0, 2);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(947, 550);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(947, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl2.Location = new System.Drawing.Point(3, 28);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(941, 19);
            this.tabControl2.TabIndex = 1;
            this.tabControl2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabCtrlFaculty_DrawItem);
            this.tabControl2.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl2_Selecting);
            this.tabControl2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
            // 
            // currentPlaceList
            // 
            this.currentPlaceList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currentPlaceList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.currentPlaceList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.currentPlaceList.Location = new System.Drawing.Point(3, 53);
            this.currentPlaceList.Name = "currentPlaceList";
            this.currentPlaceList.ReadOnly = true;
            this.currentPlaceList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.currentPlaceList.Size = new System.Drawing.Size(941, 494);
            this.currentPlaceList.TabIndex = 2;
            // 
            // tabMessages
            // 
            this.tabMessages.AccessibleName = "tabMessages";
            this.tabMessages.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabMessages.Location = new System.Drawing.Point(4, 22);
            this.tabMessages.Name = "tabMessages";
            this.tabMessages.Padding = new System.Windows.Forms.Padding(3);
            this.tabMessages.Size = new System.Drawing.Size(1191, 550);
            this.tabMessages.TabIndex = 2;
            this.tabMessages.Text = "Заявки";
            // 
            // contxtTabMenu1
            // 
            this.contxtTabMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.редактироватьToolStripMenuItem,
            this.deleteStudent});
            this.contxtTabMenu1.Name = "contxtTabMenu1";
            this.contxtTabMenu1.Size = new System.Drawing.Size(159, 48);
            // 
            // редактироватьToolStripMenuItem
            // 
            this.редактироватьToolStripMenuItem.Name = "редактироватьToolStripMenuItem";
            this.редактироватьToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.редактироватьToolStripMenuItem.Text = "Редактировать";
            // 
            // deleteStudent
            // 
            this.deleteStudent.Image = global::ASAlloc.Properties.Resources.close_2;
            this.deleteStudent.Name = "deleteStudent";
            this.deleteStudent.Size = new System.Drawing.Size(158, 22);
            this.deleteStudent.Text = "Удалить запись";
            this.deleteStudent.Click += new System.EventHandler(this.deleteStudent_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 23);
            // 
            // facultyMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1202, 591);
            this.Controls.Add(this.tabCtrlFaculty);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "facultyMainForm";
            this.Text = "Факультет: ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.facultyMainForm_FormClosed);
            this.tabCtrlFaculty.ResumeLayout(false);
            this.tabStudents.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentStudentList)).EndInit();
            this.tabPlaceFund.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentPlaceList)).EndInit();
            this.contxtTabMenu1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCtrlFaculty;
        private System.Windows.Forms.TabPage tabStudents;
        private System.Windows.Forms.TabPage tabPlaceFund;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage tabMessages;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox studentListComboBox;
        private System.Windows.Forms.TreeView treeStudents;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ComboBox placeComboBox;
        private System.Windows.Forms.TreeView treePlaces;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.DataGridView currentStudentList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.DataGridView currentPlaceList;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ContextMenuStrip contxtTabMenu1;
        private System.Windows.Forms.ToolStripMenuItem deleteStudent;
        private System.Windows.Forms.ToolStripMenuItem редактироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripButton offenseButton;
        private System.Windows.Forms.ToolStripMenuItem plannedInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plannedOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
    }
}