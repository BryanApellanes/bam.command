namespace Bam.Command
{
    /// <summary>
    /// Provides event data for command broker lifecycle events, including initialization, context resolution, and command execution.
    /// </summary>
    public class CommandBrokerEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the command-line arguments associated with the event.
        /// </summary>
        public string[] Arguments { get; set; } = null!;

        /// <summary>
        /// Gets or sets the command broker that raised the event.
        /// </summary>
        public ICommandBroker CommandBroker { get; set; } = null!;

        /// <summary>
        /// Gets or sets the resolved command context, if available.
        /// </summary>
        public IBrokeredCommandContext CommandContext { get; set; } = null!;

        /// <summary>
        /// Gets or sets the command execution result, if available.
        /// </summary>
        public IBrokeredCommandResult Command { get; set; } = null!;

        /// <summary>
        /// Gets or sets the exception that occurred during the operation, if any.
        /// </summary>
        public Exception Exception { get; set; } = null!;
    }
}
