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

        public List<Entity> TractoredItems = new List<Entity>();

        public Ship(string name) : base(name)
        {
        }

        internal override EntityUpdateType GetClientType()
        {
            return EntityUpdateType.Ship;
        }

        public override bool IsTractorable
        {
            get
            {
                return true;
            }
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

        public Entity Release()
        {

            return null;
        }
        public Entity Tractor()
        {
            var tractorableEntities = this.Universe.Entites.Where((e) => e.IsTractorable);
            var availableTractorableEntities = tractorableEntities.Where((e) => e != this && !TractoredItems.Contains(e));
            if (availableTractorableEntities.Any())
            {
                var minimum = availableTractorableEntities.Min((e) => ScreamMath.Quadrance(e.Position, this.Position));

                if (minimum > maxTractorQuadrance)
                {
                    // too far away for tractor
                    return null;
                }
                var closest = availableTractorableEntities.First((e) => ScreamMath.Quadrance(e.Position, this.Position) == minimum);
                var joint = new RopeJoint(this.Fixture.Body, closest.Fixture.Body, Vector2.Zero, Vector2.Zero);
                //joint.DampingRatio = 1;
                //joint.Frequency = 25;
                joint.CollideConnected = true;
                joint.MaxLength = (float)Math.Sqrt(maxTractorQuadrance);
                Universe.World.AddJoint(joint);
                TractoredItems.Add(closest);
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
