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
    public partial class AddServerForm : Form
    {
        public AddServerForm()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public string serverName
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
        public string serverAddress
        {
            get 
            {
                if (checkBox2.Checked)
                    return textBox2.Text;
                return "tcp:" + textBox2.Text + "," + textBox3.Text; 
            }
            set
            {
                string[] parsedStrings = value.Split(new Char[] { ',' });
                if (parsedStrings.Count() == 1)
                {
                    checkBox2.Checked = true;
                    textBox2.Text = value;
                }
                else
                {
                    string adress = parsedStrings[0].Substring(4, parsedStrings[0].Length - 4);
                    string port = parsedStrings[1];
                    textBox2.Text = adress;
                    textBox3.Text = port;

                }
            }
        }
        public bool isDebugMode
        {
            get { return checkBox1.Checked; }
            set { checkBox1.Checked = value; }
        }
        public string role
        {
            get { return comboBox1.Text; }
            set 
            {
                if (comboBox1.Items.Contains(value))
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(value);
            }
        }
        public string login
        {
            get { return textBox4.Text; }
            set { textBox4.Text = value; }
        }
        public bool result { get; private set; }
        public string securityMode
        {
            get { return comboBox2.Text; }
            set
            {
                if (comboBox2.Items.Contains(value))
                    comboBox2.SelectedIndex = comboBox2.Items.IndexOf(value);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            result = false;
            Close();
        }

        private void AddServerForm_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = ((CheckBox)sender).Checked;
            textBox4.Enabled = ((CheckBox)sender).Checked;
        }
    }
}
