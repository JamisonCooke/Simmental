﻿using Simmental.Game.Command;
using Simmental.Interfaces;
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
            var doList = new List<ICommandBase>();
            var undoList = new List<ICommandBase>();

            var activeTiles = new HashSet<Position>();
            activeTiles.Add(startPosition);
            var matchAttribute = game.Wayfinder[startPosition].TileAttribute;
            var matchType = game.Wayfinder[startPosition].TileType;
            if (matchAttribute == targetAttribute && matchType == targetType)
                return;

            while (activeTiles.Count > 0)
            {
                var p = activeTiles.FirstOrDefault();
                activeTiles.Remove(p);

                var tile = game.Wayfinder[p];

                // Add do & undo commands in case they use Ctl+Z and Ctl+Y
                doList.Add(new UpdateTile(p, targetType, targetAttribute));
                undoList.Add(new UpdateTile(p, matchType, matchAttribute));

                tile.TileAttribute = targetAttribute;
                tile.TileType = targetType;

                var addTiles = GetWalkableTiles(game, p, matchAttribute, matchType);


                foreach (var t in addTiles)
                    activeTiles.Add(t);
            }

            game.CommandManager.ExecuteCommand(doList, undoList);
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
