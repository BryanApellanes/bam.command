using Bam.Console;
using Bam.Shell;

namespace Bam.Command
{
    /// <summary>
    /// Initializes brokered commands from the menu items of an <see cref="IMenu"/>,
    /// creating a <see cref="MenuCommand"/> for each menu item.
    /// </summary>
    public class MenuCommandInitializer : IBrokeredCommandInitializer
    {
        /// <summary>
        /// Initializes a new instance using the current menu from the console context's menu manager.
        /// </summary>
        public MenuCommandInitializer() : this(BamConsoleContext.Current.MenuManager.CurrentMenu)
        {
        }

        /// <summary>
        /// Initializes a new instance with the specified menu.
        /// </summary>
        /// <param name="menu">The menu whose items will be used to initialize commands, or null for an empty command set.</param>
        public MenuCommandInitializer(IMenu? menu)
        {
            this.Menu = menu;
        }

        /// <summary>
        /// Gets the menu whose items are used to initialize commands.
        /// </summary>
        public IMenu? Menu
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and returns a dictionary of commands from the menu items, keyed by display name,
        /// selector, and option name.
        /// </summary>
        /// <returns>A dictionary mapping command names to their <see cref="MenuCommand"/> instances.</returns>
        public IDictionary<string, IBrokeredCommand> InitializeCommands()
        {
            Dictionary<string, IBrokeredCommand> results = new Dictionary<string, IBrokeredCommand>();
            if(Menu != null)
            {
                foreach (IMenuItem menuItem in Menu.Items)
                {
                    MenuCommand menuCommand = new MenuCommand(menuItem);
                    foreach(string commandName in GetCommandNames(menuItem))
                    {
                        if (!results.ContainsKey(commandName))
                        {
                            results.Add(commandName, menuCommand);
                        }
                    }
                }
            }
            return results;
        }

        private IEnumerable<string> GetCommandNames(IMenuItem menuItem)
        {
            if(menuItem != null)
            {
                if (!string.IsNullOrEmpty(menuItem.DisplayName))
                {
                    yield return menuItem.DisplayName;
                }

                if (!string.IsNullOrEmpty(menuItem.Selector))
                {
                    yield return menuItem.Selector;
                }

                if(menuItem.Attribute != null)
                {
                    if(menuItem.Attribute is ConsoleCommandAttribute attr)
                    {
                        if (!string.IsNullOrEmpty(attr.OptionName) && !menuItem.DisplayName.Equals(attr.OptionName))
                        {
                            yield return attr.OptionName;
                        }
                    }
                }
            }
            yield break;
        }
    }
}
