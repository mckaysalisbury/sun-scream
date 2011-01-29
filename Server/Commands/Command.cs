using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public abstract class Command
    {
        public string Name { get; set; }

        public abstract string Execute(Player source, string arguments);
    }
}
