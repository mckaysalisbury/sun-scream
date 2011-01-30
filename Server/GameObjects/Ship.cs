using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics.Joints;
using System.Diagnostics;

namespace Server
{
    /// <summary>
    /// Stores information about a ship
    /// </summary>
    public class Ship : Entity
    {
        private const float size = 2f;

        const int asteroidsNeededToBuild = 5;

        public bool CanBuild { get; set; }
        public float Speed { get; set; }
        public float TractorQuadrance { get; set; }
        public int TowMax { get; set; }

        public Dictionary<Entity, Joint> TractoredItems = new Dictionary<Entity, Joint>();

        public Ship(string name, Faction faction)
            : base(name)
        {
            this.Faction = faction;
            TractorQuadrance = 10000;
        }

        public EntityUpdateType TractorType { get; set; }

        internal override EntityUpdateType GetClientType()
        {
            switch (Faction)
            {
                case Server.Faction.Builders:
                    return EntityUpdateType.BuilderShip;
                case Server.Faction.Destroyers:
                    return EntityUpdateType.DestroyerShip;
                case Server.Faction.Guides:
                    return EntityUpdateType.GuideShip;
                default:
                    throw new NotImplementedException();
            }
        }

        protected override Fixture GetFixture(World world)
        {
            var fixture = FixtureFactory.CreateCircle(world, size, 1);
            fixture.Body.IsStatic = false;
            return fixture;
        }

        public string Detach()
        {
            if (TractoredItems == null || TractoredItems.Count == 0)
            {
                return "Nothing to detach.";
            }
            else
            {
                var pair = TractoredItems.First();
                Detach(pair.Key);

                Universe.PlaySound(Sounds.Pop);

                return Entity.DisplayString("Detached", pair.Key);
            }
        }

        private void Detach(Entity entity)
        {
            var joint = TractoredItems[entity];
            TractoredItems.Remove(entity);
            Universe.World.RemoveJoint(joint);
        }

        public string Tractor(float angle, int distance)
        {
            var deltaPosition = new Vector2((float)(Math.Cos(angle) * distance), (float)(Math.Sin(angle) * distance));
            var positionToLookFrom = this.Position + deltaPosition;
            return TractorEntity(positionToLookFrom);
        }

        public string RemoveAndBuild()
        {
            if (Faction == Server.Faction.Builders)
            {
                var tractoredAsteroids = TractoredItems.ToArray();
                if (tractoredAsteroids.Length >= asteroidsNeededToBuild)
                {
                    int asteroidsNeededForThisPlanet = asteroidsNeededToBuild;
                    foreach (var asteroidPair in tractoredAsteroids)
                    {
                        Detach(asteroidPair.Key);
                        asteroidPair.Key.Die();

                        if (--asteroidsNeededForThisPlanet == 0)
                        {
                            BuildPlanet();
                            return "Planet built";
                        }
                    }
                    Debug.Fail("This shouldn't ever happen, I was told that there was enough, but couldn't find enough.");
                }
                return "Not enough asteroids.";
            }
            return "Only the builders can build a new star.";
        }

        public int nextPlanetIndex = 0;

        private void BuildPlanet()
        {
            Universe.PlaySound(Sounds.Spawn);
            Universe.AddEntity(new Planet((this.Name ?? string.Empty) + (nextPlanetIndex++).ToString()), BehindMe());
        }

        private Vector2 BehindMe()
        {
            float behindAmount = (float)Math.Sqrt(this.TractorQuadrance) * 2;
            var behindPosition = new Vector2(this.Position.X - (float)Math.Cos(this.Fixture.Body.Rotation) * behindAmount, this.Position.Y - (float)Math.Sin(this.Fixture.Body.Rotation) * behindAmount);
            return behindPosition;
        }

        public string Tractor()
        {
            var positionToLookFrom = this.Position;

            return TractorEntity(positionToLookFrom);
        }

        private string TractorEntity(Vector2 positionToLookFrom)
        {
            if (TractoredItems.Count >= TowMax)
            {
                return "All tractor beams in use.";
            }
            var currentPosition = this.Position;
            var tractorableEntities = this.Universe.Entites.Where((e) => e.GetClientType() == TractorType);
            var availableTractorableEntities = tractorableEntities.Where((e) => e != this && !TractoredItems.ContainsKey(e));
            var nearEnoughAvailableTractorableEntities = availableTractorableEntities.Select((e) => new { Quadrance = ScreamMath.Quadrance(e.Position, currentPosition), Entity = e }).Where((p) => p.Quadrance < TractorQuadrance);
            if (nearEnoughAvailableTractorableEntities.Any())
            {
                var minimum = nearEnoughAvailableTractorableEntities.Min((p) => p.Quadrance);

                var closest = nearEnoughAvailableTractorableEntities.First((p) => p.Quadrance == minimum).Entity;
                var joint = new RopeJoint(this.Fixture.Body, closest.Fixture.Body, Vector2.Zero, Vector2.Zero);
                //joint.DampingRatio = 1;
                //joint.Frequency = 25;
                joint.CollideConnected = true;
                joint.MaxLength = (float)Math.Sqrt(TractorQuadrance);
                Universe.World.AddJoint(joint);
                TractoredItems.Add(closest, joint);
                if (closest is TowItem)
                {
                    ((TowItem)closest).LastShip = this;
                }
                //JointFactory.CreateRevoluteJoint(this.Fixture.Body, closest.Fixture.Body, Vector2.Zero);
                //JointFactory.CreateLineJoint(this.Fixture.Body, closest.Fixture.Body, Vector2.Zero, Vector2.Zero);

                Universe.PlaySound(Sounds.Pop);

                return Entity.DisplayString("Tractored", closest);
            }
            else
            {
                return "Nothing within range.";
            }
        }

        public override void Died()
        {
            base.Died();
            Universe.PlaySound(Sounds.ShipExplodes);
        }

        private Faction faction;
        public Faction Faction
        {
            get
            {
                return faction;
            }
            private set
            {
                faction = value;
                switch (faction)
                {
                    case Server.Faction.Destroyers:
                        TowMax = 1;
                        Speed = 0.08f;
                        TractorType = EntityUpdateType.Asteroid;
                        break;
                    case Server.Faction.Builders:
                        TowMax = 10;
                        Speed = 0.08f;
                        TractorType = EntityUpdateType.Asteroid;
                        break;
                    case Server.Faction.Guides:
                        TowMax = 1;
                        Speed = 0.12f;
                        TractorType = EntityUpdateType.Hitchhiker;
                        break;
                }
            }
        }
    }
}
