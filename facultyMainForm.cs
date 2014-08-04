using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
//using System.Windows.Media;
using System.Drawing.Drawing2D;

namespace ASAlloc
{
    public partial class facultyMainForm : Form
    {
        public facultyMainForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            this.Text = mainForm.name;
            this.studentListComboBox.Text = this.studentListComboBox.Items[0].ToString();
            this.placeComboBox.Text = this.placeComboBox.Items[2].ToString();
        }

        private void facultyMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private bool isStudentExists(string faculty, int yearOfStudy)
        {
            SqlConnection objConn = mainForm.createDBConnection();

            if (SqlCommandBuilder.isQueryNotEmpty(new GetFacultyStudentsCommand(faculty, yearOfStudy, objConn).buildCommand()))
            {
                objConn.Close();
                return true;
            }
            else
            {
                objConn.Close();
                return false;
            }
        }

        private void setupListsNodes()
        {
            SqlConnection objConn = mainForm.createDBConnection();
            treeStudents.Nodes.Clear();

            QueryResult qr = SqlCommandBuilder.getQueryResult(
                        new SelectAllRowsByTwoColumnsCommand("Lists", "Lists.description", "type", "False", "author", mainForm.name,objConn).buildCommand());
            List<TreeNode> privateLists = new List<TreeNode>();
            for (int i = 0; i < qr.getRowCount(); i++)
            {
                var tempNode = new TreeNode(qr.getValue(i, 0));
                privateLists.Add(tempNode);
            }
            var privateNodes = new TreeNode("Личные списки",privateLists.ToArray());
            privateNodes.ExpandAll();
            treeStudents.Nodes.Add(privateNodes);

            List<TreeNode> userListsNode = new List<TreeNode>();
            qr = SqlCommandBuilder.getQueryResult(new GetAllUsersExceptAdminCommand(objConn).buildCommand());
            for (int i = 0; i < qr.getRowCount(); i++)
            {
                QueryResult publicListsQuery = SqlCommandBuilder.getQueryResult(
                            new SelectAllRowsByTwoColumnsCommand("Lists", "Lists.description", "type", "True", "author", qr.getValue(i,0),objConn).buildCommand());
                if (publicListsQuery.getRowCount() == 0)
                    continue;
                List<TreeNode> publicLists = new List<TreeNode>();
                for (int j = 0; j < publicListsQuery.getRowCount(); j++)
                {
                    var tempNode = new TreeNode(publicListsQuery.getValue(j, 0));
                    publicLists.Add(tempNode);
                }
                userListsNode.Add(new TreeNode(qr.getValue(i,0),publicLists.ToArray()));
            }
            treeStudents.Nodes.Add(new TreeNode("Публичные списки", userListsNode.ToArray()));
            objConn.Close();
        }

        private void setupOrdersNodes()
        {
            SqlConnection objConn = mainForm.createDBConnection();
            treeStudents.Nodes.Clear();
            QueryResult qr = SqlCommandBuilder.getQueryResult(new SelectAllRowsByColumnValueCommand("Orders","date","type","True",objConn).buildCommand());
            
            List<TreeNode> inOrders = new List<TreeNode>();
            for (int i = 0; i < qr.getRowCount(); i++)
                inOrders.Add(new TreeNode(qr.getValue(i, 0)));
            treeStudents.Nodes.Add(new TreeNode("Заселение", inOrders.ToArray()));

            qr = SqlCommandBuilder.getQueryResult(new SelectAllRowsByColumnValueCommand("Orders", "date", "type", "False", objConn).buildCommand());

            List<TreeNode> outOrders = new List<TreeNode>();
            for (int i = 0; i < qr.getRowCount(); i++)
                outOrders.Add(new TreeNode(qr.getValue(i, 0)));
            treeStudents.Nodes.Add(new TreeNode("Выселение", outOrders.ToArray()));

            objConn.Close();
        }

