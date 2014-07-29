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
    public partial class newStudentForm : Form
    {
        public newStudentForm()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChooseBenefitForm chooseBenefitForm = new ChooseBenefitForm();
            chooseBenefitForm.ShowDialog();
            if (chooseBenefitForm.getState() && (listView1.FindItemWithText(chooseBenefitForm.getBenefit()) == null))
                listView1.Items.Add(chooseBenefitForm.getBenefit());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem eachItem in listView1.SelectedItems)
            {
                listView1.Items.Remove(eachItem);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || !System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, @"^[А-ЯЁ][а-яё]+$"))
                MessageBox.Show("Фамилия введена некоректно!");
            else if (textBox2.Text == "" || !System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, @"^[А-ЯЁ][а-яё]+$"))
                MessageBox.Show("Имя введено некоректно!");
            else if (textBox3.Text != "" && !System.Text.RegularExpressions.Regex.IsMatch(textBox3.Text, @"^[А-ЯЁ][а-яё]+$"))
                MessageBox.Show("Отчество введено некоректно!");
            else if (!System.Text.RegularExpressions.Regex.IsMatch(maskedTextBox1.Text, @"[0-9]\-[0-9]"))
                MessageBox.Show("Номер зачётки не указан!");
            else if (comboBox1.Text == "")
                MessageBox.Show("Год обучения не указан!");
            else if (comboBox2.Text == "")
                MessageBox.Show("Пол не указан!");
            else
            {
                studentName = textBox1.Text + " " + textBox2.Text + " " + textBox3.Text;
                rBook = maskedTextBox1.Text;
                yos = Convert.ToInt32(comboBox1.Text);
                gender = (comboBox2.Text == "Муж") ? true : false;
                
                aRange = (checkBox1.Checked && isTextNumber(textBox5.Text)) ? Convert.ToInt32(textBox5.Text) : -1;
                budget = (checkBox2.Checked && isTextNumber(textBox6.Text)) ? Convert.ToInt32(textBox6.Text) : -1;

                object[] array = new object[listView1.Items.Count];
                listView1.Items.CopyTo(array, 0);
                benefits = array.ToList<Object>();
                state = true;
            }
            Close();
        }


        private bool isTextNumber(string text)
        {
            bool textIsNumeric = true;
            try
            {
                int.Parse(text);
            }
            catch
            {
                textIsNumeric = false;
            }
            return textIsNumeric;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            state = false;
            Close();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Trim();
        }

        public string studentName { get; private set; }
        public string rBook { get; private set; }
        public bool gender { get; private set; }
        public int yos { get; private set; }
        public int aRange { get; private set; }
        public int budget { get; private set; }
        public List<Object> benefits { get; private set; }
        public bool state { get; private set; }
    }
}
