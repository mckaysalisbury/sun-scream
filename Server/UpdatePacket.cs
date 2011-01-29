using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Server
{
    [DataContract]
    class UpdatePacket
    {
        [DataMember(Order = 3)]
        public int ControlingEntityId { get; set; }

        [DataMember(Order = 1)]
        public List<Entity> Entites { get; set; }

        [DataMember(Order = 2)]
        public List<Message> Messages { get; set; }
    }
}
