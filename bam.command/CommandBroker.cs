using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public abstract class CommandBroker : ICommandBroker
    {
        public CommandBroker(ICommandContextResolver commandContextResolver)
        {
            this.CommandContextResolver = commandContextResolver;
            this.Initialize();
        }

        protected ICommandContextResolver CommandContextResolver { get; private set; }

        public virtual IDictionary<string, ICommandContext> CommandContexts 
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

        public IBrokeredCommand BrokerCommand(string[] arguments)
        {
            try
            {
                BrokerCommandStarted?.Invoke(this, new CommandBrokerEventArgs()
                {
                    CommandBroker = this,
                    Arguments = arguments
                });

                ICommandContext? commandContext = TryResolveContext(arguments);
                if(commandContext != null)
                {
                    IBrokeredCommand command = commandContext.Execute(arguments);

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

        private ICommandContext? TryResolveContext(string[] arguments)
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

        public virtual ICommandContext ResolveContext(string[] arguments)
        {
            ResolveContextStarted?.Invoke(this, new CommandBrokerEventArgs()
            {
                CommandBroker = this,
                Arguments = arguments
            });

            string contextName = CommandContextResolver.ResolveContextName(arguments);
            if (contextName.Equals(DefaultCommandContext.Name) && CommandContext.Default != null)
            {
                CommandContext.Default.Broker = this;
                return CommandContext.Default;
            }

            if (CommandContexts.ContainsKey(contextName))
            {
                ICommandContext context =  CommandContexts[contextName];
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
                InitializeFailed?.Invoke(this, new CommandBrokerEventArgs()
                {
                    CommandBroker = this,
                    Exception = ex
                });
            }
        }
    }
}
