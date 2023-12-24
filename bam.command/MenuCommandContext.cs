using Bam.Console;
using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class MenuCommandContext : CommandContext
    {
        public MenuCommandContext(string name) : base(name, new MenuCommandInitializer())
        {
            this.MenuManager = BamConsoleContext.Current.MenuManager;
        }

        protected IMenuManager MenuManager 
        {
            get; 
            set; 
        }
    }
}
