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
        public LightSource(string name, string description, int brightness, int distance)
            : base(name, description)
        {
            Brightness = brightness;
            Distance = distance;
        }

        public int Brightness { get; set; }
        public int Distance { get; set; }
    }
}
