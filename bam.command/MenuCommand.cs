using Bam.Shell;

namespace Bam.Command
{
    /// <summary>
    /// Wraps an <see cref="IMenuItem"/> as an <see cref="IBrokeredCommand"/>, using the menu item's
    /// display name and selector as the command name and selector.
    /// </summary>
    public class MenuCommand : IBrokeredCommand
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MenuCommand"/> wrapping the specified menu item.
        /// </summary>
        /// <param name="menuItem">The menu item to wrap as a brokered command.</param>
        public MenuCommand(IMenuItem menuItem)
        {
            this.MenuItem = menuItem;
        }

        /// <summary>
        /// Gets the underlying menu item that this command wraps.
        /// </summary>
        public IMenuItem MenuItem
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command name, derived from the menu item's display name.
        /// </summary>
        public string CommandName
        {
            get
            {
                return MenuItem.DisplayName;
            }
        }

        /// <summary>
        /// Gets the command selector, derived from the menu item's selector.
        /// </summary>
        public string CommandSelector
        {
            get
            {
                return MenuItem.Selector;
            }
        }
    }
}
