using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Server
{
    public class Player
    {
        public TcpClient Client;

        public Ship Controlling { get; set; }

        public Player(TcpClient client)
        {
            Client = client;
        }
    }
}
