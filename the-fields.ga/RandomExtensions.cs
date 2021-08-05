using System;

namespace GA
{
    public static class RandomExtensions
    {
        public static bool p(this Random rnd, double p)
        {
            return (rnd.NextDouble() <= p);
        }
    }
}