using Simmental.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Characters
{
    [Serializable]
    public class Human : BaseCharacter
    {
        public override RaceEnum Race
        {
            get { return RaceEnum.Human; }
        }

    }
}
