using Bam.Logging;

namespace Bam.Command
{
    /// <summary>
    /// Abstract base class for command brokers that resolve command contexts from arguments,
    /// execute commands within those contexts, and raise lifecycle events throughout the process.
    /// </summary>
    public abstract class CommandBroker : Loggable, ICommandBroker
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CommandBroker"/> with the specified context resolver and optional logger.
        /// Immediately loads command contexts from the resolver during construction.
        /// </summary>
        /// <param name="commandContextResolver">The resolver used to load and resolve command contexts.</param>
        /// <param name="logger">An optional logger; defaults to <see cref="Log.Default"/> if not specified.</param>
        public CommandBroker(IBrokeredCommandContextResolver commandContextResolver, ILogger? logger = null)
        {
            this.CommandContextResolver = commandContextResolver;
            this.Logger = logger ?? Log.Default!;
            this.Initialize();
        }

        /// <summary>
        /// Gets the context resolver used to load and resolve command contexts from arguments.
        /// </summary>
        protected IBrokeredCommandContextResolver CommandContextResolver { get; private set; }

        /// <summary>
        /// Gets the dictionary of available command contexts, keyed by context name.
        /// </summary>
        public virtual IDictionary<string, IBrokeredCommandContext> CommandContexts
        {
            get;
            private set;
        } = null!;

        /// <summary>
        /// Gets the logger used by this command broker.
        /// </summary>
        public ILogger Logger
        {
            get;
            private set;
        } = null!;

        /// <summary>
        /// Raised when command context initialization begins.
        /// </summary>
        public event EventHandler<CommandBrokerEventArgs>? InitializeStarted;

        /// <summary>
        /// Raised when command context initialization completes successfully.
        /// </summary>
        public event EventHandler<CommandBrokerEventArgs>? InitializeCompleted;

        /// <summary>
        /// Raised when command context initialization fails with an exception.
        /// </summary>
        public event EventHandler<CommandBrokerEventArgs>? InitializeFailed;

        /// <summary>
        /// Raised when context resolution begins.
        /// </summary>
        public event EventHandler<CommandBrokerEventArgs>? ResolveContextStarted;

        /// <summary>
        /// Raised when context resolution completes successfully.
        /// </summary>
        public event EventHandler<CommandBrokerEventArgs>? ResolveContextCompleted;

        /// <summary>
        /// Raised when context resolution fails with an exception.
        /// </summary>
        public event EventHandler<CommandBrokerEventArgs>? ResolveContextFailed;

        /// <summary>
        /// Raised when command brokering begins.
        /// </summary>
        public event EventHandler<CommandBrokerEventArgs>? BrokerCommandStarted;

        /// <summary>
        /// Raised when command brokering completes successfully.
        /// </summary>
        public event EventHandler<CommandBrokerEventArgs>? BrokerCommandCompleted;

        /// <summary>
        /// Raised when command brokering fails with an exception.
        /// </summary>
        public event EventHandler<CommandBrokerEventArgs>? BrokerCommandFailed;

        /// <summary>
        /// Resolves the appropriate command context from the arguments and executes the command,
        /// returning the result. Returns <see cref="BrokeredCommand.Empty"/> on failure.
        /// </summary>
        /// <param name="arguments">The command-line arguments used to resolve and execute the command.</param>
        /// <returns>An <see cref="IBrokeredCommandResult"/> containing the execution outcome.</returns>
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
                    IBrokeredCommandResult command = commandContext.Execute(this, arguments);

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

        /// <summary>
        /// Resolves the command context for the given arguments by using the context resolver to determine
        /// the context name and looking it up in the loaded contexts.
        /// </summary>
        /// <param name="arguments">The command-line arguments to resolve the context from.</param>
        /// <returns>The resolved <see cref="IBrokeredCommandContext"/>.</returns>
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
