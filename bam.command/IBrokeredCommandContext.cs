using Bam.Command;

namespace Bam
{
    public interface IBrokeredCommandContext
    {
        IBrokeredCommandRunner CommandRunner { get; }
        string ContextName { get; }
        IDictionary<string, IBrokeredCommand> Commands { get; }
        string ResolveCommandSelector(string[] arguments);
        IBrokeredCommandResult Execute(ICommandBroker broker, string[] arguments);
    }
}
