using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public class SetNameCommand : Command
    {
        public override string Execute(Player source, string arguments)
        {
            source.Name = arguments;
            if (source.Controlling != null)
                source.Controlling.Name = source.Name;
            return String.Format("Name set to {0}", source.Name);
        }
    }
}
