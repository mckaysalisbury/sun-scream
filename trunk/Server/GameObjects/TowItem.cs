using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace Server
{
    public abstract class TowItem : Entity
    {
        protected const float size = 1f;
        protected const float speedFactor = 0.1f;

        public Ship LastShip = null;

        public TowItem() : base(string.Empty)
        {
        }

        protected override Fixture GetFixture(World world)
        {
            var fixture = FixtureFactory.CreateCircle(world, size, 1);
            fixture.Body.BodyType = BodyType.Dynamic;
            fixture.Body.LinearVelocity = new Vector2(((float)ScreamMath.Random.NextDouble() - 0.5f) * speedFactor, ((float)ScreamMath.Random.NextDouble() - 0.5f) * speedFactor);
            return fixture;
        }

        public override void Died()
        {
            base.Died();
            if (LastShip != null)
            {
                LastShip.DetachIfAttached(this);
            }
        }
    }
}
