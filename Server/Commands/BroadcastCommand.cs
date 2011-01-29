using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public class BroadcastCommand : Command
    {
        public override string Execute(Player source, string arguments)
        {
            foreach (var player in GameServer.Instance.Universe.Players)
                player.AddMessage(String.Format("{0}: {1}", source.Name, arguments), MessageType.System);

            return null;
        }
    }
}
