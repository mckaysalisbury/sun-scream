using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    /// <summary>
    /// Stores information about a ship
    /// </summary>
    public class Ship : Entity
    {
        private const float size = 0.1f;

        public Ship(int id, string name, float x, float y) : base(id, name, x, y, size, size)
        {
        }

        internal override ClientEntityType GetClientType()
        {
            return ClientEntityType.Ship;
        }
    }
}
