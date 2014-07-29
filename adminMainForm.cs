using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

// Выделить объявление параметров запроса в отдельную функцию

namespace ASAlloc
{
    public partial class adminMainForm : Form
    {
        public adminMainForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void adminMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void tabPage2_Enter(object sender, EventArgs e)
        {
            updateRangSets();
        }
        private void tabPage1_Enter(object sender, EventArgs e)
        {
            updateUsersTable();
        }
//////////////////////Private_methods////////////////////////////////////////////////        
        private int getRoleByName(string roleString)
        {
            if (roleString.ToLower() == "администратор")
                return 1;
            if (roleString.ToLower() == "проректор")
                return 2;
            if (roleString.ToLower() == "факультет")
                return 3;
            return 0;
        }
        private void updateUsersTable()
        {
            SqlConnection objConn = mainForm.createDBConnection();
            
            QueryResult qr = SqlCommandBuilder.getQueryResult(new ReadAllRowsCommand("login", objConn).buildCommand());

            userList.Rows.Clear();

            for (int i = 0; i < qr.getRowCount(); i++)
            {
                int role = Convert.ToInt32(qr.getValue(i, "role"));
                string roleString = null;
                switch (role)
                {
                case 1:
                    roleString = "Администратор";
                    break;
                case 2:
                    roleString = "Проректор";
                    break;
                case 3:
                    roleString = "Факультет";
                    break;
                default:
                    roleString = "Неопределено";
                    break;
                }
                userList.Rows.Add((i + 1).ToString(), qr.getValue(i, "login"), qr.getValue(i, "password"), roleString, qr.getValue(i, "name"));
            }

            objConn.Close();
        }
        private void updateRangSets()
        {
            SqlConnection objConn = mainForm.createDBConnection();

            QueryResult qr = SqlCommandBuilder.getQueryResult(new ReadAllRowsCommand("Primary_Benefit", objConn).buildCommand());

            for (int i = 0; i < qr.getRowCount()-1; i++)
            {
                ((CheckBox)Controls.Find("checkBox" + (i+1).ToString(), true).First()).Checked = Convert.ToBoolean(qr.getValue(i,"enabled"));
                Controls.Find("label" + (i + 1).ToString(), true).First().Text = qr.getValue(i, "description");
                ((TrackBar)Controls.Find("trackBar" + (i+1).ToString(), true).First()).Value = Convert.ToInt32(qr.getValue(i,"priority"));
            }
            ((TrackBar)Controls.Find("trackBar11", true).First()).Value = Convert.ToInt32(qr.getValue(10, "priority"));
   
            objConn.Close();
        }
//////////////////////////Buttons///////////////////////////////////////////////
        private void button1_Click(object sender, EventArgs e)
        {
            addEditUserForm aeuf = new addEditUserForm();
            aeuf.ShowDialog();
            if (aeuf.getState())
                if(aeuf.getRole() != 1)
                {
                    SqlConnection objConn = mainForm.createDBConnection();
                    if (!SqlCommandBuilder.isQueryNotEmpty(new UserExistCommand(aeuf.getLogin().Trim(), objConn).buildCommand()))
                        new AddUserCommand(aeuf.getLogin().Trim(), (aeuf.isPassChanged()) ? md5Hash.GetMd5Hash(aeuf.getPass()) : aeuf.getPass(),
                                           aeuf.getRole(), aeuf.getName().Trim(), objConn).buildCommand().ExecuteNonQuery();
                    else
                        MessageBox.Show("Пользователь с такими параметрами уже существует!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    objConn.Close();
                    updateUsersTable();
                }
                else MessageBox.Show("Администратор уже существует!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
        }        
        private void button2_Click(object sender, EventArgs e)
        {
            int index = userList.CurrentCell.RowIndex;
            addEditUserForm editForm = new addEditUserForm();
            editForm.setInfo(userList.Rows[index].Cells[1].Value.ToString(),
                             userList.Rows[index].Cells[2].Value.ToString(),
                             getRoleByName(userList.Rows[index].Cells[3].Value.ToString()),
                             userList.Rows[index].Cells[4].Value.ToString());
            editForm.ShowDialog();

            if (editForm.getState())
            {
                SqlConnection objConn = mainForm.createDBConnection();

                new UpdateUserCommand(userList.Rows[index].Cells[1].Value.ToString().Trim(), editForm.getLogin().Trim(),
                                      (editForm.isPassChanged()) ? md5Hash.GetMd5Hash(editForm.getPass()) : editForm.getPass(),
                                      (editForm.getRole() == 1) ? getRoleByName(userList.Rows[index].Cells[3].Value.ToString()) : editForm.getRole(),
                                      editForm.getName(), objConn).buildCommand().ExecuteNonQuery();

                objConn.Close();
                updateUsersTable();
            }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            int index = userList.CurrentCell.RowIndex;

            var warnMsg = MessageBox.Show("Вы уверены?","Удаление",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);

            if (warnMsg == DialogResult.OK)
                if (userList.Rows[index].Cells[3].Value.ToString().Trim().ToLower() != "администратор")
                {
                    SqlConnection objConn = mainForm.createDBConnection();
                    new RemoveUserCommand(userList.Rows[index].Cells[1].Value.ToString().Trim(), objConn).buildCommand().ExecuteNonQuery();

                    objConn.Close();
                    updateUsersTable();
                }
                else MessageBox.Show("Администратор не может быть удалён!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
////////////////////////Labels/////////////////////////////////////////////////////////////
        private void checkBoxSwitch(string checkBox, bool changeChecked)
        {
            if (changeChecked)
                ((CheckBox)Controls.Find(checkBox, true).First()).Checked = !(((CheckBox)Controls.Find(checkBox, true).First()).Checked);
            updateCheckBoxState(((Label)Controls.Find(checkBox.Replace("checkBox", "label"), true).First()).Text,
                                ((CheckBox)Controls.Find(checkBox, true).First()).Checked);

        }
        private void label1_Click(object sender, EventArgs e)
        {
            checkBoxSwitch("checkBox" + ((Label)sender).Name.Replace("label", ""), true);
        }
        private void checkBox1_Click(object sender, EventArgs e)
        {
            checkBoxSwitch(((CheckBox)sender).Name, false);
        }
        private void updateCheckBoxState(string desc, bool state)
        {
            SqlConnection objConn = mainForm.createDBConnection();

            new SetBenefitStateCommand(desc, state, objConn).buildCommand().ExecuteNonQuery();

            objConn.Close();

        }
        private string getLabelTextByIndex(int i)
        {
            return Controls.Find("label" + i.ToString(), true).First().Text;
        }
        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            SqlConnection objConn = mainForm.createDBConnection();

            new SetBenefitValueCommand(getLabelTextByIndex(Convert.ToInt32(((TrackBar)sender).Name.Replace("trackBar", ""))),
                                       ((TrackBar)sender).Value, objConn).buildCommand().ExecuteNonQuery();

            objConn.Close();    
        }
////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
