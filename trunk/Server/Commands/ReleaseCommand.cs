using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class ReleaseCommand : Command
    {
        public override string Execute(Player source, string arguments)
        {
            var result = source.Controlling.Release();
            if (result == null)
            {
                return "Nothing available to release";
            }
            else
            {
                return string.Format("Releasing #{0}={1}:{2}", result.Id, result.Name, result.GetClientType());
            }
        }
    }
}
