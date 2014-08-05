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
    
    public partial class AddStudent : Form
    {
        public enum addStudentDialogType { defaultType = 0, outType = 1, inType = 2, offenseType = 3}
        public AddStudent()
        {
            InitializeComponent();
            dialogType = addStudentDialogType.defaultType;
            setupFaculties();
        }
        public AddStudent(addStudentDialogType type)
        {
            InitializeComponent();
            dialogType = type;
            setupControls();
        }

        private void setupControls()
        { 
            switch(dialogType)
            {
                case addStudentDialogType.defaultType:
                    break;                
                default:
                    comboBox1.Items.Clear();
                    comboBox1.Items.Add(mainForm.name);
                    comboBox1.Text = comboBox1.Items[0].ToString();
                    comboBox1.Enabled = false;
                    radioButton4.Enabled = false;
                    radioButton5.Enabled = false;
                    radioButton6.Enabled = false;
                    radioButton4.Checked = false;
                    switch(dialogType)
                    {
                        case addStudentDialogType.inType:
                            radioButton6.Checked = true;
                            break;
                        case addStudentDialogType.offenseType:
                            radioButton4.Checked = true;
                            break;
                        case addStudentDialogType.outType:
                            radioButton5.Checked = true;
                            break;
                        default: break;
                    }
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection objConn = mainForm.createDBConnection();
            string gender = radioButton1.Checked ? "" : (radioButton2.Checked ? "True" : "False");
            string havePlace = radioButton4.Checked ? "" : (radioButton5.Checked ? "True" : "False");
            List<int> courses = parseYos(textBox1.Text);
            string faculty = comboBox1.Text;
            if (dialogType == addStudentDialogType.defaultType)
            {
                QueryResult qr = SqlCommandBuilder.getQueryResult(new SelectStudentCommand(faculty, gender, courses, havePlace, objConn).buildCommand());
                if (qr.getRowCount() == 0)
                    MessageBox.Show("Ничего не найдено!");
                else
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    qr.parseColumn(2, mainForm.colNames["gender"]);
                    qr.parseColumn(5, "НЕТ", "ДА");
                    qr.addToDataGridView(dataGridView1, mainForm.colNames["student"]);
                }
            }
            else
            {
                QueryResult qr = SqlCommandBuilder.getQueryResult(new SelectStudentCommand(faculty, gender, courses, havePlace, objConn).buildOrderCommand());
                if (qr.getRowCount() == 0)
                    MessageBox.Show("Ничего не найдено!");
                else
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear();
                    qr.addToDataGridView(dataGridView1, mainForm.colNames["order"]);
                }
            }
            DialogResult = DialogResult.None;
            objConn.Close();
        }
        private List<int> parseYos(string str)
        {
            List<int> result = new List<int>();

            string[] digits = str.Replace(" ", "").Split(new Char[] { ',' });
            for (int i = 0; i < digits.Count(); i++)
            {
                
                if (digits[i].Split(new Char[] { '-' }).Count() == 2)
                {
                    bool invert = digits[i].Substring(0, 1) == "!";
                    string currentString = digits[i];
                    if (invert)
                        currentString = digits[i].Substring(1, digits[i].Length - 1);
                    string[] rangeDigits = currentString.Split(new Char[] { '-' });
                    int min, max;
                    if (int.TryParse(rangeDigits[0],out min) && int.TryParse(rangeDigits[1], out max))
                        if (min <= max && isYosInRange(min) && isYosInRange(max))
                        {
                            if (invert)
                            {
                                for (int j = 0; j < 7; j++)
                                    if (j < min || j > max)
                                        result.Add(j);
                            }
                            else
                            {
                                for (int j = min; j <= max; j++)
                                    result.Add(j);
                            }
                        }
                }
                else if (digits[i].IndexOf(">") == 0)
                {
                    int min;
                    if (int.TryParse(digits[i].Substring(1, digits[i].Count() - 1), out min))
                    {
                        if (isYosInRange(min+1))
                            for (int j = min+1; j < 7; j++)
                                result.Add(j);
                    }
                }
                else if (digits[i].IndexOf(">=") == 0)
                {
                    int min;
                    if (int.TryParse(digits[i].Substring(2, digits[i].Count() - 2), out min))
                    {
                        if (isYosInRange(min))
                            for (int j = min; j < 7; j++)
                                result.Add(j);
                    }
                }
                else if (digits[i].IndexOf("<") == 0)
                {
                    int min;
                    if (int.TryParse(digits[i].Substring(1, digits[i].Count() - 1), out min))
                    {
                        if (isYosInRange(min-1))
                            for (int j = min-1; j > 0; j--)
                                result.Add(j);
                    }
                }
                else if (digits[i].IndexOf("<=") == 0)
                {
                    int min;
                    if (int.TryParse(digits[i].Substring(2, digits[i].Count() - 2), out min))
                    {
                        if (isYosInRange(min))
                            for (int j = min; j > 0; j--)
                                result.Add(j);
                    }
                }
                else if (digits[i].IndexOf("!") == 0)
                {
                    int min;
                    if (int.TryParse(digits[i].Substring(1, digits[i].Count() - 1), out min))
                    {
                        if (isYosInRange(min))
                            for (int j = 0; j < 7; j++)
                                if (j!=min)
                                    result.Add(j);
                    }
                }
                else
                {
                    int n;
                    bool isNumeric = int.TryParse(digits[i], out n);
                    if (isNumeric)
                    {
                        if (isYosInRange(n))
                            result.Add(n);
                    }
                }


            }
            return result;
        }
        private bool isYosInRange(int yos)
        {
            return yos > 0 && yos < 7;
        }

        private void setupFaculties()
        {
            SqlConnection objConn = mainForm.createDBConnection();
            QueryResult qr = SqlCommandBuilder.getQueryResult(new GetFacultyListCommand(objConn).buildCommand());
            comboBox1.Items.Clear();
            for (int i = 0; i < qr.getRowCount(); i++)
                comboBox1.Items.Add(qr.getValue(i, 0));
            objConn.Close();
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            result.fromDataGridView(dataGridView1, true, false);
            DialogResult = DialogResult.None;
        }
        public QueryResult getResult()
        {
            return result;
        }
        public void setResult(QueryResult qr)
        {
            result = qr;
        }
        private QueryResult result = new QueryResult();

        private void AddStudent_FormClosed(object sender, FormClosedEventArgs e)
        {
            result.removeDuplicates();
        }
        private addStudentDialogType dialogType;
    }
}
