using Bam.Command;

namespace Bam
{
    /// <summary>
    /// Defines a named context containing a set of commands that can be resolved and executed.
    /// </summary>
    public interface IBrokeredCommandContext
    {
        /// <summary>
        /// Gets the runner responsible for executing commands in this context.
        /// </summary>
        IBrokeredCommandRunner CommandRunner { get; }

        /// <summary>
        /// Gets the name of this command context.
        /// </summary>
        string ContextName { get; }

        /// <summary>
        /// Gets the dictionary of commands available in this context, keyed by command name.
        /// </summary>
        IDictionary<string, IBrokeredCommand> Commands { get; }

        /// <summary>
        /// Resolves the command selector from the given arguments.
        /// </summary>
        /// <param name="arguments">The command-line arguments.</param>
        /// <returns>The resolved command selector string.</returns>
        string ResolveCommandSelector(string[] arguments);

        /// <summary>
        /// Resolves and executes the appropriate command based on the given arguments.
        /// </summary>
        /// <param name="broker">The command broker orchestrating the execution.</param>
        /// <param name="arguments">The command-line arguments.</param>
        /// <returns>The result of the command execution.</returns>
        IBrokeredCommandResult Execute(ICommandBroker broker, string[] arguments);
    }
}
