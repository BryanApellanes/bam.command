using Bam.Console;
using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class MenuCommandInitializer : IBrokeredCommandInitializer
    {
        public MenuCommandInitializer() : this(BamConsoleContext.Current.MenuManager.CurrentMenu)
        {
        }

        public MenuCommandInitializer(IMenu? menu)
        {
            this.Menu = menu;
        }

        public IMenu? Menu
        {
            get; 
            private set; 
        }

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
