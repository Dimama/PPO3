using System;
using System.Windows.Forms;
using Lab3;

namespace PluginStudent1
{
    class PluginStudentGrants : IPlugin
    {
        public String Name
        {
            get
            {
                return "StudentGrants";
            }
        }

        private void ShowGrants(Student s)
        {
            String student = s.Surname + " " + s.Name + " " + s.Middlename;
            String grants = "Стипендия: ";
            int rate = s.Rating;
            if (rate >= 85)
                grants += "повышенная";
            else
                if (rate >= 71)
                grants += "обычная";
            else
                grants += "отсутствует";
            MessageBox.Show("Студент: " + student + "\n" + grants, "Информация  о стипендиии", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void OnClickEventHandler(object sender, EventArgs e)
        {
            Student s = MainForm.CurrStd;
            ShowGrants(s);
        }
    }
}
