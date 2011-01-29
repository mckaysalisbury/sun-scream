using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class TractorCommand : Command
    {
        public override string Execute(Player source, string arguments)
        {
            var result = source.Controlling.Tractor();
            if (result == null)
            {
                return "No asteroids nearby";
            }
            else
            {               
                return string.Format("Tractoring #{0}={1}", result.Id, result.Name);
            }
        }
    }
}
