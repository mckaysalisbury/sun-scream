using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Server
{
    [DataContract]
    class Child
    {
        [DataMember(Order=1)]
        public string Name { get; set; }
    }
}
