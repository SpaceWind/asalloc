using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASAlloc
{
    public partial class addEditUserForm : Form
    {
        public addEditUserForm()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();
        }

        public void setInfo(string login, string pass, int role, string name)
        {
            this.Controls.Find("loginBox", false).ElementAt(0).Text = login;
            this.Controls.Find("passBox_1", false).ElementAt(0).Text = pass;
            this.Controls.Find("passBox_2", false).ElementAt(0).Text = pass;

            string roleStr = "";
            switch (role)
            {
                case 1:
                    roleStr = "Администратор";
                    break;

                case 2:
                    roleStr = "Проректор";
                    break;

                case 3:
                    roleStr = "Факультет";
                    break;
                default:
                    break;
            }
            this.Controls.Find("roleBox", false).ElementAt(0).Text = roleStr;
            this.Controls.Find("nameBox", false).ElementAt(0).Text = name;
        }
        public string getLogin()
        {
            return this.Controls.Find("loginBox", false).ElementAt(0).Text;
        }
        public string getPass()
        {
            return this.Controls.Find("passBox_2", false).ElementAt(0).Text;
        }
        public bool isPassChanged()
        {
            return passChanged;
        }
        public bool isPassEquals()
        {
            return passEquals;
        }
        public int getRole()
        {
            string roleStr = this.Controls.Find("roleBox", false).ElementAt(0).Text;
            if (roleStr.ToLower() == "факультет")
                return 3;
            if (roleStr.ToLower() == "проректор")
                return 2;
            if (roleStr.ToLower() == "администратор")
                return 1;
            return 0;
        }
        public string getName()
        {
            return this.Controls.Find("nameBox", false).ElementAt(0).Text;
        }
        private bool checkPassEquality()
        {
            string pass1 = this.Controls.Find("passBox_1", false).ElementAt(0).Text;
            string pass2 = this.Controls.Find("passBox_2", false).ElementAt(0).Text;
            passEquals = pass1 == pass2;
            return passEquals;
        }
        private void passBox_1_TextChanged(object sender, EventArgs e)
        {
            passChanged = true;
            checkPassEquality();
        }
        private void addEditUserForm_Activated(object sender, EventArgs e)
        {
            checkPassEquality();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int role = getRole();
            bool error = false;
            if (role == 0)
            {
                MessageBox.Show("Указана недопустимая роль");
                error = true;
            }
            if (!passEquals)
            {
                MessageBox.Show("Пароли не совпадают");
                error = true;
            }
            if (!error)
            {
                isOkPressed = true;
                this.Close();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool isOkPressed = false;
        public bool getState()
        {
            return isOkPressed;
        }

        private bool passChanged = false;
        private bool passEquals = false;
    }
}
