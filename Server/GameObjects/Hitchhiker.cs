using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace Server
{
    class Hitchhiker : TowItem
    {
        internal override EntityUpdateType GetClientType()
        {
            return EntityUpdateType.Hitchhiker;
        }

        internal override void CollidedWith(Entity collidedWith)
        {
            Universe.PlaySound(Sounds.Reentry);
            base.CollidedWith(collidedWith);
        }
    }
}
