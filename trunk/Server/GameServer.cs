using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Timers;

namespace Server
{
    class GameServer
    {
        const int Port = 1701;

        TcpListener listener;

        Universe Universe;

        Timer timer;

        public static GameServer Instance { get; set; }

        public GameServer()
        {
            Instance = this;

            Universe = UniverseGenerator.Generate();

            var ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
            listener = new TcpListener(ipAddress, Port);
            listener.Start();

            timer = new Timer(100);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();

            //output.AppendText("Listening on TCP port " + Port);

            //timer.Tick += new EventHandler(timer_Tick);
            //timer.Enabled = true;
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
                Universe.Players.Add(new Player(newClient));

                //output.AppendText("\nConnection from " + newClient.Client.RemoteEndPoint.ToString());
                //output.ScrollToCaret();
            }
        }
    }
}
