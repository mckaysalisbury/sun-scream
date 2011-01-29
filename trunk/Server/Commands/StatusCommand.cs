using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public class StatusCommand : Command
    {
        public override string Execute(Player source, string arguments)
        {
            return String.Format("Location {0} Velocity {1} Players {2} Entities {3}", source.Controlling.Position, source.Controlling.Fixture.Body.LinearVelocity, source.Controlling.Universe.Players.Count, source.Controlling.Universe.Entites.Count);
        }
    }
}
