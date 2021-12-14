using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.UI.WinForm.Render
{
    public class RenderWayfinder 
    {

        public RenderWayfinder(IWayfinder wayfinder, Graphics g, Position playerPosition)
        {
            Render(wayfinder, g, playerPosition);
        }

        private static void Render(IWayfinder wayfinder, Graphics g, Position playerPosition)
        {
            
            var renderTile = new RenderTile(g);
            var renderHelper = new RenderHelper();

            for (int i = wayfinder.CameraI; i < wayfinder.CameraWidth + wayfinder.CameraI; i++)
            {
                for (int j = wayfinder.CameraJ; j < wayfinder.CameraHeight + wayfinder.CameraJ; j++)
                {
                    ITile tile = wayfinder[i, j];
                    Rectangle rectangle = renderHelper.GetTileRect(wayfinder,  i, j);
                    bool isVisible = wayfinder.IsVisible(new Position(i, j), playerPosition, 10);
                    if (tile != null)
                    {
                        if (!tile.Seen && isVisible)
                            tile.Seen = true;       // First time we've seen this tile

                        // If we can't see this tile, don't draw anything!
                        if (!tile.Seen)
                            tile = null;
                    }

                    renderTile.Render(tile, rectangle, isVisible);
                }
                
            }
        }
    }
}
