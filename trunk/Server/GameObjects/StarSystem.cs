using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    /// <summary>
    /// Stores information about a star system
    /// </summary>
    public class StarSystem : Entity
    {
        private const float size = 10f;
        public StarSystem(int id, string name, float x, float y) : base(id, name, size, size)
        {

        }

        internal override EntityUpdateType GetClientType()
        {
            return EntityUpdateType.Star;
        }

        internal override void CreateBody(FarseerPhysics.Dynamics.World world)
        {
            base.CreateBody(world);

            Fixture.Body.IsStatic = true;
        }
    }
}
