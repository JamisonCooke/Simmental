using Simmental.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Interfaces
{
    public interface ITask
    {

        bool ExecuteTask(IGame game, ICharacter character);

    }
}
