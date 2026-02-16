namespace Bam.Command
{
    /// <summary>
    /// Defines a method for discovering and initializing the set of commands available in a context.
    /// </summary>
    public interface IBrokeredCommandInitializer
    {
        /// <summary>
        /// Discovers and returns the available commands as a dictionary keyed by command name.
        /// </summary>
        /// <returns>A dictionary mapping command names to their <see cref="IBrokeredCommand"/> instances.</returns>
        IDictionary<string, IBrokeredCommand> InitializeCommands();
    }
}
