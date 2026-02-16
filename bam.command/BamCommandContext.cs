using Bam.Console;
using Bam.Configuration;
using Bam.DependencyInjection;
using Bam.Logging;
using Bam.Services;

namespace Bam.Command
{
    /// <summary>
    /// Provides a BamContext implementation that integrates command brokering capabilities,
    /// serving as the main entry point for command-line applications using the Bam command framework.
    /// </summary>
    public class BamCommandContext : BamContext, IBamBrokeredCommandContext
    {
        static BamCommandContext()
        {
            Current = new BamCommandContext();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BamCommandContext"/> with the default service registry.
        /// </summary>
        public BamCommandContext()
        {
            this.ServiceRegistry = this.GetDefaultContextServiceRegistry();
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BamCommandContext"/> with the specified service registry.
        /// </summary>
        /// <param name="serviceRegistry">The service registry to use for dependency resolution.</param>
        public BamCommandContext(ServiceRegistry serviceRegistry)
        {
            this.ServiceRegistry = serviceRegistry;
        }

        /// <summary>
        /// Gets the singleton instance of the current <see cref="BamCommandContext"/>.
        /// </summary>
        public static BamCommandContext Current { get; private set; }

        /// <summary>
        /// Gets the command broker resolved from the service registry.
        /// </summary>
        public ICommandBroker CommandBroker => ServiceRegistry.Get<ICommandBroker>();

        /// <summary>
        /// Entry point that brokers command-line arguments, executes the resolved command,
        /// and exits with an appropriate exit code.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
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
        /// Gets the default service registry for the current context, pre-configured with
        /// standard providers for application naming, configuration, logging, and command context resolution.
        /// </summary>
        /// <returns>A <see cref="ServiceRegistry"/> configured with default service bindings.</returns>
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
