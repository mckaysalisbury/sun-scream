using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics.Joints;

namespace Server
{
    /// <summary>
    /// Stores information about a ship
    /// </summary>
    public class Ship : Entity
    {
        private const float size = 1f;
        private const float maxTractorQuadrance = 1000;

        public Ship(string name) : base(name)
        {
        }

        internal override EntityUpdateType GetClientType()
        {
            return EntityUpdateType.Ship;
        }

        protected override Fixture GetFixture(World world)
        {
            var fixture = FixtureFactory.CreateCircle(world, size, 1);
            fixture.Body.BodyType = BodyType.Dynamic;

            //fixture.Body.IsStatic = false;
            //fixture.Friction = 0;
            //fixture.Body.LinearDamping = 0;
            //fixture.Body.AngularDamping = 0;
            return fixture;
            //return FixtureFactory.CreateCircle(world,1, new Body( Width, Height, 1);
        }

        public Entity Tractor()
        {
            var tractorableEntities = this.Universe.Entites.Where((e) => e.IsTractorable);
            if (tractorableEntities.Any())
            {
                var minimum = tractorableEntities.Min((e) => ScreamMath.Quadrance(e.Position, this.Position));

                if (minimum > maxTractorQuadrance)
                {
                    // too far away for tractor
                    return null;
                }
                var closest = tractorableEntities.First((e) => ScreamMath.Quadrance(e.Position, this.Position) == minimum);
                var joint = new DistanceJoint(this.Fixture.Body, closest.Fixture.Body, Vector2.Zero, Vector2.Zero);
                joint.DampingRatio = 1;
                joint.Frequency = 25;
                joint.CollideConnected = true;
                joint.Length = (float)Math.Sqrt(maxTractorQuadrance);
                Universe.World.AddJoint(joint);
                //JointFactory.CreateRevoluteJoint(this.Fixture.Body, closest.Fixture.Body, Vector2.Zero);
                //JointFactory.CreateLineJoint(this.Fixture.Body, closest.Fixture.Body, Vector2.Zero, Vector2.Zero);

                return closest;
            }
            else
            {
                return null;
            }
        }
    }
}
