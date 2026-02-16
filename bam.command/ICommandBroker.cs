using Bam.Command;

namespace Bam
{
    /// <summary>
    /// Defines a command broker that resolves command contexts from arguments and executes commands,
    /// raising lifecycle events throughout the process.
    /// </summary>
    public interface ICommandBroker
    {
        /// <summary>
        /// Raised when command context initialization begins.
        /// </summary>
        event EventHandler<CommandBrokerEventArgs> InitializeStarted;

        /// <summary>
        /// Raised when command context initialization completes successfully.
        /// </summary>
        event EventHandler<CommandBrokerEventArgs> InitializeCompleted;

        /// <summary>
        /// Raised when command context initialization fails.
        /// </summary>
        event EventHandler<CommandBrokerEventArgs> InitializeFailed;

        /// <summary>
        /// Raised when context resolution begins.
        /// </summary>
        event EventHandler<CommandBrokerEventArgs> ResolveContextStarted;

        /// <summary>
        /// Raised when context resolution completes successfully.
        /// </summary>
        event EventHandler<CommandBrokerEventArgs> ResolveContextCompleted;

        /// <summary>
        /// Raised when context resolution fails.
        /// </summary>
        event EventHandler<CommandBrokerEventArgs> ResolveContextFailed;

        /// <summary>
        /// Raised when command brokering begins.
        /// </summary>
        event EventHandler<CommandBrokerEventArgs> BrokerCommandStarted;

        /// <summary>
        /// Raised when command brokering completes successfully.
        /// </summary>
        event EventHandler<CommandBrokerEventArgs> BrokerCommandCompleted;

        /// <summary>
        /// Raised when command brokering fails.
        /// </summary>
        event EventHandler<CommandBrokerEventArgs> BrokerCommandFailed;

        /// <summary>
        /// Gets the dictionary of available command contexts, keyed by context name.
        /// </summary>
        IDictionary<string, IBrokeredCommandContext> CommandContexts { get; }

        /// <summary>
        /// Resolves the appropriate context and executes the command for the given arguments.
        /// </summary>
        /// <param name="arguments">The command-line arguments.</param>
        /// <returns>The result of the command execution.</returns>
        IBrokeredCommandResult BrokerCommand(string[] arguments);

        /// <summary>
        /// Resolves the command context for the given arguments.
        /// </summary>
        /// <param name="arguments">The command-line arguments.</param>
        /// <returns>The resolved command context.</returns>
        IBrokeredCommandContext ResolveContext(string[] arguments);
    }
}
