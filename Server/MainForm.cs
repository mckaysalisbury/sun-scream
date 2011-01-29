using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using ProtoBuf;
using System.Net.Sockets;
using System.Net;

namespace Server
{
    public partial class MainForm : Form
    {
        const int Port = 1701;

        List<TcpClient> clients = new List<TcpClient>();
        TcpListener listener;

        public MainForm()
        {
            InitializeComponent();

            protoRichTextBox.Text = ProtoBuf.Serializer.GetProto<UpdatePacket>();

            var ipAddress = Dns.Resolve("localhost").AddressList[0];
            listener = new TcpListener(ipAddress, Port);
            listener.Start();

            output.AppendText("Listening on TCP port " + Port);

            timer.Tick += new EventHandler(timer_Tick);
            timer.Enabled = true;
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

                output.AppendText("\nConnection from " + newClient.Client.RemoteEndPoint.ToString());
                output.ScrollToCaret();
            }
        }

        void sendButton_Click(object sender, EventArgs e)
        {
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