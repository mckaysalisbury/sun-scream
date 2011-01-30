using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public class SetFactionCommand : Command
    {
        public override string Execute(Player source, string arguments)
        {
            int parsedInt;
            if (!Int32.TryParse(arguments, out parsedInt))
                return "Argument must be a number.";

            source.Faction = (Faction)parsedInt;

            return String.Format("Faction set to {0}", source.Faction);
        }
    }
}
