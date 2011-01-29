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

            universe.AddEntity(new StarSystem("The first Star"), new Vector2(10, 10));
            universe.AddEntity(new StarSystem("The second Star"), new Vector2(-10, -10));

            // Remove these later
            //universe.AddEntity(universe.GenerateShip(new Player(null) { Name = "Test" }), universe.GetSpawnLocation());

            return universe;
        }
    }
}
