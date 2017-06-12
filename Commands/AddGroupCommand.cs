using System;
using System.Collections.Generic;
using Lab3;
using WindowsFormsApplication1.Abstract;

namespace WindowsFormsApplication1.Commands
{
    class AddGroupCommand : ICommand
    {

        public Data data { set; get; }

        public AddGroupCommand(Data _data)
        {
            data = _data;
        }

        public override void Execute()
        {
            Add_ChangeGroupForm thirdForm = new Add_ChangeGroupForm(Const.ADD_GROUP);
            thirdForm.ShowDialog();
            if (thirdForm.done == false)
                return;
            Group group = thirdForm.NewGroup();
            List<Group> grps = new List<Group>();
            foreach (Group g in data.Groups)
                grps.Add(new Group(g));

            data.StackGroups.Push(new List<Group>(grps));
            if (data.UndoStackGroups.Count != 0)
                data.UndoStackGroups.Clear();
            data.Groups.Add(group);
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
