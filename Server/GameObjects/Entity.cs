using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Server
{
    /// <summary>
    /// An entity is an item that lives in the universe
    /// </summary>
    [DataContract]
    public class Entity
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
        public float LocationX { get; set; }
        /// <summary>
        /// The Y coordinate of the entity
        /// </summary>
        [DataMember(Order = 4)]
        public float LocationY { get; set; }

        /// <summary>
        /// the Width of the entity
        /// </summary>
        [DataMember(Order = 5)]
        public int Width { get; set; }
        /// <summary>
        /// The Height of the entity
        /// </summary>
        [DataMember(Order = 6)]
        public int Height { get; set; }
    }
}
