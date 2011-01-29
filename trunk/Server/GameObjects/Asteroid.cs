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
            fixture.Body.BodyType = BodyType.Dynamic;
            fixture.Body.LinearVelocity = new Vector2((float)ScreamMath.Random.NextDouble() - 0.5f, (float)ScreamMath.Random.NextDouble() - 0.5f);
            return fixture;
        }

        internal override EntityUpdateType GetClientType()
        {
            return EntityUpdateType.Asteroid;
        }

        public override bool IsTractorable
        {
            get
            {
                return true;
            }
        }
    }
}
