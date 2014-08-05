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
    public partial class createOffence : Form
    {
        public createOffence()
        {
            InitializeComponent();
            comboBox1.Text = comboBox1.Items[3].ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void createOffence_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var addStudentDialog = new AddStudent(AddStudent.addStudentDialogType.offenseType);
            addStudentDialog.StartPosition = FormStartPosition.CenterParent;
            QueryResult qr = new QueryResult();
            qr.fromDataGridView(dataGridView1,false,false);
            addStudentDialog.setResult(qr);
            addStudentDialog.ShowDialog();
            qr = addStudentDialog.getResult();
            qr.addToDataGridView(dataGridView1, mainForm.colNames["order"]);
            DialogResult = DialogResult.None;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dataGridView1.SelectedRows)
                dataGridView1.Rows.RemoveAt(item.Index);
            DialogResult = DialogResult.None;
        }
        public QueryResult getStudents()
        {
            QueryResult qr = new QueryResult();
            qr.fromDataGridView(dataGridView1, false, false);
            return qr;
        }
        public string getType()
        {
            return comboBox1.Text;
        }
        public string getDescription()
        {
            return richTextBox1.Text;
        }
    }
}
