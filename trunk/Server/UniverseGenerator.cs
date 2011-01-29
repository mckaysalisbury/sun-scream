using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            universe.AddEntity(new StarSystem(universe.GenerateId(), "The first Star", 0, 0));

            // Remove these later
            universe.AddEntity(universe.GenerateShip(new Player(null) { Name = "Test" }));

            return universe;
        }
    }
}
