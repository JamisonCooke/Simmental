using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simmental.Interfaces;

namespace Simmental.Game.Map
{
    [Serializable]
    public class Pathfinder
    {
        #region AStarTile

        private class AStarTile 
        {
            public int i { get; }
            public int j { get; }
            public Position Position { get; }
            public int Cost { get; set; }
            public double Distance { get; set; }
            public double CostDistance => Cost + Distance;
            public AStarTile PriorTile { get; set; }
            
            public AStarTile(AStarTile fromTile, int i, int j, Position finalDestination)
            {
                this.i = i;
                this.j = j;
                this.Position = new Position(i, j);
                if (fromTile != null)
                    this.Cost = fromTile.Cost + 1;

                //Distance = Math.Max(Math.Abs(finalDestination.i - i), Math.Abs(finalDestination.j - j));
                Distance = Math.Sqrt(Math.Pow(finalDestination.i - i, 2) + Math.Pow(finalDestination.j - j, 2));
                PriorTile = fromTile;
            }

        }

        #endregion

        public IWayfinder Wayfinder { get; }
        public Position From { get; }
        public Position To { get; }
        public Position CurrentPosition { get; private set; }
        public bool Complete => (CurrentPosition.i == To.i && CurrentPosition.j == To.j) || !PathExists;
        public bool PathExists => _steps.Length > 0;
        public Pathfinder(IWayfinder wayfinder, Position from, Position to)
        {
            Wayfinder = wayfinder;
            From = from;
            To = to;
            CurrentPosition = from;

            // Run A* Alogorithm
            _steps = RunAStarAlogithm(from, to);
        }

        // Step 0: is from. _steps[:1] is To. Contains the steps to make in between
        private Position[] _steps;
        private int _stepNo;

        public void Move()
        {
            // MoveLame();
            MoveAStar();
        }


        private void MoveAStar()
        {
            if (_steps == null)
                return;

            _stepNo++;
            if (_stepNo < _steps.Length)
            {
                CurrentPosition = _steps[_stepNo];
            }
        }

        
        private Position[] RunAStarAlogithm(Position from, Position to)
        {
            var activeTiles = new Dictionary<Position, AStarTile>();
            var visitedTiles = new HashSet<Position>();
            var start = new AStarTile(null, from.i, from.j, to);
            activeTiles.Add(start.Position, start);

            AStarTile finalTile = null;

            while(activeTiles.Count > 0)
            {
                // Find the smallest CostDistance in the activeTiles
                AStarTile checkTile = null;     // O(n)
                foreach(var activeTile in activeTiles.Values)
                {
                    if (checkTile == null || activeTile.CostDistance < checkTile.CostDistance)
                        checkTile = activeTile;
                }

                // See if we reached our destination. If so, set finalTile, and get out of the loop
                if (checkTile.Position.Equals(to))
                {
                    finalTile = checkTile;
                    break;
                }

                // Move the tile from active to visited -- so we never consider it again
                visitedTiles.Add(checkTile.Position);
                activeTiles.Remove(checkTile.Position);

                // Fetch all the positions we could move to and add them
                var newTiles = GetWalkableTiles(checkTile, visitedTiles);
                foreach (var tile in newTiles)
                {
                    // If the tile is already in activeTiles and has a smaller CostDistance, replace i
                    if (activeTiles.TryGetValue(tile.Position, out var aStarTile))
                    {
                        if (tile.Cost < aStarTile.Cost)
                        {
                            activeTiles[tile.Position] = tile;
                        }
                    }
                    else
                    {
                        activeTiles[tile.Position] = tile;
                    }
                }
            }

            var result = new List<Position>();
            if (finalTile == null)
                return result.ToArray();

            var t = finalTile;
            while (t != null)
            {
                result.Add(new Position(t.i, t.j));
                t = t.PriorTile;
            }

            result.Reverse();
            return result.ToArray();
        }

        /// <summary>
        /// Return a list of AStarTiles that are reachable in one step. CanStepOn must be true
        /// in the wayfinder and the i,j must be valid in order to be returned. Note, does not
        /// consider if we've visited the tile in the past.
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        private List<AStarTile> GetWalkableTiles(AStarTile tile, HashSet<Position> visitedTiles)
        {
            var result = new List<AStarTile>();

            // Add appropriate tiles to the result
            AStarTile aStarTile;

            // Loop over the 9 ways the character could move-- including not moving, which will be ignored by CreateAStar()
            for (int di = -1; di <= 1; di++)
            {
                for (int dj = -1; dj <= 1; dj++)
                {
                    // Don't include tiles we've visited before
                    if (visitedTiles.Contains(new Position(tile.i + di, tile.j + dj)))
                        continue;

                    // Create it if it's 'CanWalkOn' and not tiles i,j
                    if (CreateAStar(tile, tile.i + di, tile.j + dj, out aStarTile))
                        result.Add(aStarTile);
                }
            }

            return result;
        }

        /// <summary>
        /// Returns true if i,j was a valid place to walk and sets out tile.
        /// </summary>
        /// <returns></returns>
        private bool CreateAStar(AStarTile fromTile, int i, int j, out AStarTile aStarTile)
        {
            var tile = Wayfinder[i, j];
            if (tile != null && tile.HasAttribute(TileAttributeEnum.CanWalkOn))
            {
                if (!(fromTile.i == i && fromTile.j == j))      // Don't add it if it's the same i,j as fromTile
                {
                    aStarTile = new AStarTile(fromTile, i, j, To);
                    return true;
                }
            }

            aStarTile = null;
            return false;
        }

    }
}
