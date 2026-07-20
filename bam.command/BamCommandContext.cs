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
        public new static BamCommandContext Current { get; private set; }

        /// <summary>
        /// Gets the command broker resolved from the service registry.
        /// </summary>
        public ICommandBroker CommandBroker => ServiceRegistry.Get<ICommandBroker>();

        /// <summary>
        /// Entry point that brokers command-line arguments, executes the resolved command,
        /// and exits with an appropriate exit code. Synchronous by design: brokering is synchronous,
        /// and an async entry here would let broker failures escape unobserved.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            ICommandBroker broker = Current.ServiceRegistry.Get<ICommandBroker>();
            IBrokeredCommandResult command = broker.BrokerCommand(args);
            int exitCode = 0;
            if (!command.Success)
            {
                Message.PrintLine(GetFailureMessage(command, args), ConsoleColor.Magenta);
                exitCode = 1;
            }

            BamConsoleContext.Exit(exitCode);
        }

        /// <summary>
        /// Gets the message describing a failed brokered command, falling back to a diagnostic
        /// naming the arguments when the result carries no run result (e.g. the context or command
        /// did not resolve).
        /// </summary>
        /// <param name="command">The failed brokered command result.</param>
        /// <param name="args">The command-line arguments that were brokered.</param>
        /// <returns>A non-empty message describing the failure.</returns>
        public static string GetFailureMessage(IBrokeredCommandResult command, string[] args)
        {
            string? message = command?.RunResult?.Message;
            if (!string.IsNullOrEmpty(message))
            {
                return message;
            }
            return string.Format("Command failed: context or command not resolved (args: {0})", string.Join(" ", args ?? new string[] { }));
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
