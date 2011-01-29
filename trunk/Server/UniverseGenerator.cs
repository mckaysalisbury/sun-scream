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
            universe.AddEntity(new StarSystem("Sirius"), new Vector2(30, 5));
            universe.AddEntity(new StarSystem("Procyon"), new Vector2(-40, 20));
            universe.AddEntity(new StarSystem("Deneb"), new Vector2(100, -100));
            universe.AddEntity(new StarSystem("Sirius"), new Vector2(-100, 100));

            // Remove these later
            //universe.AddEntity(universe.GenerateShip(new Player(null) { Name = "Test" }), universe.GetSpawnLocation());

            return universe;
        }
    }
}
