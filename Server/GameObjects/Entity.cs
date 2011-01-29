using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Server
{
    [DataContract]
    public class Entity
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(Order=3)]
        public float LocationX { get; set; }
        [DataMember(Order = 4)]
        public float LocationY { get; set; }

        [DataMember(Order = 5)]
        public int Width { get; set; }
        [DataMember(Order = 6)]
        public int Height { get; set; }
    }
}
