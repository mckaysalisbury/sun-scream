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

        public static void ExecuteCommand(Player source, string message)
        {
            var args = message.Split(' ');
            if (args.Length > 0)
            {
                string commandName;
                if (args[0].StartsWith("/"))
                {
                    commandName = args[0].ToLower().TrimStart('/');
                    var index = message.IndexOf(' ');
                    if (index == -1)
                        message = String.Empty;
                    else
                        message = message.Substring(index + 1);
                }
                else
                    commandName = "broadcast";

                string result;
                if (!CommandsByName.ContainsKey(commandName))
                    result = "Unknown command";
                else
                {
                    var command = CommandsByName[commandName];
                    result = command.Execute(source, message);
                }

                if (!String.IsNullOrEmpty(result))
                    source.AddMessage(result, MessageType.System);
            }
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
