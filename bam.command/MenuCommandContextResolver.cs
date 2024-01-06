using Bam.Console;
using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class MenuCommandContextResolver : CommandContextResolver
    {
        public MenuCommandContextResolver(IMenuManager menuManager, MenuCommandRunner commandRunner): base()
        {
            this.MenuManager = menuManager;
            this.CommandRunner = commandRunner;
        }

        protected IMenuManager MenuManager
        {
            get;
            set;
        }

        protected MenuCommandRunner CommandRunner { get; set; }

        public override IDictionary<string, IBrokeredCommandContext> LoadContexts()
        {
            MenuManager.LoadMenus();
            Dictionary<string, IBrokeredCommandContext> contexts = new Dictionary<string, IBrokeredCommandContext>();
            foreach(IMenu menu in MenuManager.Menus)
            {
                if (menu != null)
                {
                    MenuCommandContext context = new MenuCommandContext(menu, CommandRunner);
                    foreach(string name in GetContextNames(menu))
                    {
                        if (!contexts.ContainsKey(name))
                        {
                            contexts.Add(name, context);
                        }
                    };
                }
            }
            return contexts;
        }

        private IEnumerable<string> GetContextNames(IMenu menu)
        {
            if(menu != null)
            {
                if (!string.IsNullOrEmpty(menu.Name))
                {
                    yield return menu.Name;
                }

                if (!string.IsNullOrEmpty(menu.DisplayName))
                {
                    yield return menu.DisplayName;
                }

                if(menu.ContainerType != null)
                {
                    yield return menu.ContainerType.Name;
                }
            }
            yield break;
        }
    }
}
