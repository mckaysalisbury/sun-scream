﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics.Contacts;

namespace Server
{
    /// <summary>
    /// An entity is an item that lives in the universe
    /// </summary>
    public abstract class Entity
    {
        static int nextId = 0;
        
        public Universe Universe { get; set; }

        public bool IsAlive { get { return Fixture != null; } }

        protected static int GenerateId()
        {
            return nextId++;
        }

        /// <summary>
        /// Creates an instance of the Entity Class, all these fields must be filled out.
        /// </summary>
        public Entity(string name)
        {
            this.Id = GenerateId();
            this.Name = name;
        }

        /// <summary>
        /// The (database) id of the entity
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the entity
        /// </summary>
        public string Name { get; set; }

        public Fixture Fixture { get; set; }

        public Vector2 Position
        {
            get { return Fixture.Body.Position; }
            set { Fixture.Body.Position = value; }
        }

        internal void CreateBody(World world)
        {
            Fixture = GetFixture(world);
            Fixture.UserData = this;
            Fixture.AfterCollision += new AfterCollisionEventHandler(CollisionEvent);
        }

        void CollisionEvent(Fixture geom1, Fixture geom2, Contact contacts)
        {
            var collidedWith = geom2.UserData as Entity;
            if (collidedWith != null)
            {
                CollidedWith(collidedWith);
            }                        
        }

        public void Die()
        {
            if (IsAlive)
            {
                Died();
                Universe.RemoveEntity(this);
            }
        }

        internal virtual void CollidedWith(Entity collidedWith)
        {

        }

        protected abstract Fixture GetFixture(World world);

        internal virtual void Update()
        {
        }

        internal abstract EntityUpdateType GetClientType();

        public virtual void Died()
        {
        }

        public static string DisplayString(string action, Entity result)
        {
            return string.Format("{0} #{1}={2}:{3}", action, result.Id, result.Name, result.GetClientType());
        }

    }
}
