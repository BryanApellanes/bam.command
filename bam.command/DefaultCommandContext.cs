namespace Bam.Command
{
    /// <summary>
    /// The default command context used when no specific context is resolved from arguments.
    /// Wraps a <see cref="MenuCommandRunner"/> with the "DEFAULT" context name.
    /// </summary>
    public class DefaultCommandContext : MenuCommandContext
    {
        /// <summary>
        /// The constant name for the default command context.
        /// </summary>
        public const string Name = "DEFAULT";

        /// <summary>
        /// Initializes a new instance of <see cref="DefaultCommandContext"/> with the specified command runner.
        /// </summary>
        /// <param name="commandRunner">The menu command runner used to execute commands in this context.</param>
        public DefaultCommandContext(MenuCommandRunner commandRunner) : base(Name, commandRunner)
        {
        }

        /// <summary>
        /// Gets the name of this context, which is always "DEFAULT".
        /// </summary>
        public string ContextName
        {
            get
            {
                return Name;
            }
        }
    }
}
