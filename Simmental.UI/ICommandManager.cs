using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Interfaces
{
    public interface ICommandManager
    {
        public void ExecuteCommand(List<ICommandBase> doList, List<ICommandBase> undoList);
        public void ExecuteCommand(ICommandBase doCommand, ICommandBase undoCommand);
        public void Undo();
        public void Redo();

    }
}
