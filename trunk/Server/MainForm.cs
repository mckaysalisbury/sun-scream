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
using System.Runtime.Serialization;
using Server.Display;

namespace Server
{
    /// <summary>
    /// The main form for the server interface
    /// </summary>
    public partial class MainForm : Form
    {
        public static MainForm Instance { get; set; }
        public GameServer server;

        /// <summary>
        /// Creates an instance of the main form class.
        /// </summary>
        public MainForm()
        {
            Instance = this;

            InitializeComponent();

            protoRichTextBox.Text = ProtoBuf.Serializer.GetProto<UpdatePacket>().Replace("fixed32", "float"); // this replace fixes a bug in the serializer

            server = new GameServer();
        }

        [DataContract]
        class Tester
        {
            [DataMember(Order=1)]
            public float TestFloat { get; set; }
        }

        internal void Log(string message)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => Log(message)));
                }
                else
                {
                    Output.AppendText(message + Environment.NewLine);
                    Output.ScrollToCaret();
                }
            }
            catch
            {
                // I don't care about exceptions in log
            }
        }

        private void watchButton_Click(object sender, EventArgs e)
        {
            var displayForm = new DisplayForm();
            displayForm.Universe = server.Universe;
            displayForm.Show();
        }
    }
}