
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Abstract
{
    public abstract class ICommand
    {
        public abstract void Execute();
        public abstract void Undo();
        public abstract void Redo();
    }
}
