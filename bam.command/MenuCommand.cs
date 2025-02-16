using Bam.Shell;

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
