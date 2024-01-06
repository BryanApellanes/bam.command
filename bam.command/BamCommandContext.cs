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
    public class BamCommandContext : BamContext, IBamBrokeredCommandContext
    {
        static BamCommandContext()
        {
            Current = new BamCommandContext();
        }

        public BamCommandContext()
        {
            this.ServiceRegistry = this.GetDefaultContextServiceRegistry();
        }

        public BamCommandContext(ServiceRegistry serviceRegistry)
        {
            this.ServiceRegistry = serviceRegistry;
        }

        public static BamCommandContext Current { get; private set; }

        public ICommandBroker CommandBroker
        {
            get
            {
                return ServiceRegistry.Get<ICommandBroker>();
            }
        }

        public static async void Main(string[] args)
        {
            ICommandBroker broker = Current.ServiceRegistry.Get<ICommandBroker>();
            IBrokeredCommandResult command = broker.BrokerCommand(args);
            int exitCode = 0;
            if (!command.Success)
            {
                Message.PrintLine(command.RunResult.Message, ConsoleColor.Magenta);
                exitCode = 1;
            }

            BamConsoleContext.Exit(exitCode);
        }

        /// <summary>
        /// Gets the default service registry for the current context.
        /// </summary>
        /// <returns></returns>
        public override ServiceRegistry GetDefaultContextServiceRegistry()
        {
            return new ServiceRegistry()
                .For<IApplicationNameProvider>().Use(new ProcessApplicationNameProvider())
                .For<IConfigurationProvider>().Use(new DefaultConfigurationProvider())
                .For<ILogger>().Use(new ConsoleLogger())
                .For<IBrokeredCommandContextResolver>().Use<ProcessCommandContextResolver>()
                .For<IBamBrokeredCommandContext>().Use(this);
        }
    }
}
