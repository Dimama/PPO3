using System;
using System.Windows.Forms;

namespace Lab3
{
    public partial class AddStudentForm : Form
    {
        public Boolean done = false;

        private void ClearTextBoxes()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private Student GetStudentInfo()
        {
            Student student = new Student();
            if (textBox1.Text == "")
                return student;
            student.Surname = this.textBox1.Text;
            student.Name = this.textBox2.Text;
            student.Middlename = this.textBox3.Text;
            student.Rating = int.Parse(this.textBox4.Text);
            return student;
        }

        private bool EmptyTextBoxes()
        {
            return (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "");
        }

        public AddStudentForm()
        {
            InitializeComponent();
            ClearTextBoxes();
        }

        public Student NewStudent()
        {            
            return GetStudentInfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int test;
            bool IsNum = int.TryParse(textBox4.Text, out test);
            if (EmptyTextBoxes())
                MessageBox.Show("Не все поля заполнены!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                if (!IsNum)
                MessageBox.Show("Значение поля \"Рейтинг\" должно быть числом!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    if (test < 0 || test > 100)
                MessageBox.Show("Допустимые значения рейтинга: 0 - 100.", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        this.done = true;
                        this.Close();
                    }
                        
        }

        private void AddStudentForm_Load(object sender, EventArgs e)
        {

        }
    }
}
