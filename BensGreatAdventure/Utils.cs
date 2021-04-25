using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BensGreatAdventure
{
    public static class Utils
    {
        public static int Sign(int n)
        {
            if (n == 0) return 0;
            return n < 0 ? -1 : 1;
        }

        public static int MinMax(int n, int min, int max)
        {
            if (n < min) return min;
            else if (n > max) return max;
            else return n;
        }
    }
}
