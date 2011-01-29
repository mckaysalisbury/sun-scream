using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Server
{
    class GameServer
    {
        const int Port = 1701;

        List<TcpClient> clients = new List<TcpClient>();
        TcpListener listener;

        Universe Universe;

        public GameServer()
        {
            Universe = UniverseGenerator.Generate();

            var ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
            listener = new TcpListener(ipAddress, Port);
            listener.Start();

            //output.AppendText("Listening on TCP port " + Port);

            //timer.Tick += new EventHandler(timer_Tick);
            //timer.Enabled = true;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            CheckForNewConnections();

            lock (clients)
            {
                if (clients.Count > 0)
                {
                    //var bytes = SerializeTestData();

                    //foreach (var client in clients)
                    //{
                    //    client.Client.Send(bytes);
                    //}
                }
            }
        }

        void CheckForNewConnections()
        {
            if (listener.Pending())
            {
                var newClient = listener.AcceptTcpClient();
                lock (clients)
                {
                    clients.Add(newClient);
                }

                //output.AppendText("\nConnection from " + newClient.Client.RemoteEndPoint.ToString());
                //output.ScrollToCaret();
            }
        }
    }
}
