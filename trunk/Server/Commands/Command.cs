using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public abstract class Command
    {
        public string Name { get; set; }

        public Command()
        {
            Name = GetType().Name;
            if (Name.EndsWith("Command"))
                Name = Name.Substring(0, Name.Length - "Command".Length);
        }

        public abstract string Execute(Player source, string arguments);
    }
}
