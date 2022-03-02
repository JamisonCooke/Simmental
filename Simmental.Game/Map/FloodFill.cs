using Simmental.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Map
{
    public static class FloodFill
    {
        public static void Execute(IGame game, Position startPosition, TileAttributeEnum targetAttribute, TileEnum targetType)
        {
            var activeTiles = new Queue<Position>();
            activeTiles.Enqueue(startPosition);
            var matchAttribute = game.Wayfinder[startPosition].TileAttribute;
            var matchType = game.Wayfinder[startPosition].TileType;
            if (matchAttribute == targetAttribute && matchType == targetType)
                return;

            while (activeTiles.Count > 0)
            {
                var p = activeTiles.Dequeue();
                var tile = game.Wayfinder[p];

                var addTiles = GetWalkableTiles(game, p, matchAttribute, matchType);
                foreach(var t in addTiles)
                    activeTiles.Enqueue(t);
                
                tile.TileAttribute = targetAttribute;
                tile.TileType = targetType;
            }
        }

        private static List<Position> GetWalkableTiles(IGame game, Position tile, TileAttributeEnum matchAttribute, TileEnum matchType)
        {
            var result = new List<Position>();

            // Loop over the 9 ways the character could move-- including not moving, which will be ignored by CreateAStar()
            for (int di = -1; di <= 1; di++)
            {
                for (int dj = -1; dj <= 1; dj++)
                {
                    // Don't include tiles we've visited before
                    if (di == 0 && dj == 0)
                        continue;
                    var checkingTile = game.Wayfinder[tile.i + di, tile.j + dj];
                    if (checkingTile == null)
                        continue;
                    if (!(checkingTile.TileAttribute == matchAttribute && checkingTile.TileType == matchType))
                        continue;
                    result.Add(new Position(tile.i + di, tile.j + dj));
                }
            }

            return result;
        }
    }
}
