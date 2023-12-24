using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public abstract class CommandContext : ICommandContext
    {
        public CommandContext(string name, ICommandInitializer commandInitializer)
        {            
            this.ContextName = name;
            this.CommandInitializer = commandInitializer;
            this.Commands = this.InitializeCommands();
        }

        static DefaultCommandContext? _default;
        static object _defaultLock = new object();
        public static CommandContext? Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () => new DefaultCommandContext());
            }
        }

        protected ICommandInitializer CommandInitializer { get; set; }
        protected internal ICommandBroker Broker { get; set; }

        public string ContextName
        {
            get;
        }

        protected virtual IDictionary<string, ICommand> InitializeCommands()
        {
            return this.CommandInitializer.InitializeCommands();
        }

        public IDictionary<string, ICommand> Commands
        {
            get;
            protected set;
        }

        public virtual string ResolveCommandSelector(string[] arguments)
        {
            if(arguments == null || arguments.Length == 0)
            {
                ICommand? command = this.Commands.Values.FirstOrDefault();
                if (command != null)
                {
                    return command.CommandSelector;
                }
                throw new InvalidOperationException("No arguments specified and no commands found");
            }            
            return arguments[0];
        }

        public virtual IBrokeredCommand Execute(string[] arguments)
        {
            string commandName = ResolveCommandSelector(arguments);
            ICommand? command = this.GetCommand(commandName);
            if (command != null)
            {
                ICommandExecutionResult? result = command.Execute(arguments);
                return new BrokeredCommand(this.Broker, this, command, result);
            }

            return new BrokeredCommand(this.Broker, this, null, new CommandExecutionExceptionResult(commandName, new ArgumentNullException(commandName)) { Arguments = arguments });
        }

        protected ICommand? GetCommand(string commandName)
        {
            if (this.Commands != null && this.Commands.ContainsKey(commandName))
            {
                return this.Commands[commandName];
            }
            return null;
        }
    }
}
