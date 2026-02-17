namespace Bam.Command
{
    /// <summary>
    /// Represents a command run result that indicates failure due to an exception.
    /// Always reports <see cref="Success"/> as false and provides the exception as the result.
    /// </summary>
    public class RunExceptionResult : IBrokeredCommandRunResult
    {
        /// <summary>
        /// Initializes a new instance with the specified command name and exception.
        /// </summary>
        /// <param name="commandName">The name of the command that failed.</param>
        /// <param name="exception">The exception that caused the failure.</param>
        public RunExceptionResult(string commandName, Exception exception)
        {
            this.CommandName = commandName;
            this.Exception = exception;
        }

        /// <summary>
        /// Gets the exception that caused the command failure.
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Gets the exception message as the result message.
        /// </summary>
        public string Message
        {
            get
            {
                return Exception.Message;
            }
        }

        /// <summary>
        /// Gets a value indicating success; always returns false for exception results.
        /// </summary>
        public bool Success
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the exception as the result value.
        /// </summary>
        public object Result
        {
            get
            {
                return this.Exception;
            }
        }


        /// <summary>
        /// Gets the name of the command that failed.
        /// </summary>
        public string CommandName
        {
            get;
            private set;
        }

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
