using Bam.Console;
using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class MenuCommand : ICommand
    {
        public MenuCommand(IMenuManager menuManager, IMenuItem menuItem)
        {
            this.MenuManager = menuManager;
            this.MenuItem = menuItem;
        }

        public string CommandName
        {
            get
            {
                return MenuItem.DisplayName;
            }
        }

        public string CommandSelector
        {
            get
            {
                return MenuItem.Selector;
            }
        }

        protected IMenuManager MenuManager
        {
            get;
            private set;
        }

        protected IMenuItem MenuItem
        {
            get;
            private set;
        }

        public ICommandExecutionResult? Execute(string[] arguments)
        {
            IMenuItemRunResult? runResult = MenuManager.RunMenuItem(MenuItem, MenuInput.FromArguments(arguments));

            return new MenuCommandExecutionResult(runResult, this.MenuItem, arguments);
        }
    }
}
