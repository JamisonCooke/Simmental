using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simmental.UI;

namespace Simmental.Game.Design
{
    [Serializable]
    public class Designer : IDesigner
    {
        public Position TopLeft { get; set; }
        public Position BottomRight { get; set; }

        public bool HighlightRange { get; set; }

        public IEnumerable<ITile> SelectedTiles(IWayfinder wayfinder)
        {
            int fromI = (TopLeft.i < BottomRight.i) ? TopLeft.i : BottomRight.i;
            int toI = (TopLeft.i > BottomRight.i) ? TopLeft.i : BottomRight.i;
            int fromJ = (TopLeft.j < BottomRight.j) ? TopLeft.j : BottomRight.j;
            int toJ = (TopLeft.j > BottomRight.j) ? TopLeft.j : BottomRight.j;

            for (int i = fromI; i <= toI; i++)
            {
                for (int j = fromJ; j <= toJ; j++)
                {
                    yield return wayfinder[i, j];
                }
            }
        }

        /// <summary>
        /// Swap numbers n1 and n2 (by reference)
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        private void Swap(ref int n1, ref int n2)
        {
            int temp = n1;
            n1 = n2;
            n2 = temp;
        }
    }
}
