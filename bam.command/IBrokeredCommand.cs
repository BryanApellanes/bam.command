namespace Bam.Command
{
    public interface IBrokeredCommand
    {
        ICommandBroker Broker { get; }
        ICommandContext Context { get; }
        ICommand Command { get; }
        bool Success { get; }
        ICommandExecutionResult ExecutionResult { get; }
    }
}
