using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Server
{
    public static class Commands
    {
        public static List<Command> CommandList = new List<Command>();
        public static Dictionary<String, Command> CommandsByName = new Dictionary<string, Command>();

        public static void ExecuteCommand(Player source, string command)
        {
            
        }

        public static void RegisterCommands(Assembly assembly)
        {
            foreach (Type t in assembly.GetTypes())
            {
                if (t.IsSubclassOf(typeof(Command)))
                {
                    ConstructorInfo constructor = t.GetConstructor(new Type[] { });
                    if (constructor != null)
                    {
                        Command command = (Command)constructor.Invoke(new object[] { });
                        CommandList.Add(command);
                        CommandsByName[command.Name.ToLower()] = command;
                    }
                }
            }
        }
    }
}
