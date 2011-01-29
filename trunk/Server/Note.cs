using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Server
{
    [DataContract]
    public class Note
    {
        [DataMember(Order = 1)]
        public int Target { get; set; }

        [DataMember(Order = 2)]
        public NoteType Type { get; set; }
    }
}
