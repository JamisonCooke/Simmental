using Simmental.Interfaces;
using System.Collections.Generic;

namespace Simmental.Interfaces
{
    public interface IDesigner
    {
        Position BottomRight { get; set; }
        Position TopLeft { get; set; }

        bool HighlightRange { get; set; }

        IEnumerable<ITile> SelectedTiles(IWayfinder wayfinder);
        ITile SelectedTile(IWayfinder wayfinder);
        IEnumerable<Position> SelectedPositions(IWayfinder wayfinder);

    }
}