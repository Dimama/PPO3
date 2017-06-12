using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using WindowsFormsApplication1;
using WindowsFormsApplication1.Commands;

namespace Lab3
{
    public partial class MainForm : Form
    {
        static public Group CurrGroup;
        static public Student CurrStd;
        static public bool isGroup;
        static public String ConfigFilename;
        public String PlugStudentPath = "D:\\lab3\\WindowsFormsApplication1\\Plugins\\PluginStudent1\\bin\\Debug\\PluginStudent1.dll";
        public String PlugExportPath = "D:\\lab3\\WindowsFormsApplication1\\Plugins\\PluginExport\\bin\\Debug\\PluginExport.dll";
        public String Filename = "D:\\lab3\\WindowsFormsApplication1\\groups.xml";
        Invoker inv = new Invoker();
        Data data;

        private void CreateTree()
        {
            TreeView.Nodes.Clear();
            GroupBox.Visible = false;
            TreeView.Nodes.Add("Группы");
            foreach (Group group in data.Groups)
            {
                TreeView.Nodes[0].Nodes.Add(group.index);
                foreach (Student student in group.Students)
                {
                    TreeView.Nodes[0].LastNode.Nodes.Add(student.Surname + ' ' + student.Name + ' ' + student.Middlename);
                    TreeView.ExpandAll();
                }
            }
        }

        private void LoadConfigFile()
        {
            ConfigFilename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\config.txt";
            if (File.Exists(ConfigFilename))
            {
                string[] paths = new string[3];
                paths = File.ReadAllLines(ConfigFilename);
                Filename = paths[0];
                PlugStudentPath = paths[1];
                PlugExportPath = paths[2];
            }
            else
            {
                File.AppendAllText(ConfigFilename, Filename + "\n");
                File.AppendAllText(ConfigFilename, PlugStudentPath + "\n");
                File.AppendAllText(ConfigFilename, PlugExportPath);
            }
        }
        private Group GetGroup(String index)
        {
            return data.Groups.Find(x => x.index == index);
        }

        private Student GetStudent(String FIO)
        {
            String index = TreeView.SelectedNode.Parent.Text;
            Group group = GetGroup(index);
            return group.Students.Find(x => x.Surname + ' ' + x.Name + ' ' + x.Middlename == FIO);
        }

        private void ShowGroupTable()
        {
            String index = TreeView.SelectedNode.Text;
            Group group = GetGroup(index);
            GroupBox.Text = "Группа";
            label1.Text = "Индекс группы:";
            label2.Text = group.index;
            label3.Text = "Количество студентов:";
            label4.Text = group.StudentsCount().ToString();
            label5.Text = "Максимальный рейтинг:";
            label6.Text = group.MaxRating().ToString();
            label7.Text = "Минимальный рейтинг:";
            label8.Text = group.MinRating().ToString();
            label9.Text = "Средний рейтинг:";
            label10.Text = group.AverageRating().ToString();
            label11.Text = "Староста:";
            label12.Text = group.GetHead();
        }

        private void ShowStudentTable()
        {
            String FIO = TreeView.SelectedNode.Text;
            String index = TreeView.SelectedNode.Parent.Text;
            Student student = GetStudent(FIO);
            GroupBox.Text = "Студент";
            label1.Text = "Имя:";
            label2.Text = student.Surname;
            label3.Text = "Фамилия:";
            label4.Text = student.Name;
            label5.Text = "Отчество:";
            label6.Text = student.Middlename;
            label7.Text = "Индекс группы:";
            label8.Text = index;
            label9.Text = "Рейтинг:";
            label10.Text = student.Rating.ToString();
            label11.Text = "Должность:";
            if (student.Head)
                label12.Text = "Староста";
            else
                label12.Text = "-";
        }

        private bool SelectedTreeRoot()
        {
            return TreeView.SelectedNode.Text == "Группы";
        }

        private bool SelectedGroup()
        {
            return TreeView.SelectedNode.Parent.Text == "Группы";
        }

        private String GetGroupIndexForGroup()
        {
            return TreeView.SelectedNode.Text;
        }

        private String GetGroupIndexForStudent()
        {
            return TreeView.SelectedNode.Parent.Text;
        }

