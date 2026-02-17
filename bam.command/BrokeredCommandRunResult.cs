namespace Bam.Command
{
    /// <summary>
    /// Default implementation of <see cref="IBrokeredCommandRunResult"/> that stores the outcome of a brokered command execution.
    /// </summary>
    public class BrokeredCommandRunResult : IBrokeredCommandRunResult
    {
        /// <summary>
        /// Gets or sets a message describing the result of the command execution.
        /// </summary>
        public string Message
        {
            get;
            set;
        } = null!;

        /// <summary>
        /// Gets or sets a value indicating whether the command executed successfully.
        /// </summary>
        public bool Success
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the return value produced by the command execution.
        /// </summary>
        public object? Result
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the command that was executed.
        /// </summary>
        public string CommandName
        {
            get;
            set;
        } = null!;

        /// <summary>
        /// Gets or sets the arguments that were passed to the command.
        /// </summary>
        public string[] Arguments
        {
            get;
            set;
        } = null!;

    }
}
