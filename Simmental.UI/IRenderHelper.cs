using System.Drawing;

namespace Simmental.UI
{
    public interface IRenderHelper
    {
        bool GetTileIndex(IWayfinder wayfinder, int x, int y, out int i, out int j);
        Rectangle GetTileRect(IWayfinder wayfinder, int i, int j);
    }
}