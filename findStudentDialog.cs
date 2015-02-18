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
    public partial class findStudentDialog : Form
    {
        public findStudentDialog(SqlConnection connection)
        {
            InitializeComponent();
            radioButton2.Checked = true;
            updateGroupBoxes();
            objConn = connection;
        }

        private void updateGroupBoxes()
        {
            foreach (Control ctrl in groupBox1.Controls)
                ctrl.Enabled = radioButton1.Checked;
            foreach (Control ctrl in groupBox2.Controls)
                ctrl.Enabled = radioButton2.Checked;
            foreach (Control ctrl in groupBox3.Controls)
                ctrl.Enabled = radioButton3.Checked;
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            updateGroupBoxes();
        }
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            QueryResult qr = SqlCommandBuilder.getQueryResult(new GetRoomsRowCommand(Convert.ToInt32(comboBox1.Text), "number", objConn).buildCommand());
            for (int i = 0; i < qr.getRowCount(); i++)
                comboBox2.Items.Add(qr.getValue(i, 0).ToString());
        }

        private SqlConnection objConn;        
    }
}
