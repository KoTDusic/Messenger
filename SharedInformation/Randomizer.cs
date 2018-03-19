using System;

namespace SharedInformation
{
    public static class Randomizer
    {
        private static readonly Random Random = new Random();

        public static int Next()
        {
           return Random.Next();
        }
        public static int Next(int maxValue)
        {
            return Random.Next(maxValue);
        }
        public static int Next(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue);
        }
    }
}
