using Simmental.Interfaces;
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
            : this(name: sp[0], description: sp[1], brightness: sp.ToInt(2), distance: sp.ToInt(3), isLit: sp.ToBool(4))
        { }

        public static string GetSignatureFormat()
        {
            return "Name,Description,Brightness:Int32,Distance:Int32,IsLit:Boolean";
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

        public override string GetSignature()
        {
            var sp = new SignatureParts(typeof(LightSource), Name, Description, Brightness.ToString(), Distance.ToString(), IsLit.ToString());

            return sp.ToSignature();
        }
    }
}
