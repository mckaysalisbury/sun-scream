﻿using System;
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
    /// Stores information about a planet
    /// </summary>
    public class Planet : Entity
    {
        private const float size = 50f;
        public Planet(string name) : base(name)
        {

        }

        internal override EntityUpdateType GetClientType()
        {
            return EntityUpdateType.Planet;
        }

        GravityController gravity;

        protected override Fixture GetFixture(World world)
        {
            var fixture = FixtureFactory.CreateCircle(world, size, 1);

            gravity = new GravityController(.01f);
            gravity.AddBody(fixture.Body);
            gravity.GravityType = GravityType.DistanceSquared;

            world.AddController(gravity);
            return fixture;
        }

        internal override void CollidedWith(Entity collidedWith)
        {
            if (collidedWith is Asteroid)
            {
                Die();
                var ship = ((Asteroid)collidedWith).LastShip;
                if (ship != null && ship.Faction == Faction.Destroyers)
                {
                    GameServer.Instance.Broadcast(String.Format("{0} scored by destroying a planet!", ship.Name));
                    Score.Give(ship);
                }
            }
            if (collidedWith is Hitchhiker)
            {
                var ship = ((Hitchhiker)collidedWith).LastShip;
                if (ship != null && ship.Faction == Faction.Guides)
                {
                    GameServer.Instance.Broadcast(String.Format("{0} scored by landing a hitchhiker!", ship.Name));
                    Score.Give(ship);
                }
            }
            collidedWith.Die();
        }

        public override void Died()
        {
            Universe.PlaySound(Sounds.PlanetExplodes);

            Universe.World.RemoveController(gravity);

            Universe.AddEntity(new Asteroid(), Position);
            Universe.AddEntity(new Asteroid(), Position);
            Universe.AddEntity(new Asteroid(), Position);
            Universe.AddEntity(new Asteroid(), Position);
            Universe.AddEntity(new Asteroid(), Position);
            Universe.AddEntity(new Asteroid(), Position);

            Universe.AddEntity(new Hitchhiker(), Position);
            Universe.AddEntity(new Hitchhiker(), Position);
            Universe.AddEntity(new Hitchhiker(), Position);
        }
    }
}
