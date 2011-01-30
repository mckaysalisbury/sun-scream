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

            universe.AddEntity(new StarSystem("Sol"), new Vector2(0,0));
            universe.AddEntity(new StarSystem("Alpha Centauri"), new Vector2(3000, 500));
            universe.AddEntity(new StarSystem("Procyon"), new Vector2(-4000, 2000));
            universe.AddEntity(new StarSystem("Deneb"), new Vector2(10000, -10000));
            universe.AddEntity(new StarSystem("Sirius"), new Vector2(-10000, 10000));
            universe.AddEntity(new StarSystem("Betelgeuse"), new Vector2(10000, 9000));
            universe.AddEntity(new StarSystem("Sirius"), new Vector2(-10000, -9000));

            universe.AddEntity(new Asteroid(), new Vector2(1000, -500));
            universe.AddEntity(new Asteroid(), new Vector2(-1000, 500));
            universe.AddEntity(new AsteroidGenerator(), universe.GetSpawnLocation());

            return universe;
        }
    }
}
