namespace Bam.Command
{
    public interface IBrokeredCommandResult
    {
        ICommandBroker Broker { get; }
        IBrokeredCommandContext Context { get; }
        Bam.IBrokeredCommand Command { get; }
        bool Success { get; }
        IBrokeredCommandRunResult RunResult { get; }
    }
}
