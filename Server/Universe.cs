using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System.Timers;
using System.Net.Sockets;

namespace Server
{
    /// <summary>
    /// The whole game universe
    /// </summary>
    public class Universe
    {
        List<Entity> Entites = new List<Entity>();
        List<Player> Players = new List<Player>();

        World World;

        DateTime lastUpdate = DateTime.Now;

        Timer timer;

        /// <summary>
        /// Creates an instance of the Universe class
        /// </summary>
        public Universe()
        {
            World = new World(Vector2.Zero) { AutoClearForces = true };

            timer = new Timer(100);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.AutoReset = false;

            Update();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Update();
        }

        public void AddPlayer(Player player)
        {            
            Players.Add(player);
            player.Universe = this;
            player.Controlling = this.GenerateShip(player);
            AddEntity(player.Controlling, GetSpawnLocation());
        }

        public void RemovePlayer(Player player)
        {
            Entites.Remove(player.Controlling);
            Players.Remove(player);
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

        public void RemoveEntity(Entity entity)
        {
            Entites.Remove(entity);
        }

        void Update()
        {
            foreach (var entity in Entites)
            {
                entity.Update();

                //GameServer.Instance.Log(string.Format("{0}={1}@({2},{3})", entity.Id, entity.Name, entity.Position.X, entity.Position.Y));
            }

            //World.Step(10);
            World.Step((float)(DateTime.Now - lastUpdate).TotalMilliseconds);

            foreach (var player in Players.ToArray())
            {
                player.Update();

                if (player.Client.Client.Connected)
                {
                    var packet = new UpdatePacket() { ControllingEntityId = player.Controlling.Id };

                    GameServer.Instance.Log(string.Format("{0}", player.Controlling.Fixture.Body.LinearVelocity));

                    foreach (var entity in Entites)
                    {
                        packet.Entities.Add(new EntityUpdate() { Type = entity.GetClientType(), Id = entity.Id, LocationX = (int)(entity.Fixture.Body.Position.X * 1000000), LocationY = (int)(entity.Fixture.Body.Position.Y * 1000000) });
                    }

                    try
                    {
                        player.Client.Client.Send(packet.Serialize());
                    }
                    catch (SocketException)
                    {
                        player.Disconnect();
                    }
                }
            }

            lastUpdate = DateTime.Now;
            timer.Start();
        }

        private int nextId = 0;

        internal int GenerateId()
        {
            return nextId++;
        }
    }
}
