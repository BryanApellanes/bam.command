using Bam.Services;
using Bam.Shell;
using System.Reflection;
using Bam.DependencyInjection;

namespace Bam.Command
{
    /// <summary>
    /// Executes menu commands by resolving the target method's declaring type instance via dependency injection,
    /// resolving method arguments, and invoking the method via reflection.
    /// </summary>
    public class MenuCommandRunner : IBrokeredCommandRunner
    {
        /// <summary>
        /// Initializes a new instance with the specified dependency provider and argument provider.
        /// </summary>
        /// <param name="dependencyProvider">The dependency provider used to resolve method instances.</param>
        /// <param name="commandArgumentProvider">The argument provider used to resolve method parameters from command-line arguments.</param>
        public MenuCommandRunner(IDependencyProvider dependencyProvider, IBrokeredCommandArgumentProvider commandArgumentProvider)
        {
            this.DependencyProvider = dependencyProvider;
            this.CommandArgumentProvider = commandArgumentProvider;
        }

        /// <summary>
        /// Gets the dependency provider used to resolve instances of command method declaring types.
        /// </summary>
        public IDependencyProvider DependencyProvider
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the argument provider used to resolve method parameters from command-line arguments.
        /// </summary>
        public IBrokeredCommandArgumentProvider CommandArgumentProvider
        {
            get;
            private set;
        }

        /// <summary>
        /// Executes the specified brokered command by casting it to a <see cref="MenuCommand"/> and running it.
        /// </summary>
        /// <param name="command">The brokered command to execute (must be a <see cref="MenuCommand"/>).</param>
        /// <param name="arguments">The command-line arguments to pass to the command.</param>
        /// <returns>An <see cref="IBrokeredCommandRunResult"/> containing the execution outcome.</returns>
        public IBrokeredCommandRunResult Run(IBrokeredCommand command, string[] arguments)
        {
            MenuCommand menuCommand = (MenuCommand)command;
            return Run(menuCommand, arguments);
        }

        /// <summary>
        /// Executes the specified menu command by resolving the method instance, arguments, and invoking via reflection.
        /// Returns a failure result if an exception occurs.
        /// </summary>
        /// <param name="menuCommand">The menu command to execute.</param>
        /// <param name="arguments">The command-line arguments to pass to the command.</param>
        /// <returns>An <see cref="IBrokeredCommandRunResult"/> containing the execution outcome.</returns>
        public IBrokeredCommandRunResult Run(MenuCommand menuCommand, string[] arguments)
        {
            try
            {
                IMenuItem menuItem = menuCommand.MenuItem;
                MethodInfo method = menuItem.MethodInfo;
                object? instance = menuItem.Instance;
                if (method.IsStatic)
                {
                    instance = null;
                }
                if (instance == null && !method.IsStatic && method.DeclaringType != null)
                {
                    instance = DependencyProvider.Get(method.DeclaringType);
                }
                object?[] methodArgs = CommandArgumentProvider.GetCommandArguments(method, arguments);

                object? result = method.Invoke(instance, methodArgs);

                return new MenuCommandRunResult(new MenuItemRunResult()
                {
                    Result = result,
                    MenuItem = menuItem,
                    Success = true
                }, menuItem, arguments);
            }
            catch (Exception ex)
            {
                return new MenuCommandRunResult(new MenuItemRunResult()
                {
                    Result = ex,
                    MenuItem = menuCommand.MenuItem,
                    Success = false,
                    Exception = ex.GetInnerException()
                }, menuCommand.MenuItem, arguments);
            }
        }
    }
}
