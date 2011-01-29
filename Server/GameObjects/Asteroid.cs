using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Controllers;


namespace Server
{
    /// <summary>
    /// Stores information about a asteroid
    /// </summary>
    public class Asteroid : Entity
    {
        private const float size = 0.1f;

        public Asteroid() : base(string.Empty)
        {
        }

        protected override Fixture GetFixture(World world)
        {
            var fixture = FixtureFactory.CreateCircle(world, size, 1);

            var gravity = new GravityController(.01f);
            gravity.AddBody(fixture.Body);
            gravity.GravityType = GravityType.DistanceSquared;

            world.AddController(gravity);
            return fixture;
        }

        internal override EntityUpdateType GetClientType()
        {
            return EntityUpdateType.Asteroid;
        }
    }
}
