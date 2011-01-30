using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class BuildCommand : Command
    {
        public override string Execute(Player source, string arguments)
        {
            bool success = source.Controlling.RemoveAndBuild();
            return (success ? "Built" : "Could not build");
        }
    }
}
