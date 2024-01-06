using Bam.Console;
using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class MenuCommand : IBrokeredCommand
    {
        public MenuCommand(IMenuItem menuItem)
        {
            this.MenuItem = menuItem;
        }

        public IMenuItem MenuItem
        {
            get;
            private set;
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
    }
}
