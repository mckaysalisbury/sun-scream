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

        public Role Role { get; set; }

        Faction faction;
        public Faction Faction
        {
            get { return faction; }
            set
            {
                faction = value;
                UpdateFaction();
            }
        }

        public List<Message> Messages { get; set; }
        public List<Note> Notes { get; set; }

        public Player(TcpClient client)
        {
            Client = client;
            client.Client.NoDelay = true;

            Name = client.Client.RemoteEndPoint.ToString();

            Notes = new List<Note>();
            Messages = new List<Message>();

            GameServer.Instance.Log("Player Connected from " + client.Client.RemoteEndPoint);
        }

        public void Disconnect()
        {
            GameServer.Instance.Log("Client Disconnected");
            Client.Close();
            Client = null;
            Universe.RemovePlayer(this);
        }

        public void AddNote(int target, NoteType type)
        {
            Notes.Add(new Note() { Target = target, Type = type });
        }

        public void UpdateFaction()
        {
            if (Controlling != null)
            {
                Controlling.Die();
            }
            Controlling = Universe.GenerateShip(this, Faction);
        }

        DateTime deathTime = DateTime.MinValue;

        public void Update()
        {
            if (!Client.Client.Connected)
            {
                Disconnect();
                return;
            }

            if (Controlling != null && Controlling.Fixture == null)
            {
                deathTime = DateTime.Now;
                Controlling = null;
            }

            if (Controlling == null && deathTime < DateTime.Now - TimeSpan.FromSeconds(2))
                Respawn();

            var update = CheckForUpdate();
            if (update != null)
            {
                foreach (var message in update.Messages)
                {
                    Commands.ExecuteCommand(this, message);
                }

                if (update.Thrust != null && Controlling != null)
                {
                    var angle = update.Thrust.Angle / 100f;

                    switch (Role)
                    {
                        case Server.Role.Thrust:
                            var thrust = AngleToVector(angle, Controlling.Speed);
                            Controlling.Fixture.Body.ApplyForce(thrust);
                            Controlling.Fixture.Body.Rotation = angle;
                            Controlling.Fixture.Body.AngularVelocity = 0;
                            break;

                        case Server.Role.Tractor:
                            var result = Controlling.Tractor(angle, update.Thrust.Distance);
                            this.AddMessage(result, MessageType.System);
                            break;

                        default:
                            throw new NotImplementedException();
                    }

                    //GameServer.Instance.Log(String.Format("Angle {0} Distance {1}", update.Thrust.Angle, update.Thrust.Distance));
                    //GameServer.Instance.Log(String.Format("Thrust {0}", thrust));
                    //GameServer.Instance.Log(String.Format("Velocity {0}", Controlling.Fixture.Body.LinearVelocity));
                }
            }
        }

        void Respawn()
        {
            Universe.PlaySound(Sounds.Spawn);
            Universe.GenerateShip(this);
        }

        public static Vector2 AngleToVector(double angle, double distance)
        {
            return new Vector2((float)(distance * Math.Cos(angle)), (float)(distance * Math.Sin(angle)));
        }

        //int throwAwayBytes = 23;

        public Client.UpdateToServer CheckForUpdate()
        {
            //if (throwAwayBytes > 0)
            //{
            //    if (Client.Client.Available >= throwAwayBytes)
            //    {
            //        var throwAway = new byte[throwAwayBytes];
            //        Client.Client.Receive(throwAway);
            //        throwAwayBytes = 0;
            //    }
            //}

            if (Client.Client.Available >= 4)
            {
                var bytes = new byte[4];
                Client.Client.Receive(bytes);
                var packetLength = BitConverter.ToInt32(bytes, 0);

                if (packetLength > 100000 || packetLength < 0)
                {
                    GameServer.Instance.Log(String.Format("Invalid packet length {0}, Bytes were = {1}", packetLength, BitConverter.ToString(bytes)));

                    Disconnect();
                    return null;
                }

                if (packetLength > Client.Client.Available)
                    GameServer.Instance.Log(String.Format("Sent Length {0}  Available {1}", packetLength, Client.Client.Available));

                if (Client.Client.Available < packetLength)
                {
                    GameServer.Instance.Log("Partial packet recieved");

                    Disconnect();
                    return null;
                }

                var packetBytes = new byte[packetLength];
                Client.Client.Receive(packetBytes);
                return ProtoBuf.Serializer.Deserialize<Client.UpdateToServer>(new MemoryStream(packetBytes));
            }

            return null;
        }

        internal void AddMessage(string message, MessageType type)
        {
            Messages.Add(new Message() { Text = message, Type = type });
        }
    }
}
