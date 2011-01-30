using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Server
{
    /// <summary>
    /// Generates a new universe
    /// </summary>
    public static class UniverseGenerator
    {
        /// <summary>
        /// Generates a new universe
        /// </summary>
        /// <returns>a newly generated universe</returns>
        public static Universe GenerateUniverse()
        {
            var universe = new Universe();

            Populate(universe);

            return universe;
        }

        private static void Populate(Universe universe)
        {
            universe.AddEntity(new Planet("Earth"), new Vector2(0, 0));
            universe.AddEntity(new Planet("Venus"), new Vector2(3000, 500));
            universe.AddEntity(new Planet("Mars"), new Vector2(-4000, 2000));
            universe.AddEntity(new Planet("Jupiter"), new Vector2(10000, -10000));
            universe.AddEntity(new Planet("Gamma Cephei Ab"), new Vector2(-10000, 10000));
            universe.AddEntity(new Planet("Persephane"), new Vector2(10000, 9000));
            universe.AddEntity(new Planet("Sirius"), new Vector2(-10000, -9000));

            universe.AddEntity(new Asteroid(), new Vector2(1000, -500));
            universe.AddEntity(new Asteroid(), new Vector2(-1000, 500));

            //universe.AddEntity(new AsteroidGenerator(), universe.GetSpawnLocation());
        }
    }
}
