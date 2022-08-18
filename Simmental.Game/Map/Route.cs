using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Map
{
    /// <summary>
    /// Represents a list of positions delimited by dashes. For example, [3,1]-[14,1]-[14,7]-[9,4] 
    /// </summary>
    [Serializable]
    public class Route
    {
        private List<Position> _positions = new();

        public List<Position> Positions => _positions;

        public Route() { }
        public Route(string routeText)
        {
            string[] positions = routeText.Split('-');
            foreach(string position in positions)
            {
                _positions.Add(new Position(position));
            }
        }

        public override string ToString()
        {
            return string.Join("-", _positions);
        }

        /// <summary>
        /// Returns a human readable error message for any errors found in routeText, otherwise an empty string.
        /// </summary>
        /// <param name="routeText"></param>
        /// <returns></returns>
        public static string ValidateRoute(string routeText)
        {
            string[] positionText = routeText.Split('-');
            foreach(string position in positionText)
            {
                string errormessage = Position.IsValid(position);
                if (!string.IsNullOrEmpty(errormessage))
                {
                    return errormessage;
                }
            }
            return "";
        }
    }
}
