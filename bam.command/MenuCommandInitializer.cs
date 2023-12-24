using Bam.Console;
using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class MenuCommandInitializer : ICommandInitializer
    {
        public MenuCommandInitializer() : this(BamConsoleContext.Current.MenuManager)
        {
        }

        public MenuCommandInitializer(IMenuManager menuManager)
        {
            this.MenuManager = menuManager;
        }

        public IMenuManager MenuManager 
        {
            get; 
            private set; 
        }

        public IDictionary<string, ICommand> InitializeCommands()
        {
            Dictionary<string, ICommand> results = new Dictionary<string, ICommand>();
            if(MenuManager.CurrentMenu != null)
            {
                foreach (IMenuItem menuItem in MenuManager.CurrentMenu.Items)
                {
                    results.Add(menuItem.Selector, new MenuCommand(MenuManager, menuItem));
                }
            }
            return results;
        }
    }
}
