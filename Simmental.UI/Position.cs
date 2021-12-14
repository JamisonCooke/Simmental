using Simmental.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.UI
{
    [Serializable]
    public class Position
    {

        /// <summary>
        /// Tile location across the x-axis
        /// </summary>
        public int i { get; set; }

        /// <summary>
        /// Tile location across the y-axis
        /// </summary>
        public int j { get; set; }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Position() { }

        /// <summary>
        /// Creates a tile position with an (i,j) coordinate
        /// </summary>
        /// <param name="i">Tile index horizontally</param>
        /// <param name="j">Tile index vertically</param>
        public Position(int i, int j)
        {
            // Notice that i shadows the classes i, so we have to use this to access it
            this.i = i;
            this.j = j;
        }

        public Position(Position position)
        {
            this.i = position.i;
            this.j = position.j;
        }

        /// <summary>
        /// Is true when the position being passed in is only unit away from this position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool AdjacentTo(Position position)
        {
            return Math.Abs(this.i - position.i) <= 1 && Math.Abs(this.j - position.j) <= 1;
        }

        public bool EqualTo(Position position)
        {
            return position.i == this.i && position.j == this.j;
        }

    }
}
