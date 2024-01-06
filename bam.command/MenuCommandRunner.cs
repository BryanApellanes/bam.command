using Bam.Net;
using Bam.Services;
using Bam.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class MenuCommandRunner : IBrokeredCommandRunner
    {
        public MenuCommandRunner(IDependencyProvider dependencyProvider, IBrokeredCommandArgumentProvider commandArgumentProvider)
        {
            this.DependencyProvider = dependencyProvider;
            this.CommandArgumentProvider = commandArgumentProvider;
        }

        public IDependencyProvider DependencyProvider
        {
            get;
            private set;
        }

        public IBrokeredCommandArgumentProvider CommandArgumentProvider
        {
            get;
            private set;
        }

        public IBrokeredCommandRunResult Run(IBrokeredCommand command, string[] arguments)
        {
            MenuCommand menuCommand = (MenuCommand)command;
            return Run(menuCommand, arguments);
        }

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
