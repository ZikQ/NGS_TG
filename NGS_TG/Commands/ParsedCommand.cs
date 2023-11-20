using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGS_TG.Commands
{
    public class ParsedCommand
    {
        public string CommandName { get; internal set; }
        public string[] Arguments { get; }

        public ParsedCommand(string commandName, string[] arguments)
        {
            CommandName = commandName;
            Arguments = arguments;
        }
        public static ParsedCommand ParseCommand(string input)
        {
            if (input.StartsWith('/'))
            {
                string[] parts = input.Substring(1).Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length >= 1)
                {
                    string commandName = parts[0];
                    string[] arguments = parts.Skip(1).ToArray();

                    return new ParsedCommand(commandName, arguments);
                }
            }

            return null;
        }
    }

    
}
