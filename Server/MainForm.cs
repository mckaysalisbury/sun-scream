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
    /// <summary>
    /// The main form for the server interface
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Creates an instance of the main form class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            protoRichTextBox.Text = ProtoBuf.Serializer.GetProto<UpdatePacket>();

        }

    }
}