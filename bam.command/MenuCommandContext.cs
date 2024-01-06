using Bam.Console;
using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class MenuCommandContext : BrokeredCommandContext
    {
        public MenuCommandContext(string name, MenuCommandRunner commandRunner) : base(name, new MenuCommandInitializer(), commandRunner)
        {
            this.MenuManager = BamConsoleContext.Current.MenuManager;
        }

        public MenuCommandContext(IMenu menu, MenuCommandRunner commandRunner): base(menu.Name, new MenuCommandInitializer(menu), commandRunner)
        {
            this.Menu = menu;
            this.CommandRunner = commandRunner;
        }

        protected IMenu Menu 
        {
            get; 
            private set; 
        }

        protected IMenuManager MenuManager 
        {
            get; 
            set; 
        }
    }
}
