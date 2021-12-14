using Simmental.UI;
using System.Collections.Generic;

namespace Simmental.UI
{
    public interface IDesigner
    {
        Position BottomRight { get; set; }
        Position TopLeft { get; set; }

        IEnumerable<ITile> SelectedTiles(IWayfinder wayfinder);

    }
}