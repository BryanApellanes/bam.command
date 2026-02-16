namespace Bam
{
    /// <summary>
    /// Represents the detailed result of running a brokered command, including success status,
    /// message, result value, and the original arguments.
    /// </summary>
    public interface IBrokeredCommandRunResult
    {
        /// <summary>
        /// Gets a message describing the result of the command execution.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Gets a value indicating whether the command executed successfully.
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Gets the return value produced by the command execution.
        /// </summary>
        object Result { get; }

        /// <summary>
        /// Gets the name of the command that was executed.
        /// </summary>
        string CommandName { get; }

        /// <summary>
        /// Gets the arguments that were passed to the command.
        /// </summary>
        string[] Arguments { get; }
    }
}
