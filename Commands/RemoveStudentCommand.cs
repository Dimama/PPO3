using System;
using System.Collections.Generic;
using Lab3;
using WindowsFormsApplication1.Abstract;

namespace WindowsFormsApplication1.Commands
{
    class DelStudentCommand : ICommand
    {

        public String index { set; get; }
        public String FIO { set; get; }
        public Data data { set; get; }

        public DelStudentCommand(Data _data, String _index, String _FIO)
        {
            data = _data;
            index = _index;
            FIO = _FIO;
        }
        public override void Execute()
        {
            List<Group> grps = new List<Group>();
            foreach (Group g in data.Groups)
                grps.Add(new Group(g));

            data.StackGroups.Push(new List<Group>(grps));
            if (data.UndoStackGroups.Count != 0)
                data.UndoStackGroups.Clear();

            Group group = data.Groups.Find(x => x.index == index);
            Student student = group.Students.Find(x => x.Surname + ' ' + x.Name + ' ' + x.Middlename == FIO);
            group.Students.Remove(student);
            int i;
            for (i = 0; i < data.Groups.Count; i++)
                if (data.Groups[i].index == index)
                    data.Groups[i] = group;
        }

        public override void Undo()
        {
            if (data.StackGroups.Count == 0)
                return;
            data.UndoStackGroups.Push(data.Groups);
            data.Groups = data.StackGroups.Pop();
        }

        public override void Redo()
        {
            if (data.UndoStackGroups.Count == 0)
                return;
            data.StackGroups.Push(data.Groups);
            data.Groups = data.UndoStackGroups.Pop();
        }
    }
}
