using Bam.Console;
using Bam.Shell;

namespace Bam.Command
{
    /// <summary>
    /// A command context that initializes its commands from console menu items and executes them
    /// using a <see cref="MenuCommandRunner"/>.
    /// </summary>
    public class MenuCommandContext : BrokeredCommandContext
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MenuCommandContext"/> with the specified name and command runner,
        /// using the current console context's menu manager.
        /// </summary>
        /// <param name="name">The name of this command context.</param>
        /// <param name="commandRunner">The runner used to execute menu commands.</param>
        public MenuCommandContext(string name, MenuCommandRunner commandRunner) : base(name, new MenuCommandInitializer(), commandRunner)
        {
            this.MenuManager = BamConsoleContext.Current.MenuManager;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="MenuCommandContext"/> from a specific menu and command runner.
        /// </summary>
        /// <param name="menu">The menu whose items define the available commands.</param>
        /// <param name="commandRunner">The runner used to execute menu commands.</param>
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
