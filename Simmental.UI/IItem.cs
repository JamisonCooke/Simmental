using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Interfaces
{
    public interface IItem : ISignature
    {
        string Name { get; }
        string GetFullName();
        string Description { get; }
        // IWeaponAttributes WeaponAttributes { get; }
        int Count { get; set; }

        IEnumerable<(string menuText, Action menuAction)> GetMenuItems(IGame game);
    }
}
