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
        public Ship()
        {
            Width = 0.1f;
            Height = 0.1f;
        }
    }
}
