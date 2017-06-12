using System;
using System.IO;
using System.Windows.Forms;
using Lab3;

namespace WindowsFormsApplication1
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            string[] paths = new string[3];
            paths = File.ReadAllLines(MainForm.ConfigFilename);
            textBox1.Text = paths[0];
            textBox2.Text = paths[1];
            textBox3.Text = paths[2];
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вы можете изменить загружаемые плагины и файл с данными.\n" +
                "Чтобы изменения вступили в силу, нажмите 'Применить' и перезапустите приложение." +
                "", "Справка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            File.Delete(MainForm.ConfigFilename);
            File.AppendAllText(MainForm.ConfigFilename, textBox1.Text + "\n");
            File.AppendAllText(MainForm.ConfigFilename, textBox2.Text + "\n");
            File.AppendAllText(MainForm.ConfigFilename, textBox3.Text);
            MessageBox.Show("Данные сохранены в файле:\n" + MainForm.ConfigFilename, "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}
