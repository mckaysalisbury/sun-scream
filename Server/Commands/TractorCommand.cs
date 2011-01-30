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
            if (source.Controlling != null)
                return source.Controlling.Tractor();
            return null;
        }
    }
}
