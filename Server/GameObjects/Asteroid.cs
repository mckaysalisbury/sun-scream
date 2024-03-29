﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Controllers;

namespace Server
{
    /// <summary>
    /// Stores information about a asteroid
    /// </summary>
    public class Asteroid : TowItem
    {
        internal override EntityUpdateType GetClientType()
        {
            return EntityUpdateType.Asteroid;
        }
    }
}
