using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public class SetRoleCommand : Command
    {
        public override string Execute(Player source, string arguments)
        {
            if (source.Role == Role.Thrust)
                source.Role = Role.Tractor;
            else
                source.Role = Role.Thrust;
            
            return String.Format("Mode set to {0}", source.Role);
        }
    }
}
