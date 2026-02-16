using Bam.Shell;

namespace Bam.Command
{
    /// <summary>
    /// Resolves command contexts by loading menus from an <see cref="IMenuManager"/> and creating
    /// a <see cref="MenuCommandContext"/> for each menu.
    /// </summary>
    public class MenuCommandContextResolver : CommandContextResolver
    {
        /// <summary>
        /// Initializes a new instance with the specified menu manager and command runner.
        /// </summary>
        /// <param name="menuManager">The menu manager used to discover available menus.</param>
        /// <param name="commandRunner">The runner used to execute menu commands.</param>
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

        /// <summary>
        /// Loads menus from the menu manager and creates a command context for each menu,
        /// keyed by the menu's name, display name, and container type name.
        /// </summary>
        /// <returns>A dictionary mapping context names to their <see cref="MenuCommandContext"/> instances.</returns>
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
