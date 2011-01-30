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
        private const float maxTractorQuadrance = 10000;
        private int maxTractors;
        private const int asteroidsNeededToBuild = 5;

        public Dictionary<Entity, Joint> TractoredItems = new Dictionary<Entity, Joint>();

        public Ship(string name, int maxTractors) : base(name)
        {
            this.maxTractors = maxTractors;
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
            //fixture.Body.BodyType = BodyType.Dynamic;

            fixture.Body.IsStatic = false;
            //fixture.Friction = 0;
            //fixture.Body.LinearDamping = 0;
            //fixture.Body.AngularDamping = 0;
            return fixture;
            //return FixtureFactory.CreateCircle(world,1, new Body( Width, Height, 1);
        }

        public Entity Release()
        {
            if (TractoredItems == null || TractoredItems.Count == 0)
            {
                return null;
            }
            else
            {
                var pair = TractoredItems.First();
                Remove(pair.Key);
                return pair.Key;
            }
        }

        private void Remove(Entity entity)
        {
            var joint = TractoredItems[entity];
            TractoredItems.Remove(entity);
            //Universe.World.RemoveJoint(joint);
        }

        public Entity Tractor(float angle, int distance)
        {
            var deltaPosition = new Vector2((float)(Math.Cos(angle) * distance), (float)(Math.Sin(angle) * distance));
            var positionToLookFrom = this.Position + deltaPosition;
            return TractorEntity(positionToLookFrom);
        }

        public void RemoveAndBuild()
        {
            var tractoredAsteroids = TractoredItems.Where((e) => e.Key.GetClientType() == EntityUpdateType.Asteroid).ToArray();
            if (tractoredAsteroids.Length >= asteroidsNeededToBuild)
            {
                int asteroidsNeededForThisPlanet = asteroidsNeededToBuild;
                foreach (var asteroidPair in tractoredAsteroids)
                {
                    Remove(asteroidPair.Key);
                    asteroidPair.Key.Die();

                    if (--asteroidsNeededForThisPlanet == 0)
                    {
                        BuildPlanet();
                        break;
                    }

                }
            }
        }

        public int nextPlanetIndex = 0;

        private void BuildPlanet()
        {
            Universe.AddEntity(new StarSystem((this.Name ?? string.Empty) + (nextPlanetIndex++).ToString()), BehindMe());
        }

        private Vector2 BehindMe()
        {
            const float behindAmount = 100f;
            var behindPosition = new Vector2(this.Position.X - (float)Math.Cos(this.Fixture.Body.Rotation) * behindAmount, this.Position.Y - (float)Math.Sin(this.Fixture.Body.Rotation) * behindAmount);
            return behindPosition;
        }

        public Entity Tractor()
        {
            var positionToLookFrom = this.Position;

            return TractorEntity(positionToLookFrom);
        }

        private Entity TractorEntity(Vector2 positionToLookFrom)
        {
            if (TractoredItems.Count >= maxTractors)
            {
                return null;
            }
            var currentPosition = this.Position;
            var tractorableEntities = this.Universe.Entites.Where((e) => e.IsTractorable);
            var availableTractorableEntities = tractorableEntities.Where((e) => e != this && !TractoredItems.ContainsKey(e));
            var nearEnoughAvailableTractorableEntities = availableTractorableEntities.Select((e) => new { Quadrance = ScreamMath.Quadrance(e.Position, currentPosition), Entity = e }).Where((p) => p.Quadrance < maxTractorQuadrance);
            if (nearEnoughAvailableTractorableEntities.Any())
            {
                var minimum = nearEnoughAvailableTractorableEntities.Min((p) => p.Quadrance);

                var closest = nearEnoughAvailableTractorableEntities.First((p) => p.Quadrance == minimum).Entity;
                var joint = new RopeJoint(this.Fixture.Body, closest.Fixture.Body, Vector2.Zero, Vector2.Zero);
                //joint.DampingRatio = 1;
                //joint.Frequency = 25;
                joint.CollideConnected = true;
                joint.MaxLength = (float)Math.Sqrt(maxTractorQuadrance);
                Universe.World.AddJoint(joint);
                TractoredItems.Add(closest, joint);
                if (closest is Asteroid)
                {
                    ((Asteroid)closest).LastShip = this;
                }
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
