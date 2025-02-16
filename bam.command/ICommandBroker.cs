using Bam.Command;

namespace Bam
{
    public interface ICommandBroker
    {
        event EventHandler<CommandBrokerEventArgs> InitializeStarted;
        event EventHandler<CommandBrokerEventArgs> InitializeCompleted;
        event EventHandler<CommandBrokerEventArgs> InitializeFailed;

        event EventHandler<CommandBrokerEventArgs> ResolveContextStarted;
        event EventHandler<CommandBrokerEventArgs> ResolveContextCompleted;
        event EventHandler<CommandBrokerEventArgs> ResolveContextFailed;

        event EventHandler<CommandBrokerEventArgs> BrokerCommandStarted;
        event EventHandler<CommandBrokerEventArgs> BrokerCommandCompleted;
        event EventHandler<CommandBrokerEventArgs> BrokerCommandFailed;

        IDictionary<string, IBrokeredCommandContext> CommandContexts { get; }

        IBrokeredCommandResult BrokerCommand(string[] arguments);
        IBrokeredCommandContext ResolveContext(string[] arguments);
    }
}