        private void setupStudentNodes()
        {
            SqlConnection objConn = mainForm.createDBConnection();
            treeStudents.Nodes.Clear();
            try
            {
                QueryResult qr = SqlCommandBuilder.getQueryResult(new GetFacultyListCommand(objConn).buildCommand());
                for (int row = 0; row < qr.getRowCount(); row++)
                {
                    List<TreeNode> courses = new List<TreeNode>();
                    string facName = qr.getValue(row, "login");
                    for (int i = 1; i <= 5; i++)
                    {
                        if (isStudentExists(facName, i))
                            courses.Add(new TreeNode("Курс " + i.ToString()));
                    }
                    TreeNode facultyNode = new TreeNode(facName, courses.ToArray());
                    treeStudents.Nodes.Add(facultyNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            objConn.Close();
        }

        private void tabStudents_Enter(object sender, EventArgs e)
        {
            if (studentListComboBox.Text == "Студенты")
                setupStudentNodes();
            else if (studentListComboBox.Text == "Списки студентов")
                setupListsNodes();
            else if (studentListComboBox.Text == "Приказы")
                setupOrdersNodes();
        }

        private void setupPlaceNode(bool faculty, bool state)
        {
            SqlConnection objConn = mainForm.createDBConnection();
            treePlaces.Nodes.Clear();

            try
            {
                int buildingsCount = SqlCommandBuilder.getCorpusCount();

                for (int i = 0; i < buildingsCount; i++) 
                {
                    QueryResult qrFloors = SqlCommandBuilder.getQueryResult(new SelectAllRowsByColumnValueCommand("Floor", "corpus", (i + 1).ToString(), objConn).buildCommand());
                    List<TreeNode> floors = new List<TreeNode>();
                    for (int floorIndex = 0; floorIndex < qrFloors.getRowCount(); floorIndex++)
                    {
                        QueryResult qrRooms = SqlCommandBuilder.getQueryResult(new GetRoomsRowCommand(i + 1, floorIndex + 1, faculty, state, objConn).buildCommand());

                        if (qrRooms.getRowCount() !=0 || !faculty)
                        {
                            List<TreeNode> rooms = new List<TreeNode>();
                            for (int roomIndex = 0; roomIndex < qrRooms.getRowCount(); roomIndex++)
                                rooms.Add(new TreeNode("Комната #" + qrRooms.getValue(roomIndex, "number").ToString()));
                            floors.Add(new TreeNode("Этаж #" + qrFloors.getValue(floorIndex, "floor_level").ToString(), rooms.ToArray()));

                        }
                    }
                    TreeNode buildingNode = new TreeNode("Строение #" + (i+1).ToString(), floors.ToArray());
                    treePlaces.Nodes.Add(buildingNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            objConn.Close();
        }

        private void tabPlaceFund_Enter(object sender, EventArgs e)
        {
            if (placeComboBox.Text == "Все")
                setupPlaceNode(false, true);
            else if (placeComboBox.Text == "Свободные")
                setupPlaceNode(true, false);
            else if (placeComboBox.Text == "Доступные")
                setupPlaceNode(true, true);
        }

        private tabDescriptor.tabType parseListName(string owner, string name)
        {
            bool isOwnList = owner == mainForm.name;
            if (!isOwnList)
                return tabDescriptor.tabType.unsavedPrivateListTab;
            if (name.IndexOf("Заселение") == 0)
                return tabDescriptor.tabType.publicListInTab;
            if (name.IndexOf("Выселение") == 0)
                return tabDescriptor.tabType.publicListOutTab;
            if (name.IndexOf("Нарушение") == 0)
                return tabDescriptor.tabType.violatorsListTab;
            if (name.IndexOf("Приказ") == 0)
                return tabDescriptor.tabType.orderListTab;
            return tabDescriptor.tabType.unsavedPrivateListTab;
        }

        private void treeStudents_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SqlConnection objConn = mainForm.createDBConnection();
            if (studentListComboBox.Text == "Студенты")
            {
                currentStudentList.Rows.Clear();
                QueryResult qr;
                string tabText;
                string columnOrder = "name, rbook, gender, faculty, yos, place, accomm_range, budget";
                string faculty;
                if (e.Node.Parent == null)
                {
                    faculty = e.Node.Text;
                    qr = SqlCommandBuilder.getQueryResult(new SelectAllRowsByColumnValueCommand("Student", columnOrder, "faculty", faculty, objConn).buildCommand());
                    tabText = e.Node.Text;
                }
                else
                {
                    faculty = e.Node.Parent.Text;
                    qr = SqlCommandBuilder.getQueryResult(new SelectAllRowsByTwoColumnsCommand("Student", columnOrder, "faculty", faculty,
                                                                                               "yos", e.Node.Text.Replace("Курс ", ""),
                                                                                               objConn).buildCommand());
                    tabText = e.Node.Parent.Text + " : " + e.Node.Text;
                }
                qr.parseColumn(2, mainForm.colNames["gender"]);
                qr.parseColumn(5, "НЕТ", "ДА");
                qr.addToDataGridView(currentStudentList, mainForm.colNames["student"]);
                string tabName = createNewTab(tabControl1, tabText, qr, tabDescriptor.tabType.unsavedPrivateListTab, mainForm.colNames["student"]);
                tabs[tabName].addColumnParser(2, mainForm.colNames["gender"]);
            }
            else if (studentListComboBox.Text == "Списки студентов")
            {
                if (e.Node.Parent != null)
                {
                    if (e.Node.Parent.Text == "Личные списки")
                    {
                        currentStudentList.Rows.Clear();
                        QueryResult prList = SqlCommandBuilder.getQueryResult(new GetStudentListCommand(mainForm.name, false, e.Node.Text, objConn).buildCommand());
                        prList.parseColumn(2, mainForm.colNames["gender"]);
                        prList.parseColumn(5, "НЕТ", "ДА");
                        prList.addToDataGridView(currentStudentList, mainForm.colNames["student"]);
                        string tabName = createNewTab(tabControl1, e.Node.Text, prList, tabDescriptor.tabType.savedPrivateListTab, mainForm.colNames["student"]);
                        tabs[tabName].addColumnParser(2, mainForm.colNames["gender"]);
                    }
                    else if (e.Node.Parent.Parent != null)
                    {
                        currentStudentList.Rows.Clear();
                        QueryResult pubList = SqlCommandBuilder.getQueryResult(new GetPublicListCommand(e.Node.Text, objConn).buildCommand());
                        pubList.addToDataGridView(currentStudentList, mainForm.colNames["order"]);                        

                        createNewTab(tabControl1, e.Node.Text, pubList, parseListName(e.Node.Parent.Text,e.Node.Text), mainForm.colNames["order"]);
                    }
                }
            }
            else if (studentListComboBox.Text == "Приказы")
            {
                if (e.Node.Parent != null)
                {
                    DateTime dt = DateTime.Parse(e.Node.Text);
                    string newDate = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + " " +
                                     dt.Hour.ToString() + ":" + dt.Minute.ToString() + ":"+ dt.Second.ToString();
                    QueryResult qr = SqlCommandBuilder.getQueryResult(new GetOrderCommand(newDate, objConn).buildCommand());
                    qr.addToDataGridView(currentStudentList,mainForm.colNames["order"]);
                    createNewTab(tabControl1, "Приказ от " + e.Node.Text.Substring(0, 10), qr, tabDescriptor.tabType.orderListTab, mainForm.colNames["order"]);
                }
            }

            objConn.Close();
        }

        private void setupButtonsEnabledProperty(tabDescriptor.tabType type)
        {
            switch (type)
            {
                case tabDescriptor.tabType.noTab:
                    setupButtonsEnabledProperty(false, false, false, false, false, false);
                    break;
                case tabDescriptor.tabType.unsavedPrivateListTab:
                    setupButtonsEnabledProperty(true, true, false, false, false, false);
                    break;
                case tabDescriptor.tabType.savedPrivateListTab:
                    setupButtonsEnabledProperty(true, true, true, true, true, false);
                    break;
                case tabDescriptor.tabType.publicListInTab:
                    setupButtonsEnabledProperty(true, true, false, false, false, true);
                    break;
                case tabDescriptor.tabType.publicListOutTab:
                    setupButtonsEnabledProperty(false, true, false, false, false, true);
                    break;
                case tabDescriptor.tabType.publicListOutUnplannedTab:
                    setupButtonsEnabledProperty(true, true, false, false, false, true);
                    break;
                case tabDescriptor.tabType.violatorsListTab:
                    setupButtonsEnabledProperty(false, true, false, false, false, true);
                    break;
                case tabDescriptor.tabType.orderListTab:
                    setupButtonsEnabledProperty(false, true, false, false, false, mainForm.role == 2);
                    break;
                default:
                    break;
            }
        }

        private void closeTab(object sender)
        {
            TabControl tb = (TabControl)sender;
            tabs.Remove(tb.TabPages[tb.SelectedIndex].Name);
            tb.TabPages.RemoveAt(tb.SelectedIndex);
            string senderName = tb.Name;
            if (tb.TabCount == 0)
            {
                if (senderName == "tabControl1")
                    currentStudentList.Columns.Clear();
                else if (senderName == "tabControl2")
                    currentPlaceList.Columns.Clear();
                setupButtonsEnabledProperty(tabDescriptor.tabType.noTab);
            }
        }

        private string createNewTab(TabControl ctrl, string text, QueryResult content, tabDescriptor.tabType type, StringDictionary parser)
        {
            TabPage newPage = new TabPage(text);
            string tabName = "tabPage_" + tabCounter.ToString();
            newPage.Name = tabName;
            tabDescriptor tb = new tabDescriptor(content, type, parser);
            setupButtonsEnabledProperty(type);
            tabs[tabName] = tb;
            ctrl.TabPages.Add(newPage);
            ctrl.SelectTab(tabName);
            prevTabIndex = ctrl.SelectedIndex;
            tabCounter++;
            return tabName;
        }


        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            currentStudentList.Rows.Clear();
            if (e.TabPage != null)
            {
                QueryResult qr = tabs[e.TabPage.Name].qr;
                if (qr != null)
                    qr.addToDataGridView(currentStudentList,tabs[e.TabPage.Name].columnNameParser);
                setupButtonsEnabledProperty(tabs[e.TabPage.Name].type_);
            //    toolStripButton5.Enabled = tabs[e.TabPage.Name].isRemovable;
            //    toolStripButton7.Enabled = tabs[e.TabPage.Name].isRemovable;
            //    SqlConnection objConn = mainForm.createDBConnection();
              /*  if (new GetListIDCommand(e.TabPage.Text, objConn).buildCommand().ExecuteScalar() == null)
                {
                    tabs[e.TabPage.Name].isRemovable = false;
                    toolStripButton5.Enabled = false;
                    toolStripButton7.Enabled = false;
                    toolStripButton8.Enabled = false;
                }*/
              //  objConn.Close();
            }
        }

        private void treePlaces_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            currentPlaceList.Rows.Clear();
            QueryResult qr;
            SqlConnection objConn = mainForm.createDBConnection();

            string tabText;
            string columnOrder;

            bool isPlaceChosen = false;
            if (e.Node.Parent != null)
            {
                if (e.Node.Parent.Parent != null)
                {
                    isPlaceChosen = true;
                    columnOrder = "Place.id, Place.state, Room.number, Floor.floor_level, Floor.corpus, Student.faculty, Student.yos, Student.name";

                    qr = SqlCommandBuilder.getQueryResult(new GetPlaceRowCommand(e.Node.Text.Replace("Комната #", ""),
                                                                                 e.Node.Parent.Text.Replace("Этаж #", ""),
                                                                                 e.Node.Parent.Parent.Text.Replace("Строение #", ""), columnOrder, objConn).buildCommand());
                    tabText = e.Node.Parent.Parent.Text + " : " + e.Node.Parent.Text + " : " + e.Node.Text;
                }
                else
                {                    
                    columnOrder = "Room.id, Room.number, Room.type, Room.max_places, Room.current_places, Floor.floor_level, Floor.corpus";
                  
                    qr = SqlCommandBuilder.getQueryResult(new GetRoomsRowCommand(Convert.ToInt32(e.Node.Parent.Text.Replace("Строение #", "")),
                                                                                 Convert.ToInt32(e.Node.Text.Replace("Этаж #", "")), columnOrder, objConn).buildCommand());
                    tabText = e.Node.Parent.Text + " : " + e.Node.Text;
                }
            }
            else 
            {
                columnOrder = "Room.id, Room.number, Room.type, Room.max_places, Room.current_places, Floor.floor_level, Floor.corpus";

                qr = SqlCommandBuilder.getQueryResult(new GetRoomsRowCommand(Convert.ToInt32(e.Node.Text.Replace("Строение #", "")), columnOrder, objConn).buildCommand());
                tabText = e.Node.Text;
            }

            string parserName = isPlaceChosen ? "place" : "room";
            qr.parseColumn(1, mainForm.colNames["place_state"]);
            qr.addToDataGridView(currentPlaceList, mainForm.colNames[parserName]);

        //    createNewTab(tabControl2, tabText, qr, mainForm.role == 2, mainForm.role == 2, mainForm.colNames[parserName]);

            objConn.Close();
        }

        private void tabControl2_Selecting(object sender, TabControlCancelEventArgs e)
        {
            currentPlaceList.Rows.Clear();
            if (e.TabPage != null)
            {
                QueryResult qr = tabs[e.TabPage.Name].qr;
                if (qr != null)
                    qr.addToDataGridView(currentPlaceList,tabs[e.TabPage.Name].columnNameParser);
            }
        }

        private void tabCtrlFaculty_DrawItem(object sender, DrawItemEventArgs e)
        {           
            RectangleF tabTextArea = RectangleF.Empty;
            for (int nIndex = 0; nIndex < ((TabControl)sender).TabCount; nIndex++)
            {
                if (nIndex == ((TabControl)sender).SelectedIndex)
                {
                    tabTextArea = (RectangleF)((TabControl)sender).GetTabRect(nIndex);
                    LinearGradientBrush br = new LinearGradientBrush(tabTextArea, SystemColors.ControlLightLight,SystemColors.Control, LinearGradientMode.Vertical);
                    e.Graphics.FillRectangle(br, e.Bounds);
                    using (Bitmap bmp = new Bitmap(ASAlloc.Properties.Resources.close_2))
                    {
                        e.Graphics.DrawImage(bmp, tabTextArea.X + tabTextArea.Width - 13, 5, 10, 10);
                    }
                    br.Dispose();
                    Rectangle rect = new Rectangle((int)tabTextArea.Left, (int)tabTextArea.Top, (int)tabTextArea.Width, (int)tabTextArea.Height);
                    Pen pen = new Pen(Color.Gray);
                    e.Graphics.DrawRectangle(pen, rect);                    
                }
                else
                {
                    tabTextArea = (RectangleF)((TabControl)sender).GetTabRect(nIndex);
                    LinearGradientBrush br = new LinearGradientBrush(tabTextArea, SystemColors.ButtonFace, Color.Gray, LinearGradientMode.Vertical);
                    e.Graphics.FillRectangle(br,tabTextArea);
                    br.Dispose();
                    Rectangle rect = new Rectangle((int)tabTextArea.Left, (int)tabTextArea.Top, (int)tabTextArea.Width, (int)tabTextArea.Height);
                    Pen pen = new Pen(Color.Gray);
                    e.Graphics.DrawRectangle(pen, rect);
                }

                string str = ((TabControl)sender).TabPages[nIndex].Text;
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Near;
                using (SolidBrush brush = new SolidBrush(((TabControl)sender).TabPages[nIndex].ForeColor))
                {
                    e.Graphics.DrawString(str, ((TabControl)sender).Font, brush, tabTextArea, stringFormat);
                }
            }
        }

        private void setupButtonsEnabledProperty(bool addStudentToListButton, bool saveAsButton, bool saveButton,
                                                 bool removeListButton, bool renameListButton, bool postListButton)
        {
            toolStripButton4.Enabled = saveAsButton;
            toolStripButton5.Enabled = removeListButton;
            toolStripButton6.Enabled = addStudentToListButton;
            toolStripButton7.Enabled = saveButton;
            toolStripButton8.Enabled = renameListButton;
            toolStripButton10.Enabled = postListButton;
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            int selectedTab = ((TabControl)sender).SelectedIndex;
            bool isCurrentTabClosed = false;
            if( prevTabIndex == selectedTab)
            {
                RectangleF tabTextArea = (RectangleF)((TabControl)sender).GetTabRect(((TabControl)sender).SelectedIndex);
                tabTextArea = new RectangleF(tabTextArea.X + tabTextArea.Width - 16, 5, 13, 13);
                Point pt = new Point(e.X, e.Y);
                if (tabTextArea.Contains(pt))
                {
                    isCurrentTabClosed = true;
                    closeTab(sender);
                }
            }
            if (!isCurrentTabClosed)
                prevTabIndex = selectedTab;
            else if (isCurrentTabClosed)
            {
                if (prevTabIndex != 0)
                {
                    if (((TabControl)sender).TabCount == prevTabIndex)
                    {
                        ((TabControl)sender).SelectTab(prevTabIndex - 1);
                        prevTabIndex -= 1;
                    }
                    else
                        ((TabControl)sender).SelectTab(prevTabIndex);
                }

            }
        }

        private void addStudentButton_Click(object sender, EventArgs e)
        {
            newStudentForm nsf = new newStudentForm();
            nsf.ShowDialog();
            if (nsf.state)
            {
                SqlConnection objConn = mainForm.createDBConnection();
                new AddStudentCommand(nsf.studentName, nsf.rBook, nsf.gender, mainForm.name, nsf.yos, nsf.aRange, nsf.budget, objConn).buildCommand().ExecuteNonQuery();
                if (nsf.benefits.Count !=0)
                    new BindBenefitToStudent(nsf.rBook, nsf.benefits, objConn).buildCommand().ExecuteNonQuery();
                objConn.Close();
                MessageBox.Show("СТУДЕНТ ДОБАВЛЕН");
            }
        }
        private void addNewListButton_Click(object sender, EventArgs e)
        {
            AddStudent dlg = new AddStudent();
            dlg.ShowDialog();
            QueryResult qr = dlg.getResult();
            createNewTab(tabControl1, "Новый список", qr, tabDescriptor.tabType.unsavedPrivateListTab, new StringDictionary());
            currentStudentList.Rows.Clear();
            qr.addToDataGridView(currentStudentList);

        }
        private void currentStudentList_CellMouseRClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DataGridView dgv = (DataGridView)sender;
                if (!dgv.SelectedCells.Contains(((DataGridView)sender)[e.ColumnIndex, e.RowIndex]))
                    ((DataGridView)sender).CurrentCell = ((DataGridView)sender)[e.ColumnIndex, e.RowIndex];
                contxtTabMenu1.Show(Cursor.Position);
            }
        }  
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (currentStudentList.RowCount <= 1)
            {
                MessageBox.Show("Сохранение пустого списка запрещено!", "Список пуст", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            listNameDialog lnd = new listNameDialog(tabControl1.SelectedTab.Text);
            lnd.StartPosition = FormStartPosition.CenterParent;
            var result = lnd.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                SqlConnection objConn = mainForm.createDBConnection();
                DateTime DT = DateTime.Now;
                string listName = (lnd.listName.Trim() == "") ? "список " + mainForm.name + " от: " + 
                                                                DT.Year.ToString() + "-" + DT.Month.ToString() + "-" + DT.Day.ToString() + " " +
                                                                DT.Hour.ToString() + ":" + DT.Minute.ToString() + ":" + DT.Second.ToString() : lnd.listName;
                QueryResult qr = SqlCommandBuilder.getQueryResult(new GetListIDCommand(listName, objConn).buildCommand());

                if (qr.getRowCount() != 0)
                {
                    var mbResult = MessageBox.Show("Заменить существующий список?", "Список существует", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (mbResult == System.Windows.Forms.DialogResult.Cancel)
                    {
                        objConn.Close();
                        return;
                    }
                    new RemoveListCommand(Convert.ToInt32(qr.getValue(0,0)), objConn).buildCommand().ExecuteNonQuery();
                }
                List<string> rBooksEnum = new List<string>();
                for (int i = 0; i < currentStudentList.RowCount - 1; i++)
                    rBooksEnum.Add(currentStudentList.Rows[i].Cells[1].Value.ToString().Trim());

                new AddListCommand(false, DateTime.Now, listName, rBooksEnum, objConn).buildCommand().ExecuteNonQuery();
                tabControl1.SelectedTab.Text = listName;
                tabs[tabControl1.SelectedTab.Name].type_ = tabDescriptor.tabType.savedPrivateListTab;
                setupButtonsEnabledProperty(tabDescriptor.tabType.savedPrivateListTab);
                setupListsNodes();
                objConn.Close();
                studentListComboBox.Text = studentListComboBox.Items[1].ToString();
                MessageBox.Show("Список сохранён");
            }
        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count == 0)
                return;
            string listName = tabControl1.SelectedTab.Text;
            SqlConnection objConn = mainForm.createDBConnection();
            new RemoveListCommand(Convert.ToInt32((new GetListIDCommand(listName, objConn)).buildCommand().ExecuteScalar().ToString()), objConn).buildCommand().ExecuteNonQuery();
            closeTab(tabControl1);
            if (prevTabIndex != 0)
            {
                if (tabControl1.TabCount == prevTabIndex)
                {
                    tabControl1.SelectTab(prevTabIndex - 1);
                    prevTabIndex -= 1;
                }
                else
                    tabControl1.SelectTab(prevTabIndex);
            }
            setupListsNodes();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            AddStudent dlg = new AddStudent();
            dlg.setResult(tabs[tabControl1.SelectedTab.Name].qr);
            dlg.ShowDialog();
            tabs[tabControl1.SelectedTab.Name].qr = dlg.getResult();
            currentStudentList.Rows.Clear();
            dlg.getResult().addToDataGridView(currentStudentList);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (currentStudentList.RowCount <= 1)
            {
                MessageBox.Show("Сохранение пустого списка запрещено!", "Список пуст", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlConnection objConn = mainForm.createDBConnection();
            string listName = currentTabText();
            
            new RemoveListCommand(Convert.ToInt32(new GetListIDCommand(listName,objConn).buildCommand().ExecuteScalar().ToString()), objConn).buildCommand().ExecuteNonQuery();
            List<string> rBooksEnum = new List<string>();
            for (int i = 0; i < currentStudentList.RowCount - 1; i++)
                rBooksEnum.Add(currentStudentList.Rows[i].Cells[1].Value.ToString().Trim());

            new AddListCommand(false, DateTime.Now, listName, rBooksEnum, objConn).buildCommand().ExecuteNonQuery();
            setupButtonsEnabledProperty(tabDescriptor.tabType.savedPrivateListTab);
            //tabs[tabControl1.SelectedTab.Name].isRemovable = true;
            //toolStripButton5.Enabled = true;
            setupListsNodes();
            objConn.Close();
            MessageBox.Show("Список сохранён");
        }    
        private string currentTabText()
        {
            return tabControl1.SelectedTab.Text;
        }
        private void deleteStudent_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in currentStudentList.SelectedRows)
                currentStudentList.Rows.Remove(dr);
            tabs[tabControl1.SelectedTab.Name].qr.fromDataGridView(currentStudentList, false, true);
        } 
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            listNameDialog dlg = new listNameDialog();
            dlg.StartPosition = FormStartPosition.CenterParent;
            var result = dlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
                return;
            SqlConnection objConn = mainForm.createDBConnection();
            DateTime DT = DateTime.Now;
            string listName = (dlg.listName.Trim() == "") ? "список " + mainForm.name + " от: " +
                                                            DT.Year.ToString() + "-" + DT.Month.ToString() + "-" + DT.Day.ToString() + " " +
                                                            DT.Hour.ToString() + ":" + DT.Minute.ToString() + ":" + DT.Second.ToString() : dlg.listName;
            new RenameListCommand(currentTabText(), listName, objConn).buildCommand().ExecuteNonQuery();
            tabControl1.SelectedTab.Text = listName;
            setupListsNodes();

        }
        private void OutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        private int prevTabIndex = -1;
        static private int tabCounter = 0;
        private Dictionary<string, tabDescriptor> tabs = new Dictionary<string, tabDescriptor>();

