using Simmental.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Characters.Tasks
{
    public interface ITask
    {

        bool ExecuteTask(IGame game, ICharacter character);

    }
}
