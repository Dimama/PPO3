using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Add_ChangeGroupForm : Form
    {
        public Boolean done = false;

        private void ClearTextBox()
        {
            textBox1.Clear();
        }

        private void AddGroupInterface()
        {
            this.Text = "Добавить группу";
            label1.Text = "Индекс группы";
        }

        private void ChangeGroupInterface()
        {
            this.Text = "Изменить группу";
            label1.Text = "Новый индекс";
        }

        private bool EmptyTextBox()
        {
            return (textBox1.Text == "");
        }

        private String GetGroupIndex()
        {
            return textBox1.Text;
        }

        private Group GetGroupInfo()
        {
            Group group = new Group();
            if (EmptyTextBox())
                return group;
            group.index = GetGroupIndex();
            return group;
        }

        public Add_ChangeGroupForm()
        {
            InitializeComponent();
        }

        public Add_ChangeGroupForm(int n)
        {
            InitializeComponent();
            switch(n)
            {
                case Const.ADD_GROUP:
                    AddGroupInterface();
                    break;
                case Const.RENAME_GROUP:
                    ChangeGroupInterface();
                    break;
                default:
                    break;
            }                                    
        }

        public Group NewGroup()
        {
            return GetGroupInfo();
        }

        public String ChangeIndex()
        {
            return GetGroupIndex();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (EmptyTextBox())
                MessageBox.Show("Не введён индекс группы!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                this.done = true;
                this.Close();
            }
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearTextBox();
            this.Close();
        }
    }
}
