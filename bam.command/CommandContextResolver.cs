using Bam.Command;

namespace Bam
{
    /// <summary>
    /// Abstract base class for resolving command contexts. Uses the first argument as the context name
    /// or falls back to the default context when no arguments are provided.
    /// </summary>
    public abstract class CommandContextResolver : IBrokeredCommandContextResolver
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CommandContextResolver"/>.
        /// </summary>
        public CommandContextResolver()
        {
        }

        /// <summary>
        /// When overridden in a derived class, loads and returns all available command contexts.
        /// </summary>
        /// <returns>A dictionary mapping context names to their <see cref="IBrokeredCommandContext"/> instances.</returns>
        public abstract IDictionary<string, IBrokeredCommandContext> LoadContexts();

        /// <summary>
        /// Resolves the context name from the given arguments. Returns the first argument as the context name,
        /// or <see cref="DefaultCommandContext.Name"/> if no arguments are provided.
        /// </summary>
        /// <param name="arguments">The command-line arguments to resolve from.</param>
        /// <returns>The resolved context name.</returns>
        public string ResolveContextName(string[] arguments)
        {
            if(arguments == null || arguments.Length == 0)
            {
                return DefaultCommandContext.Name;
            }

            return arguments[0];
        }
    }
}
