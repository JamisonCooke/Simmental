using Simmental.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Command
{
    internal class Step
    {
        public bool IsFirstStep => PriorStep == null;

        public List<ICommandBase> DoList { get; set; }
        public List<ICommandBase> UndoList { get; set; }

        public Step NextStep { get; set; }
        public Step PriorStep { get; set; }


    }
}
