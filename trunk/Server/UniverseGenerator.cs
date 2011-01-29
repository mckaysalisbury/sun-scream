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

            universe.AddEntity(new StarSystem(universe.GenerateId(), "The first Star"), new Vector2(1, 1));
            //universe.AddEntity(new StarSystem(universe.GenerateId(), "The second Star"), new Vector2(-1, -1));

            // Remove these later
            //universe.AddEntity(universe.GenerateShip(new Player(null) { Name = "Test" }), universe.GetSpawnLocation());

            return universe;
        }
    }
}
