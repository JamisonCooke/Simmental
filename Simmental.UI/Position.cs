using Simmental.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Interfaces
{
    [Serializable]
    public class Position : IEquatable<Position>
    {

        /// <summary>
        /// Tile location across the x-axis
        /// </summary>
        public int i { get; }

        /// <summary>
        /// Tile location across the y-axis
        /// </summary>
        public int j { get; }

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
        /// Initializes from text "i,j" or "[i,j]"
        /// </summary>
        /// <param name="positionText"></param>
        public Position(string positionText)
        {
            try
            {
                string[] parts = positionText.Replace("[", "").Replace("]", "").Split(',');

                // Code
                this.i = int.Parse(parts[0]);
                this.j = int.Parse(parts[1]);
            }
            catch(Exception ex)
            {
                throw new Exception($"Unable to create Position object from string '{positionText}'.", ex);
            }
        }

        /// <summary>
        /// Returns a human readable error message if the passed positionText is valid. 
        /// Null or empty of it is valid
        /// </summary>
        /// <param name="positionText"></param>
        /// <returns></returns>
        public static string IsValid(string positionText)
        {
            string[] parts = positionText.Replace("[", "").Replace("]", "").Split(',');

            if (parts.Length != 2)
            {
                return $"Position in wrong format '{positionText}'. Expecting [i,j]";
            }

            if (!int.TryParse(parts[0], out int part))
            {
                return $"Unable to create position object from string: invalid input {parts[0]}";
            }

            if (!int.TryParse(parts[1], out int part2))
            {
                return $"Unable to create position object from string: invalid input {parts[1]}";
            }

            return "";
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

        public override bool Equals(object obj)
        {
            if (obj is Position p)
                return Equals(p);
            else
                return false;
        }

        public bool Equals(Position p)
        {
            return p.i == this.i && p.j == this.j;
        }

        public static bool operator ==(Position p1, Position p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Position p1, Position p2)
        {
            return !p1.Equals(p2);
        }

        /// <summary>
        /// Allows a string to be cast into a Position object
        /// </summary>
        /// <param name="text"></param>
        public static implicit operator Position(string text)
        {
            return new Position(text);
        }

        public override int GetHashCode()
        {
            return j * 1000003 + i;
        }

        public override string ToString()
        {
            return $"[{i},{j}]";
        }

    }
}
