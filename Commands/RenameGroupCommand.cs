using System;
using System.Collections.Generic;
using Lab3;
using WindowsFormsApplication1.Abstract;


namespace WindowsFormsApplication1.Commands
{
    class RenameGroupCommand : ICommand
    {

        public String index { set; get; }

        public Data data { set; get; }

        public RenameGroupCommand(Data _data, String _index)
        {
            data = _data;
            index = _index;

        }

        public override void Execute()
        {
            Add_ChangeGroupForm thirdForm = new Add_ChangeGroupForm(Const.RENAME_GROUP);
            thirdForm.ShowDialog();
            if (thirdForm.done == false)
                return;
            String NewIndex = thirdForm.ChangeIndex();

            List<Group> grps = new List<Group>();
            foreach (Group g in data.Groups)
                grps.Add(new Group(g));

            data.StackGroups.Push(new List<Group>(grps));

            if (data.UndoStackGroups.Count != 0)
                data.UndoStackGroups.Clear();
            foreach (Group group in data.Groups)
                if (group.index == index)
                    group.index = NewIndex;
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
