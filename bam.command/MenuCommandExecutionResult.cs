using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class MenuCommandExecutionResult : ICommandExecutionResult
    {
        public MenuCommandExecutionResult(IMenuItemRunResult? menuItemRunResult, IMenuItem menuItem, string[] arguments)
        {
            this.MenuItemRunResult = menuItemRunResult;
            this.MenuItem = menuItem;
            this.Arguments = arguments;
        }

        public IMenuItemRunResult? MenuItemRunResult
        {
            get;
            private set;
        }

        public IMenuItem MenuItem
        {
            get;
            private set;
        }

        public string Message
        {
            get
            {
                return MenuItemRunResult?.Message ?? string.Empty;
            }
        }

        public bool Success
        {
            get
            {
                return MenuItemRunResult?.Success ?? false;
            }
        }

        public object Result
        {
            get
            {
                return MenuItemRunResult?.Result ?? string.Empty;
            }
        }

        public string CommandName
        {
            get
            {
                return MenuItem?.DisplayName ?? string.Empty;
            }
        }

        public string[] Arguments
        {
            get;
            private set;
        }
    }
}
