using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simmental.UI;

namespace Simmental.Game.Map
{
    [Serializable]
    public class Wayfinder : Simmental.UI.IWayfinder
    {
        public ITile[][] Tiles { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int CameraI { get; set; }
        public int CameraJ { get; set; }
        public int CameraWidth { get; set; }
        public int CameraHeight { get; set; }
        public int TilePixelWidth { get; set; } = 20;
        public int TilePixelHeight { get; set; } = 19;

        /// <summary>
        /// Returns the Tile at [i,j]
        /// (this is an example of using an Indexer -- which makes an object look like an array allowing used of the square brackets)
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public ITile this[int i, int j]
        {
            get
            {
                // When the (i,j) coordinate is out of bounds, return null
                if (i < 0 || i >= Width || j < 0 || j >= Height)
                    return null;

                return Tiles[i][j];
            }
        }
        public ITile this[Position position]
        {
            get
            {
                return this[position.i, position.j];
            }
        }


        public void Initialize(int width, int height)
        {
            Tiles = new Tile[width][];
            Width = width;
            Height = height;
            for (int i = 0; i < width; i++)
            {
                Tiles[i] = new Tile[height];
                
                for (int j = 0; j < height; j++)
                {
                    Tiles[i][j] = new Tile()
                    {
                        Name = $"({i},{j})",
                        TileType = UI.TileEnum.Grass,
                        TileAttribute = TileAttributeEnum.CanWalkOn,
                    };

                }
            }
        }

        public bool IsVisible(Position from, Position to, int maxDistance)
        {
            // First -- check if it's close enough
            var d = Math.Sqrt(Math.Pow(to.i - from.i, 2) + Math.Pow(to.j - from.j, 2));
            if (d > maxDistance)
                return false;

            if (d == 0)
                return !this[to].HasAttribute(TileAttributeEnum.Opaque);

            return IsVisible(from, to);
        }


        /// <summary>
        /// Returns true if there is line of sight between from and to, which can't be further than maxDistance
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="maxDistance"></param>
        /// <returns></returns>
        public bool IsVisible(Position from, Position to)
        {
            // Second -- Check all the tiles inbetween to see if any of them are non-transparent
            // We are using y = mx + b to find the line between them (in our case j = mi + b).
            double m = 0;  // default to 0
            if (from.i != to.i)
                m = (to.j - from.j) / ((double)to.i - from.i);
            double b = to.j - m * to.i;

            if (Math.Abs(from.i - to.i) > Math.Abs(from.j - to.j))
            {
                // Loop over the i
                int fromi = Math.Min(from.i, to.i);
                int toi = Math.Max(from.i, to.i);

                for (int i = fromi; i <= toi; i++)
                {
                    int j = (int)Math.Round(m * i + b);
                    var tile = this[i, j];
                    if (tile == null || tile.HasAttribute(TileAttributeEnum.Opaque))
                        return false;
                }
            }
            else
            {
                // Loop over the j--
                int fromj = Math.Min(from.j, to.j);
                int toj = Math.Max(from.j, to.j);

                for (int j = fromj; j <= toj; j++)
                {
                    int i;
                    if (m == 0) // avoid divide by zero
                        i = to.i;
                    else
                        i = (int)Math.Round((j - b) / m);

                    var tile = this[i, j];
                    if (tile == null || tile.HasAttribute(TileAttributeEnum.Opaque))
                        return false;
                }
            }

            // Well, it must be visible!
            return true;
        }

        /// <summary>
        /// Moves an ICharacter from his/her existing position to a new position, updating
        /// the Tile.NPCs list at the same time
        /// </summary>
        /// <param name="character"></param>
        /// <param name="moveTo"></param>
        public void Move(ICharacter character, Position moveTo)
        {
            var fromTile = this[character.Position];
            if (fromTile.NPCs.Contains(character))
                fromTile.NPCs.Remove(character);
            this[moveTo].NPCs.Add(character);
            
            character.SetPositionInternal(moveTo);
        }

        public void ApplyLightSources(IGame game)
        {
            // Set all (visible) tiles to their default light level
            for (int i = this.CameraI; i < this.CameraWidth + this.CameraI; i++)
            {
                for (int j = this.CameraJ; j < this.CameraHeight + this.CameraJ; j++)
                {
                    var tile = this[i, j];
                    if (tile != null)
                        tile.LightLevel = tile.DefaultLightLevel;
                }
            }

            // Apply light sources to all tiles
            foreach ((ILightSource lightSource, Position p) in game.GetLightSources())
            {
                ApplyLightSource(game, p, lightSource);
            }
        }

        private void ApplyLightSource(IGame game, Position p, ILightSource lightSource)
        {
            // Loop over all the tiles the light source can reach--
            // and check if that tile IsVisible() and if so, add in the light

            for (int i = p.i - lightSource.Distance; i <= p.i + lightSource.Distance; i++)
            {
                for (int j = p.j - lightSource.Distance; j <= p.j + lightSource.Distance; j++)
                {
                    var tile = this[i, j];
                    if (tile != null)
                    {
                        if (IsVisible(p, new Position(i, j), lightSource.Distance))
                        {
                            tile.LightLevel += lightSource.Brightness;
                            if (tile.LightLevel > 100)
                                tile.LightLevel = 100;
                        } 
                    }
                }
            }

        }
    }
}
