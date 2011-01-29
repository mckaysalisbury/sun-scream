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
        }

        public Ship GenerateShip(Player player)
        {
            Tuple<float, float> location = GetSpawnLocation();
            var ship = new Ship(this.GenerateId(), player.Name, location.Item1, location.Item2);
            return ship;
        }


        private Tuple<float, float> GetSpawnLocation()
        {
            return new Tuple<float, float>(0, 0);
        }


        public void AddEntity(Entity entity)
        {
            entity.CreateBody(World);
            Entites.Add(entity);
        }

        void Update()
        {
            foreach (var entity in Entites)
                entity.Update();

            World.Step((float)(DateTime.Now - lastUpdate).TotalMilliseconds);

            foreach (var player in Players)
            {
                var packet = new UpdatePacket();

                foreach (var entity in Entites)
                {
                    packet.Entites.Add(new EntityUpdate() { Type = entity.GetClientType(), Id = entity.Id, LocationX = entity.Fixture.Body.Position.X, LocationY = entity.Fixture.Body.Position.Y });
                }

                packet.Messages.Add(new Message() { Text = "Hello World", Type = MessageType.System });

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
