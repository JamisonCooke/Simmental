using System.Drawing;

namespace Simmental.Interfaces
{
    public interface IRenderHelper
    {
        bool GetTileIndex(IWayfinder wayfinder, int x, int y, out int i, out int j);
        Rectangle GetTileRect(IWayfinder wayfinder, int i, int j);
    }
}