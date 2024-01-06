using Bam.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
