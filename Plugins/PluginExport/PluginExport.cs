using System;
using System.IO;
using System.Windows.Forms;
using Lab3;

namespace PluginExport
{
    public class PluginExport: IPlugin
    {
        public String Name
        {
            get
            {
                return "Export";
            }
        }

        private void ExportGroup(Group g)
        {
            String filename = "D:\\lab3\\WindowsFormsApplication1\\" + g.index + ".txt";
            if (File.Exists(filename))
                File.Delete(filename);
            File.AppendAllText(filename, "Группа: " + g.index + "\n");
            File.AppendAllText(filename, "Староста: " + g.GetHead() + "\n");
            File.AppendAllText(filename, "Студентов: " + g.StudentsCount() + "\n");
            File.AppendAllText(filename, "Средний рейтинг: " + g.AverageRating() + "\n");
            File.AppendAllText(filename, "\nСтуденты:\n");

            foreach(Student s in g.Students)
            {
                File.AppendAllText(filename, "\nФамилия: " + s.Surname + "\n");
                File.AppendAllText(filename, "Имя: " + s.Name + "\n");
                File.AppendAllText(filename, "Отчество: " + s.Middlename + "\n");
                File.AppendAllText(filename, "Рейтинг: " + s.Rating + "\n");
            }
            
            MessageBox.Show("Данные сохранены в файле:\n" + filename, "Экспорт", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ExportStudent(Student s)
        {
            String filename = "D:\\lab3\\WindowsFormsApplication1\\" + s.Surname + ".txt";
            if (File.Exists(filename))
                File.Delete(filename);

            File.AppendAllText(filename, "Фамилия: " + s.Surname + "\n");
            File.AppendAllText(filename, "Имя: " + s.Name + "\n");
            File.AppendAllText(filename, "Отчество: " + s.Middlename + "\n");
            File.AppendAllText(filename, "Рейтинг: " + s.Rating + "\n");
            File.AppendAllText(filename, "Староста: " + s.Head.ToString() + "\n");

            MessageBox.Show("Данные сохранены в файле:\n" + filename, "Экспорт", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public void OnClickEventHandler(object sender, EventArgs e)
        {

            if (MainForm.isGroup)
                ExportGroup(MainForm.CurrGroup);
            else
                ExportStudent(MainForm.CurrStd);
            
        }
    }
}
