using System.Collections.Generic;
using WindowsFormsApplication1.Abstract; 

namespace Lab3
{
    static class Const
    {
        public const int ADD_GROUP = 1;
        public const int RENAME_GROUP = 2;
    }


    public class Data
    {
        public List<Group> Groups { set; get; }
        public Stack<List<Group>> StackGroups { set; get; }
        public Stack<List<Group>> UndoStackGroups { set; get; }

        public Data(List<Group> Lg, Stack<List<Group>> s1, Stack<List<Group>> s2)
        {
            Groups = Lg;
            StackGroups = s1;
            UndoStackGroups = s2;
        }
}
    

    class Invoker
    {
        public Stack<ICommand> CommandsHistory;
        public Stack<ICommand> UndoCommandHistory;

        public Invoker()
        {
            CommandsHistory = new Stack<ICommand>();
            UndoCommandHistory = new Stack<ICommand>();
        }


        public void ExecuteAction(ICommand com)
        {
            com.Execute();
            CommandsHistory.Push(com);
            if (UndoCommandHistory.Count != 0)
                UndoCommandHistory.Clear();
     
        }

        public void UndoAction()
        {
            if (CommandsHistory.Count > 0)
            {
                ICommand undoCommand = CommandsHistory.Pop();
                UndoCommandHistory.Push(undoCommand);
                undoCommand.Undo();
            }
        }

        public void RedoAction()
        {
            if (UndoCommandHistory.Count > 0)
            {
                ICommand redoCommand = UndoCommandHistory.Pop();
                CommandsHistory.Push(redoCommand);
                redoCommand.Redo();
            }
        }
    }
}
