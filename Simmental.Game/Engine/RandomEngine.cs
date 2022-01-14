using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Simmental.Game.Engine
{
    public class RandomEngine
    {
        public RandomEngine()
        {
            //_seed = Timer.ElapsedMilliseconds();
        }

        public RandomEngine(double seed)
        {
            _seed = seed;
        }

        private double _seed = 1.0;

        /// <summary>
        /// Returns a random looking number between 0 <= 1
        /// </summary>
        /// <returns></returns>
        public double Next()
        {
            double newNumber = _seed * 832171 * Math.PI;
            _seed = Math.Floor(newNumber);      // Floor drops .1283712987 the decimals and gives you the whole number
            
            return newNumber - _seed;           // Return just the decimal positions for the result
        }

        public int Foo()
        {
            int i = 123;
            int j = 234;

            int r = (int)(i / (double)j * 1234);
            return r;
        }

        public int Next(int maxValue)
        {
            return (int)(Next() * maxValue);
        }

    }
}
