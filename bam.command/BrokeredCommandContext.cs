using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public abstract class BrokeredCommandContext : IBrokeredCommandContext
    {
        public const string DefaultContextName = "DEFAULT";

        public BrokeredCommandContext(string name, IBrokeredCommandInitializer commandInitializer, IBrokeredCommandRunner commandRunner)
        {            
            this.ContextName = name;
            this.CommandInitializer = commandInitializer;
            this.CommandRunner = commandRunner;
            this.Commands = this.InitializeCommands();
        }

        static DefaultCommandContext? _default;
        static object _defaultLock = new object();
        public static BrokeredCommandContext? Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () => new DefaultCommandContext(BamCommandContext.Current.ServiceRegistry.Get<MenuCommandRunner>()));
            }
        }

        protected IBrokeredCommandInitializer CommandInitializer { get; set; }
        
        //protected internal ICommandBroker Broker { get; set; }

        public IBrokeredCommandRunner CommandRunner { get; set; }

        public string ContextName
        {
            get;
            protected set;
        }

        protected virtual IDictionary<string, IBrokeredCommand> InitializeCommands()
        {
            return this.CommandInitializer.InitializeCommands();
        }

        public IDictionary<string, IBrokeredCommand> Commands
        {
            get;
            protected set;
        }

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
