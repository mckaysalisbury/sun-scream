using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Server.GameObjects;

namespace Server
{
    /// <summary>
    /// Stores information about a asteroid
    /// </summary>
    [Deprecated]
    public class AsteroidGenerator : Entity
    {
        private const float size = 0.1f;

        public AsteroidGenerator() : base(string.Empty)
        {
        }

        int updateCount = 0;

        internal override void Update()
        {
            if (updateCount % 100 == 0)
                Universe.AddEntity(new Asteroid(), Position);

            updateCount++;
        }

        protected override Fixture GetFixture(World world)
        {
            var fixture = FixtureFactory.CreateCircle(world, size, 1);
            fixture.Body.IsStatic = true;
            fixture.CollisionFilter.CollidesWith = Category.None;

            //fixture.Body.BodyType = BodyType.Dynamic;

            //fixture.Body.IsStatic = false;
            //fixture.Friction = 0;
            //fixture.Body.LinearDamping = 0;
            //fixture.Body.AngularDamping = 0;
            return fixture;
            //return FixtureFactory.CreateCircle(world,1, new Body( Width, Height, 1);
        }

        internal override EntityUpdateType GetClientType()
        {
            return EntityUpdateType.Invisible;
        }
    }
}
