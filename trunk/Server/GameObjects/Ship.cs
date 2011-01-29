using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace Server
{
    /// <summary>
    /// Stores information about a ship
    /// </summary>
    public class Ship : Entity
    {
        private const float size = 0.1f;

        public Ship(int id, string name) : base(id, name)
        {
        }

        internal override EntityUpdateType GetClientType()
        {
            return EntityUpdateType.Ship;
        }

        protected override Fixture GetFixture(World world)
        {
            var fixture = FixtureFactory.CreateRectangle(world, size, size, 1);
            fixture.Body.IsStatic = false;
            fixture.Friction = 0;
            return fixture;
            //return FixtureFactory.CreateCircle(world,1, new Body( Width, Height, 1);
        }
    }
}
