using Bam.Console;
using Bam.Net;
using Bam.Net.Configuration;
using Bam.Net.CoreServices;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class BamCommandContext : IBamCommandContext
    {
        static BamCommandContext()
        {
            Current = new BamCommandContext();
        }

        public BamCommandContext()
        {
            this.ServiceRegistry = this.GetServiceRegistry();
        }

        public BamCommandContext(ServiceRegistry serviceRegistry)
        {
            this.ServiceRegistry = serviceRegistry;
        }

        public static BamCommandContext Current { get; private set; }

        
        public void Configure(Action<ServiceRegistry> configure)
        {
            configure(this.ServiceRegistry);
        }

        public ICommandBroker CommandBroker
        {
            get
            {
                return ServiceRegistry.Get<ICommandBroker>();
            }
        }

        public IApplicationNameProvider ApplicationNameProvider
        {
            get
            {
                return ServiceRegistry.Get<IApplicationNameProvider>();
            }
        }

        public IConfigurationProvider ConfigurationProvider
        {
            get
            {
                return ServiceRegistry.Get<IConfigurationProvider>();
            }
        }

        public ILogger Logger
        {
            get
            {
                return ServiceRegistry.Get<ILogger>();
            }
        }

        public ServiceRegistry ServiceRegistry
        {
            get;
            private set;
        }

        public static async void Main(string[] args)
        {
            ICommandBroker broker = Current.ServiceRegistry.Get<ICommandBroker>();
            IBrokeredCommand command = broker.BrokerCommand(args);
            int exitCode = 0;
            if (!command.Success)
            {
                Message.PrintLine(command.ExecutionResult.Message, ConsoleColor.Magenta);
                exitCode = 1;
            }

            BamConsoleContext.Exit(exitCode);
        }

        protected ServiceRegistry GetServiceRegistry()
        {
            return new ServiceRegistry()
                .For<IApplicationNameProvider>().Use(new ProcessApplicationNameProvider())
                .For<IConfigurationProvider>().Use(new DefaultConfigurationProvider())
                .For<ILogger>().Use(new ConsoleLogger())
                .For<ICommandContextResolver>().Use<ProcessCommandContextResolver>()
                .For<IBamCommandContext>().Use(this);
        }
    }
}
