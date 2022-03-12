using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.UI
{
    public interface ICommandManager
    {
        public void ExecuteCommand(List<ICommandBase> doList, List<ICommandBase> undoList);
        public void Undo();
        public void Redo();

    }
}
