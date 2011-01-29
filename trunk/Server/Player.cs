using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using Microsoft.Xna.Framework;

namespace Server
{
    public class Player
    {
        public TcpClient Client;

        public Ship Controlling { get; set; }
        public string Name { get; set; }
        public Universe Universe { get; set; }

        public Player(TcpClient client)
        {
            Client = client;

            GameServer.Instance.Log("Player Connected from " + client.Client.RemoteEndPoint);
        }

        public void Disconnect()
        {
            Client.Close();
            Universe.RemovePlayer(this);
        }

        int packetLength;

        public void Update()
        {
            if (!Client.Client.Connected)
            {
                Disconnect();
            }

            var update = CheckForUpdate();
            if (update != null)
            {
                if (update.Thrust != null && Controlling != null)
                {
                    var thrust = AngleToVector(update.Thrust.Angle / 100f, update.Thrust.Distance / 1000f);
                    Controlling.Fixture.Body.ApplyForce(thrust);
                    GameServer.Instance.Log(String.Format("Thrust {0}", thrust));
                }
            }
        }

        public static Vector2 AngleToVector(double angle, double distance)
        {
            return new Vector2((float)(distance * Math.Cos(angle)), (float)(distance * Math.Sin(angle)));
        }

        public Client.UpdateToServer CheckForUpdate()
        {
            if (Client.Client.Available >= 4)
            {
                var bytes = new byte[4];
                Client.Client.Receive(bytes);
                packetLength = BitConverter.ToInt32(bytes, 0);

                GameServer.Instance.Log(String.Format("Sent Length {0}  Available {1}", packetLength.ToString(), Client.Client.Available));

                if (Client.Client.Available < packetLength)
                    throw new NotImplementedException("partial packet received");

                var packetBytes = new byte[packetLength];
                Client.Client.Receive(packetBytes);
                return ProtoBuf.Serializer.Deserialize<Client.UpdateToServer>(new MemoryStream(packetBytes));
            }

            return null;
        }
    }
}
