using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Server
{
    public static class ScreamMath
    {
        public static Random Random = new Random();

        public static float Quadrance(Vector2 one, Vector2 two)
        {
            return (one.X - two.X) * (one.X - two.X) + (one.Y - two.Y) * (one.Y - two.Y);
        }
    }
}
