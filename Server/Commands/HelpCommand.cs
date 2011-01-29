using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public class HelpCommand : Command
    {
        public override string Execute(Player source, string arguments)
        {
            var result = new StringBuilder();

            foreach (var command in Commands.CommandList)
            {
                result.AppendFormat("\n/{0}", command.Name);
            }

            return result.ToString();
        }
    }
}
