using Simmental.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Command
{
    /// <summary>
    /// Keeps track of all the Do/Undo lists. Single instance for the running game-- not serializable
    /// </summary>\
    public class CommandManager : ICommandManager
    {
        private IGame _game;
        private Step _current;      // The current step

        public CommandManager(IGame game) 
        {
            _game = game;
            _current = new Step();      // Give it a blank FirstStep, so we always have a _current Pointer
        }
        
        /// <summary>
        /// Immediately executes the doList, and adds a step to the end of the commands
        /// </summary>
        /// <param name="doList"></param>
        /// <param name="undoList"></param>
        public void ExecuteCommand(List<ICommandBase> doList, List<ICommandBase> undoList)
        {
            foreach (CommandBase command in doList)
                command.Execute(_game);
            
            var step = new Step();
            step.DoList = doList;
            step.UndoList = undoList;
            step.NextStep = null;
            step.PriorStep = _current;
            step.PriorStep.NextStep = step;

            _current = step;
        }

        /// <summary>
        /// Undoes the last command
        /// </summary>
        public void Undo()
        {
            if (_current.IsFirstStep)
                return;

            foreach (CommandBase command in _current.UndoList)
                command.Execute(_game);

            _current = _current.PriorStep;
        }

        /// <summary>
        /// Reexecutes the last undone command
        /// </summary>
        public void Redo()
        {
            if (_current.NextStep == null)
                return;

            foreach (CommandBase command in _current.NextStep.DoList)
                command.Execute(_game);

            _current = _current.NextStep;
        }

        public void ExecuteCommand(ICommandBase doCommand, ICommandBase undoCommand)
        {
            ExecuteCommand(new List<ICommandBase>() { doCommand }, new List<ICommandBase>() { undoCommand });
        }
    }
}
