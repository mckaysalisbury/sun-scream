using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System.Timers;

namespace Server
{
    /// <summary>
    /// The whole game universe
    /// </summary>
    public class Universe
    {
        public List<Entity> Entites = new List<Entity>();
        public List<Player> Players = new List<Player>();

        World World;

        DateTime lastUpdate = DateTime.Now;

        Timer timer;

        /// <summary>
        /// Creates an instance of the Universe class
        /// </summary>
        public Universe()
        {
            World = new World(Vector2.Zero);

            Update();

            timer = new Timer(100);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Update();
        }

        public void AddPlayer(Player player)
        {            
            Players.Add(player);
            player.Controlling = this.GenerateShip(player);
            Entites.Add(player.Controlling);
        }

        public Ship GenerateShip(Player player)
        {
            var ship = new Ship(this.GenerateId(), player.Name);
            return ship;
        }

        public Vector2 GetSpawnLocation()
        {
            return new Vector2(0, 0);
        }

        public void AddEntity(Entity entity, Vector2 position)
        {
            entity.CreateBody(World);
            entity.Position = position;
            Entites.Add(entity);
        }

        void Update()
        {
            foreach (var entity in Entites)
            {
                entity.Update();

                GameServer.Instance.Log(string.Format("{0}={1}@({2},{3})", entity.Id, entity.Name, entity.Position.X, entity.Position.Y));
            }

            World.Step((float)(DateTime.Now - lastUpdate).TotalMilliseconds);            

            foreach (var player in Players)
            {
                var packet = new UpdatePacket();

                foreach (var entity in Entites)
                {
                    packet.Entities.Add(new EntityUpdate() { Type = entity.GetClientType(), Id = entity.Id, LocationX = (int)(entity.Fixture.Body.Position.X * 100), LocationY = (int)(entity.Fixture.Body.Position.Y * 100) });
                }

                player.Client.Client.Send(packet.Serialize());
            }

            lastUpdate = DateTime.Now;
        }

        private int nextId = 0;

        internal int GenerateId()
        {
            return nextId++;
        }
    }
}