        private void плановоеВыселениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection objConn = mainForm.createDBConnection();
            QueryResult qr = SqlCommandBuilder.getQueryResult(new GetResidentsListCommand(false, objConn).buildCommand());

            string listText = "Выселение (" + mainForm.name + ")";
            qr.addToDataGridView(currentStudentList, mainForm.colNames["order"]);
            createNewTab(tabControl1, listText, qr, tabDescriptor.tabType.publicListOutTab, mainForm.colNames["order"]);
            objConn.Close();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            TabPage currentTab = tabControl1.SelectedTab;          
            SqlConnection objConn = mainForm.createDBConnection();

            QueryResult qr = SqlCommandBuilder.getQueryResult(new GetListIDCommand(currentTab.Text, objConn).buildCommand());
            if (qr.getRowCount() != 0)
            {
                var mbResult = MessageBox.Show("Заменить существующий список?", "Список существует", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (mbResult == System.Windows.Forms.DialogResult.Cancel)
                {
                    objConn.Close();
                    return;
                }
                new RemoveListCommand(Convert.ToInt32(qr.getValue(0, 0)), objConn).buildCommand().ExecuteNonQuery();
            }
            List<string> rbooks = new List<string>();
            for(int i=0; i<tabs[currentTab.Name].qr.getRowCount(); i++)
                rbooks.Add(tabs[currentTab.Name].qr.getValue(i, 1).Trim());
            new AddListCommand(true, DateTime.Now, currentTabText(), rbooks, objConn).buildCommand().ExecuteNonQuery();
            setupListsNodes();
            objConn.Close();
        }        
    }
}
