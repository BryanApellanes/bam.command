namespace Bam
{
    /// <summary>
    /// Represents a command that can be resolved and executed by a command broker.
    /// </summary>
    public interface IBrokeredCommand
    {
        /// <summary>
        /// Gets the display name of the command.
        /// </summary>
        string CommandName { get; }

        /// <summary>
        /// Gets the selector string used to match this command from command-line arguments.
        /// </summary>
        string CommandSelector { get; }
    }
}
