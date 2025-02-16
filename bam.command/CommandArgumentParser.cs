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
