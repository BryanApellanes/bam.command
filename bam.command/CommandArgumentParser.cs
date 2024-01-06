using Bam.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class CommandArgumentParser : ICommandArgumentParser
    {
        public CommandArgumentParser() { }

        public IParsedCommandArguments ParseCommandArguments(string[] arguments)
        {
            return new ParsedCommandArguments(arguments);
        }
    }
}
