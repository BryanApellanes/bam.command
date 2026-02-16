namespace Bam.Command
{
    /// <summary>
    /// Represents a brokered command backed by an external executable process.
    /// </summary>
    public class ProcessCommand : IBrokeredCommand
    {
        /// <summary>
        /// Initializes a new instance with the specified name and executable path.
        /// The command selector is set to the same value as the name.
        /// </summary>
        /// <param name="name">The name and selector for this command.</param>
        /// <param name="exePath">The file path to the executable.</param>
        public ProcessCommand(string name, string exePath)
        {
            this.CommandName = name;
            this.CommandSelector= name;
            this.ExePath= exePath;
        }

        /// <summary>
        /// Gets the name of this command.
        /// </summary>
        public string CommandName { get; private set; }

        /// <summary>
        /// Gets the selector used to match this command from arguments.
        /// </summary>
        public string CommandSelector { get; private set; }

        /// <summary>
        /// Gets the file path to the executable that this command runs.
        /// </summary>
        public string ExePath { get; private set; }
    }
}
