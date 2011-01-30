using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Server
{
    public enum EntityUpdateType
    {
        Invisible,
        BuilderShip,
        DestroyerShip,
        GuideShip,
        Planet,
        Asteroid,
        Hitchhiker,
    }
}