        private String GetFIO()
        {
            return TreeView.SelectedNode.Text;
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void CheckUndoRedo()
        {
            if (inv.CommandsHistory.Count == 0)
                UndoButton.Enabled = false;
            else
                UndoButton.Enabled = true;
            if (inv.UndoCommandHistory.Count == 0)
                RedoButton.Enabled = false;

        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            GroupBox.Visible = false;
            UndoButton.Enabled = false;
            RedoButton.Enabled = false;
            LoadConfigFile();
            Loader loader = new XMLLoader();

            data = new Data(loader.Load(Filename), new Stack<List<Group>>(), new Stack<List<Group>>());
            CreateTree();
            LoadPlugin(PlugStudentPath);
            LoadPlugin(PlugExportPath);
            
        }

        private void LoadPlugin(String plugPath)
        {
            Assembly assembly1 = Assembly.LoadFrom(plugPath);
            foreach (Type t in assembly1.GetTypes())
            {
                if (t.IsClass && typeof(IPlugin).IsAssignableFrom(t))
                {
                    IPlugin plugin = (IPlugin)Activator.CreateInstance(t);
                    if (plugin.Name == "StudentGrants")
                        contextMenuStrip3.Items.Add("Информация о стипендии", null, plugin.OnClickEventHandler);
                    if (plugin.Name == "Export")
                    {
                        contextMenuStrip2.Items.Add("Экспорт в *.txt", null, plugin.OnClickEventHandler);
                        contextMenuStrip3.Items.Add("Экспорт в *.txt", null, plugin.OnClickEventHandler);
                    }
                }
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (SelectedTreeRoot())
                return;

            GroupBox.Visible = true;
            if (SelectedGroup())
                ShowGroupTable();
            else
                ShowStudentTable();
        }
        
        
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                TreeView.SelectedNode = e.Node;
                if (e.Node.Text == "Группы")
                    TreeView.ContextMenuStrip = contextMenuStrip1;
                else
                    if (e.Node.Parent.Text == "Группы")
                    {
                        String index = TreeView.SelectedNode.Text;
                        CurrGroup = GetGroup(index);
                        isGroup = true;
                        TreeView.ContextMenuStrip = contextMenuStrip2;
                    }
                    else
                    {
                        String FIO = TreeView.SelectedNode.Text;
                        CurrStd = GetStudent(FIO);
                        isGroup = false;
                        TreeView.ContextMenuStrip = contextMenuStrip3;
                    }
            }
        }

        private void ToolStripMenuAddStudent_Click(object sender, EventArgs e)
        {
            AddStudentCommand aStd = new AddStudentCommand(data, GetGroupIndexForGroup());
            inv.ExecuteAction(aStd);
            CreateTree();
            CheckUndoRedo();
        }

        private void ToolStripMenuDelGroup_Click(object sender, EventArgs e)
        {
            DelGroupCommand dGrp = new DelGroupCommand(data, GetGroupIndexForGroup());
            inv.ExecuteAction(dGrp); 
            CreateTree();
            CheckUndoRedo();
        }

        private void ToolStripMenuDelStudent_Click(object sender, EventArgs e)
        {
            DelStudentCommand dStd = new DelStudentCommand(data, GetGroupIndexForStudent(), GetFIO());
            inv.ExecuteAction(dStd);
            CreateTree();
            CheckUndoRedo();
        }

        private void ToolStripMenuAddGroup_Click(object sender, EventArgs e)
        {
            AddGroupCommand aGrp = new AddGroupCommand(data);
            inv.ExecuteAction(aGrp);
            CreateTree();
            CheckUndoRedo();
        }

        private void ToolStripMenuRenameGroup_Click(object sender, EventArgs e)
        {
            RenameGroupCommand rGrp = new RenameGroupCommand(data, GetGroupIndexForGroup());
            inv.ExecuteAction(rGrp);
            CreateTree();
            CheckUndoRedo();
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            inv.UndoAction();
            CreateTree();
            RedoButton.Enabled = true;
            CheckUndoRedo();
        }

        private void RedoButton_Click(object sender, EventArgs e)
        {
            inv.RedoAction();
            CreateTree();
            CheckUndoRedo();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ConfigForm form = new ConfigForm();
            form.ShowDialog();
        }
    }
}
