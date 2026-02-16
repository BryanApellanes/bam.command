namespace Bam.Command
{
    /// <summary>
    /// Abstract base class for command contexts that manage a set of brokered commands,
    /// handling command initialization, resolution, and execution within a named context.
    /// </summary>
    public abstract class BrokeredCommandContext : IBrokeredCommandContext
    {
        /// <summary>
        /// The default context name used when no specific context is specified.
        /// </summary>
        public const string DefaultContextName = "DEFAULT";

        /// <summary>
        /// Initializes a new instance of <see cref="BrokeredCommandContext"/> with the specified name, initializer, and runner.
        /// </summary>
        /// <param name="name">The name of this command context.</param>
        /// <param name="commandInitializer">The initializer used to discover and register commands.</param>
        /// <param name="commandRunner">The runner used to execute commands.</param>
        public BrokeredCommandContext(string name, IBrokeredCommandInitializer commandInitializer, IBrokeredCommandRunner commandRunner)
        {
            this.ContextName = name;
            this.CommandInitializer = commandInitializer;
            this.CommandRunner = commandRunner;
            this.Commands = this.InitializeCommands();
        }

        static DefaultCommandContext? _default;
        static object _defaultLock = new object();
        /// <summary>
        /// Gets the thread-safe singleton default command context, backed by a <see cref="MenuCommandRunner"/>.
        /// </summary>
        public static BrokeredCommandContext? Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () => new DefaultCommandContext(BamCommandContext.Current.ServiceRegistry.Get<MenuCommandRunner>()));
            }
        }

        /// <summary>
        /// Gets or sets the command initializer responsible for discovering and registering commands.
        /// </summary>
        protected IBrokeredCommandInitializer CommandInitializer { get; set; }

        //protected internal ICommandBroker Broker { get; set; }

        /// <summary>
        /// Gets or sets the command runner responsible for executing commands.
        /// </summary>
        public IBrokeredCommandRunner CommandRunner { get; set; }

        /// <summary>
        /// Gets the name of this command context.
        /// </summary>
        public string ContextName
        {
            get;
            protected set;
        }

        /// <summary>
        /// Initializes and returns the dictionary of commands available in this context by delegating to the <see cref="CommandInitializer"/>.
        /// </summary>
        /// <returns>A dictionary mapping command names to their <see cref="IBrokeredCommand"/> instances.</returns>
        protected virtual IDictionary<string, IBrokeredCommand> InitializeCommands()
        {
            return this.CommandInitializer.InitializeCommands();
        }

        /// <summary>
        /// Gets the dictionary of commands available in this context, keyed by command name.
        /// </summary>
        public IDictionary<string, IBrokeredCommand> Commands
        {
            get;
            protected set;
        }

        /// <summary>
        /// Resolves the command selector from the given arguments. Returns the second argument if two or more
        /// are provided, the first argument if only one is provided, or the default command name if no arguments are given.
        /// </summary>
        /// <param name="arguments">The command-line arguments to resolve from.</param>
        /// <returns>The resolved command selector string.</returns>
        public virtual string ResolveCommandSelector(string[] arguments)
        {
            if(arguments == null || arguments.Length == 0)
            {
                IBrokeredCommand? command = this.Commands.Values.FirstOrDefault(c => c.CommandName.Equals(DefaultContextName));
                if(command == null)
                {
                    command = this.Commands.Values.FirstOrDefault();
                }
                if (command != null)
                {
                    return command.CommandName;
                }
                throw new InvalidOperationException("No arguments specified and no commands found");
            }
            if(arguments.Length >= 2)
            {
                return arguments[1];
            }
            return arguments[0];
        }

        /// <summary>
        /// Resolves and executes the appropriate command based on the given arguments, returning the result.
        /// </summary>
        /// <param name="broker">The command broker orchestrating the execution.</param>
        /// <param name="arguments">The command-line arguments used to resolve and execute the command.</param>
        /// <returns>An <see cref="IBrokeredCommandResult"/> containing the execution outcome.</returns>
        public virtual IBrokeredCommandResult Execute(ICommandBroker broker, string[] arguments)
        {
            string commandName = ResolveCommandSelector(arguments);
            Bam.IBrokeredCommand? command = this.GetCommand(commandName);
            if (command != null)
            {
                IBrokeredCommandRunResult? result = this.CommandRunner.Run(command, arguments);
                return new BrokeredCommand(broker, this, command, result);
            }

            return new BrokeredCommand(broker, this, null, new RunExceptionResult(commandName, new ArgumentNullException(commandName)) { Arguments = arguments });
        }

        /// <summary>
        /// Looks up a command by name from the available commands dictionary.
        /// </summary>
        /// <param name="commandName">The name of the command to retrieve.</param>
        /// <returns>The matching <see cref="IBrokeredCommand"/>, or null if not found.</returns>
        protected IBrokeredCommand? GetCommand(string commandName)
        {
            if (this.Commands != null && this.Commands.ContainsKey(commandName))
            {
                return this.Commands[commandName];
            }
            return null;
        }
    }
}
