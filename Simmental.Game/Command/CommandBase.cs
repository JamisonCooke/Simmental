using Simmental.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Command
{
    public abstract class CommandBase : ICommandBase
    {
        public abstract void Execute(IGame game);

    }
}
