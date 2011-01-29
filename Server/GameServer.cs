using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Timers;
using System.Reflection;

namespace Server
{
    public class GameServer
    {
        const int Port = 1701;

        TcpListener listener;

        public Universe Universe { get; private set; }

        Timer timer;

        public static GameServer Instance { get; set; }

        public GameServer()
        {
            Instance = this;

            Commands.RegisterCommands(Assembly.GetExecutingAssembly());

            Universe = UniverseGenerator.GenerateUniverse();

            var ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
            listener = new TcpListener(ipAddress, Port);
            listener.Start();

            timer = new Timer(100);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();

            Log("Listening on TCP port " + Port);
        }

        public void Broadcast(string message)
        {
            foreach (var player in GameServer.Instance.Universe.Players)
                player.AddMessage(message, MessageType.System);
        }

        public void Log(string message)
        {
            MainForm.Instance.Log(message);
            Broadcast(message);
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CheckForNewConnections();
        }

        void CheckForNewConnections()
        {
            if (listener.Pending())
            {
                var newClient = listener.AcceptTcpClient();
                Universe.AddPlayer(new Player(newClient));

                //output.AppendText("\nConnection from " + newClient.Client.RemoteEndPoint.ToString());
                //output.ScrollToCaret();
            }
        }
    }
}
