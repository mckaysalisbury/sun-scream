using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Server
{
    [DataContract]
    class DescendedObject : TestObject
    {
        [DataMember(Order=8)]
        public int Heath { get; set; }
    }
}