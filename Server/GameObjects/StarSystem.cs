using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    /// <summary>
    /// Stores information about a star system
    /// </summary>
    public class StarSystem : Entity
    {
        private const float size = 10f;
        public StarSystem(int id, string name, float x, float y) : base(id, name, x, y, size, size)
        {
        }
    }
}
