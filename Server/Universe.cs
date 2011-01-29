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
            player.Controlling = new Ship();
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
                //player.Client.S

            }

            lastUpdate = DateTime.Now;



            //var bytes = SerializeTestData();
            //lock (clients)
            //{
            //    foreach (var client in clients)
            //    {

            //        var packet = new UpdatePacket();
            //        packet.Messages.Add(new Message() { Text = "Hello World", Type = MessageType.System });

            //        client.Client.Send(bytes);
            //    }
            //}
        }

        //byte[] SerializeTestData()
        //{
        //    using (var stream = new MemoryStream())
        //    {
        //        Serializer.SerializeWithLengthPrefix(stream, propertyGrid1.SelectedObject as TestObject, PrefixStyle.Fixed32);

        //        var bytes = new byte[stream.Length];
        //        stream.Position = 0;
        //        stream.Read(bytes, 0, (int)stream.Length);

        //        output.AppendText("\nSerialized to: " + BitConverter.ToString(bytes));
        //        output.ScrollToCaret();

        //        return bytes;
        //    }
        //}
    }
}
