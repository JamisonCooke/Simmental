using Simmental.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Items
{
    [Serializable]
    public class LightSource : ItemBase, ILightSource, ISignature
    {
        public LightSource(string name, string description, int brightness, int distance, bool isLit)
            : base(name, description)
        {
            Brightness = brightness;
            Distance = distance;
            IsLit = isLit;
        }

        public LightSource(SignatureParts sp) 
            : this(sp[0], sp[1], int.Parse(sp[2]), int.Parse(sp[3]), bool.Parse(sp[4]))
        { }

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

        public string GetSignature()
        {
            var sp = new SignatureParts(typeof(LightSource), Name, Description, Brightness.ToString(), Distance.ToString(), IsLit.ToString());

            return sp.ToSignature();
        }
    }
}
