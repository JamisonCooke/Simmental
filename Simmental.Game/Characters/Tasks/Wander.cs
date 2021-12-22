using Simmental.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Characters.Tasks
{
    [Serializable]
    internal class Wander : ITask
    {
        public bool ExecuteTask(IGame game, ICharacter character)
        {
            return false;
        }
    }
}
