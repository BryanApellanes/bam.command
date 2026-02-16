namespace Bam.Command
{
    /// <summary>
    /// Represents the result of a brokered command execution, containing the broker, context,
    /// command, and run result information.
    /// </summary>
    public class BrokeredCommand : IBrokeredCommandResult
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BrokeredCommand"/> with the specified broker, context, command, and execution result.
        /// </summary>
        /// <param name="broker">The command broker that executed the command.</param>
        /// <param name="context">The command context in which the command was executed.</param>
        /// <param name="command">The command that was executed, or null if no command was found.</param>
        /// <param name="executionResult">The result of the command execution, or null if execution did not occur.</param>
        public BrokeredCommand(ICommandBroker broker, IBrokeredCommandContext context, Bam.IBrokeredCommand? command, IBrokeredCommandRunResult? executionResult)
        {
            this.Broker = broker;
            this.Context = context;
            this.Command = command;
            this.RunResult = executionResult;
        }

        static IBrokeredCommandResult _empty;
        static object _emptyLock = new object();
        /// <summary>
        /// Gets a thread-safe singleton empty result with all null properties, used as a fallback when no command result is available.
        /// </summary>
        public static IBrokeredCommandResult Empty
        {
            get
            {
                return _emptyLock.DoubleCheckLock(ref _empty, () => new BrokeredCommand(null, null, null, null));
            }
        }

        /// <summary>
        /// Gets or sets the command broker that executed the command.
        /// </summary>
        public ICommandBroker Broker
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the command context in which the command was executed.
        /// </summary>
        public IBrokeredCommandContext Context
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the command that was executed, or null if no matching command was found.
        /// </summary>
        public Bam.IBrokeredCommand? Command
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether the command executed successfully, determined by the underlying run result.
        /// </summary>
        public bool Success
        {
            get
            {
                return RunResult?.Success == true;
            }
        }

        /// <summary>
        /// Gets or sets the detailed run result of the command execution.
        /// </summary>
        public IBrokeredCommandRunResult? RunResult
        {
            get;
            set;
        }
    }
}
