using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public static class Score
    {
        private static Dictionary<Ship, int> scores = new Dictionary<Ship, int>();
        public static int Give(Ship ship)
        {
            Ensure(ship);
            return scores[ship] = scores[ship] + 1;
        }

        private static void Ensure(Ship ship)
        {
            if (!scores.ContainsKey(ship))
            {
                scores[ship] = 0;
            }
        }

        public static int Get(Ship ship)
        {
            Ensure(ship);
            return scores[ship];
        }

        public static IEnumerable<KeyValuePair<Ship, int>> GetAll()
        {
            return scores;
        }
    }
}
