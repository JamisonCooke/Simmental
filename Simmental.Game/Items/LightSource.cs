using Simmental.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Items
{
    [Serializable]
    public class LightSource : ItemBase, ILightSource
    {
        public LightSource(string name, string description, int brightness, int distance, bool isLit)
            : base(name, description)
        {
            Brightness = brightness;
            Distance = distance;
            IsLit = isLit;
        }

        public int Brightness { get; set; }
        public int Distance { get; set; }
        public bool IsLit { get; set; }

        public override string GetFullName()
        {
            if (IsLit)
                return base.GetFullName() + " (Lit)";
            else
                return base.GetFullName() + " (Unlit)";
        }

        public override IEnumerable<(string menuText, Action menuAction)> GetMenuItems(IGame game)
        {   
            if (IsLit)
                yield return ("Turn off", TurnOff);
            else
                yield return ("Turn on", TurnOn);
        }

        public void TurnOn()
        {
            IsLit = true;
        }
        public void TurnOff()
        {
            IsLit = false;
        }

    }
}
