using Bam.Shell;

namespace Bam.Command
{
    /// <summary>
    /// Represents the result of executing a menu command, delegating most properties
    /// to the underlying <see cref="IMenuItemRunResult"/>.
    /// </summary>
    public class MenuCommandRunResult : IBrokeredCommandRunResult
    {
        /// <summary>
        /// Initializes a new instance with the specified menu item run result, menu item, and arguments.
        /// </summary>
        /// <param name="menuItemRunResult">The underlying menu item execution result.</param>
        /// <param name="menuItem">The menu item that was executed.</param>
        /// <param name="arguments">The arguments that were passed to the command.</param>
        public MenuCommandRunResult(IMenuItemRunResult? menuItemRunResult, IMenuItem menuItem, string[] arguments)
        {
            this.MenuItemRunResult = menuItemRunResult;
            this.MenuItem = menuItem;
            this.Arguments = arguments;
        }

        /// <summary>
        /// Gets the underlying menu item execution result.
        /// </summary>
        public IMenuItemRunResult? MenuItemRunResult
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the menu item that was executed.
        /// </summary>
        public IMenuItem MenuItem
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the message from the underlying menu item run result, or an empty string if not available.
        /// </summary>
        public string Message
        {
            get
            {
                return MenuItemRunResult?.Message ?? string.Empty;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the command executed successfully, based on the underlying menu item run result.
        /// </summary>
        public bool Success
        {
            get
            {
                return MenuItemRunResult?.Success ?? false;
            }
        }

        /// <summary>
        /// Gets the return value produced by the menu item execution, or an empty string if not available.
        /// </summary>
        public object Result
        {
            get
            {
                return MenuItemRunResult?.Result ?? string.Empty;
            }
        }

        /// <summary>
        /// Gets the command name, derived from the menu item's display name.
        /// </summary>
        public string CommandName
        {
            get
            {
                return MenuItem?.DisplayName ?? string.Empty;
            }
        }

        /// <summary>
        /// Gets the arguments that were passed to the command.
        /// </summary>
        public string[] Arguments
        {
            get;
            private set;
        }
    }
}
