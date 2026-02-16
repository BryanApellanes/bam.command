namespace Bam.Command
{
    /// <summary>
    /// Defines operations for loading available command contexts and resolving a context name from arguments.
    /// </summary>
    public interface IBrokeredCommandContextResolver
    {
        /// <summary>
        /// Loads and returns all available command contexts.
        /// </summary>
        /// <returns>A dictionary mapping context names to their <see cref="IBrokeredCommandContext"/> instances.</returns>
        IDictionary<string, IBrokeredCommandContext> LoadContexts();

        /// <summary>
        /// Resolves the context name from the given command-line arguments.
        /// </summary>
        /// <param name="arguments">The command-line arguments.</param>
        /// <returns>The resolved context name.</returns>
        string ResolveContextName(string[] arguments);
    }
}
