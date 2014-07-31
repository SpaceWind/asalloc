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
    public partial class listNameDialog : Form
    {
        public listNameDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            listName = textBox1.Text.Trim();
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        public string listName { get; private set; }
    }
}
