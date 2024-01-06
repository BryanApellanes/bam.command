using Bam.Net;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public abstract class CommandBroker : Loggable, ICommandBroker
    {
        public CommandBroker(IBrokeredCommandContextResolver commandContextResolver, ILogger? logger = null)
        {
            this.CommandContextResolver = commandContextResolver;
            this.Logger = logger ?? Log.Default;
            this.Initialize();
        }

        protected IBrokeredCommandContextResolver CommandContextResolver { get; private set; }

        public virtual IDictionary<string, IBrokeredCommandContext> CommandContexts 
        {
            get; 
            private set; 
        }

        public ILogger Logger
        {
            get;
            private set;
        }

        public event EventHandler<CommandBrokerEventArgs> InitializeStarted;

        public event EventHandler<CommandBrokerEventArgs> InitializeCompleted;
        public event EventHandler<CommandBrokerEventArgs> InitializeFailed;
        
        public event EventHandler<CommandBrokerEventArgs> ResolveContextStarted;
        public event EventHandler<CommandBrokerEventArgs> ResolveContextCompleted;
        public event EventHandler<CommandBrokerEventArgs> ResolveContextFailed;

        public event EventHandler<CommandBrokerEventArgs> BrokerCommandStarted;
        public event EventHandler<CommandBrokerEventArgs> BrokerCommandCompleted;
        public event EventHandler<CommandBrokerEventArgs> BrokerCommandFailed;

        public IBrokeredCommandResult BrokerCommand(string[] arguments)
        {
            try
            {
                BrokerCommandStarted?.Invoke(this, new CommandBrokerEventArgs()
                {
                    CommandBroker = this,
                    Arguments = arguments
                });

                IBrokeredCommandContext? commandContext = TryResolveContext(arguments);
                if(commandContext != null)
                {
                    IBrokeredCommandResult command = commandContext.Execute(arguments);

                    if (command != null)
                    {
                        BrokerCommandCompleted?.Invoke(this, new CommandBrokerEventArgs()
                        {
                            CommandBroker = this,
                            Arguments = arguments,
                            Command = command
                        });
                    }

                    return command ?? BrokeredCommand.Empty;
                }

                return BrokeredCommand.Empty;
            }
            catch (Exception ex)
            {
                BrokerCommandFailed?.Invoke(this, new CommandBrokerEventArgs()
                {
                    CommandBroker = this,
                    Arguments = arguments,
                    Exception = ex
                });
                return BrokeredCommand.Empty;
            }
        }

        private IBrokeredCommandContext? TryResolveContext(string[] arguments)
        {
            try
            {
                return ResolveContext(arguments);
            }
            catch (Exception ex)
            {
                ResolveContextFailed?.Invoke(this, new CommandBrokerEventArgs()
                {
                    CommandBroker = this,
                    Arguments = arguments,
                    Exception = ex
                });
            }
            return null;
        }

        public virtual IBrokeredCommandContext ResolveContext(string[] arguments)
        {
            Info("Resolving context: arguments='{0}'", string.Join(" ", arguments));

            ResolveContextStarted?.Invoke(this, new CommandBrokerEventArgs()
            {
                CommandBroker = this,
                Arguments = arguments
            });

            string contextName = CommandContextResolver.ResolveContextName(arguments);
            if (contextName.Equals(DefaultCommandContext.Name) && BrokeredCommandContext.Default != null)
            {
                BrokeredCommandContext.Default.Broker = this;
                return BrokeredCommandContext.Default;
            }

            if (CommandContexts.ContainsKey(contextName))
            {
                IBrokeredCommandContext context =  CommandContexts[contextName];
                Info("Resolved context '{0}'", contextName);

                ResolveContextCompleted?.Invoke(this, new CommandBrokerEventArgs()
                {
                    CommandBroker = this,
                    CommandContext = context,
                    Arguments = arguments
                });
                return context;
            }

            throw new ArgumentException($"Context not found, specified arguments were: '{string.Join(' ', arguments)}'");
        }

        protected void Initialize()
        {
            try
            {
                // TODO: review the best way to switch the logger subscription on and off for diagnostics
                // this.Subscribe(this.Logger);
                InitializeStarted?.Invoke(this, new CommandBrokerEventArgs()
                {
                    CommandBroker = this
                });

                this.CommandContexts = this.CommandContextResolver.LoadContexts();

                InitializeCompleted?.Invoke(this, new CommandBrokerEventArgs()
                {
                    CommandBroker = this
                });
            }
            catch (Exception ex)
            {
                Error("Failed to load contexts: {0}", ex.Message);
                InitializeFailed?.Invoke(this, new CommandBrokerEventArgs()
                {
                    CommandBroker = this,
                    Exception = ex
                });
            }
        }
    }
}
