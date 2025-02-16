namespace Bam.Command
{
    public interface ICommandArgumentParser
    {
        IParsedCommandArguments ParseCommandArguments(string[] arguments);
    }
}
