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
    public partial class ChooseBenefitForm : Form
    {
        public ChooseBenefitForm()
        {
            this.Location = new Point(MousePosition.X - 60, MousePosition.Y - 60);
            InitializeComponent();
        }

        private void ChooseBenefitForm_Activated(object sender, EventArgs e)
        {
            setupComboBox();
        }
        public string getBenefit()
        {
            return benefitName;
        }
        public bool getState()
        {
            return isOkPressed;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            isOkPressed = true;
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void setupComboBox()
        {
            SqlConnection objConn = mainForm.createDBConnection();
            QueryResult qr = SqlCommandBuilder.getQueryResult(new ReadAllRowsCommand("Primary_Benefit", "description", objConn).buildCommand());
            for (int i = 0; i < qr.getRowCount()-1; i++)
                comboBox1.Items.Add(qr.getValue(i, 0));
            objConn.Close();
            comboBox1.Text = comboBox1.Items[0].ToString();
        }
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            benefitName = comboBox1.Text;
        }
        /// <summary>
        /// 
        /// </summary>
        private string benefitName = null;
        private bool isOkPressed = false;
    }
}
