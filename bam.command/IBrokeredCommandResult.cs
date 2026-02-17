namespace Bam.Command
{
    /// <summary>
    /// Represents the complete result of a brokered command execution, including the broker, context,
    /// command, success status, and detailed run result.
    /// </summary>
    public interface IBrokeredCommandResult
    {
        /// <summary>
        /// Gets the command broker that executed the command.
        /// </summary>
        ICommandBroker Broker { get; }

        /// <summary>
        /// Gets the command context in which the command was executed.
        /// </summary>
        IBrokeredCommandContext Context { get; }

        /// <summary>
        /// Gets the command that was executed.
        /// </summary>
        Bam.IBrokeredCommand? Command { get; }

        /// <summary>
        /// Gets a value indicating whether the command executed successfully.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Gets the detailed run result of the command execution.
        /// </summary>
        IBrokeredCommandRunResult? RunResult { get; }
    }
}
