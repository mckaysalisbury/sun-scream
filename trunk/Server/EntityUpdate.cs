using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Server
{
    [DataContract]
    public class EntityUpdate
    {
        /// <summary>
        /// The (database) id of the entity
        /// </summary>
        [DataMember(Order = 1)]
        public int Id { get; set; }

        /// <summary>
        /// The name of the entity
        /// </summary>
        [DataMember(Order = 2)]
        public string Name { get; set; }

        /// <summary>
        /// The X coordinate of the entity
        /// </summary>
        [DataMember(Order=3)]
        public int LocationX { get; set; }
        /// <summary>
        /// The Y coordinate of the entity
        /// </summary>
        [DataMember(Order = 4)]
        public int LocationY { get; set; }

        [DataMember(Order = 5)]
        public EntityUpdateType Type { get; set; }

        [DataMember(Order = 6)]
        public int Angle { get; set; }
    }
}
