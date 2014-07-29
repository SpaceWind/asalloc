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

namespace ASAlloc
{
    public partial class Authorisation : Form
    {
        public Authorisation()
        {
            InitializeComponent();
        }

        private void Reject_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Submit_Click(object sender, EventArgs e)
        {            
            SqlConnection objConn = mainForm.createDBConnection();

            bool success = false;
            SqlCommand cmd = new AuthCommand(txtLogin.Text.Trim(), txtPass.Text.Trim(), objConn).buildCommand();
            if (SqlCommandBuilder.isQueryNotEmpty(cmd))
                success = authorizated = true;
            if (success)
            {
                QueryResult qr = SqlCommandBuilder.getQueryResult(cmd);
                int role = 0;
                role = Convert.ToInt32(qr.getValue(0,"role"));
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
                        roleString = "Undefined";
                        break;
                }
                MessageBox.Show("РОЛЬ: " + roleString + "\nИМЯ: " + qr.getValue(0, "name"));    

                mainForm.role = role;
                mainForm.name = qr.getValue(0, "login");

                switch (role)
                {
                case 1:
                    adminMainForm amf = new adminMainForm();
                    amf.Show();
                    Close();
                    break;
                case 2:
                    prorectorMainForm pmf = new prorectorMainForm();
                    pmf.Show();
                    Close();
                    break;
                case 3:
                    facultyMainForm fmf = new facultyMainForm();
                    fmf.Show();
                    Close();
                    break;
                default:
                    break;
                }

            }
            else
            {
                MessageBox.Show("Такой пользователь не найден");
            }
        }
        private void Authorisation_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!authorizated)
                Application.Exit();
        }
        private void txtLogin_TextChanged(object sender, EventArgs e)
        {
            ;
        } 

        private bool authorizated = false;
    }
}
