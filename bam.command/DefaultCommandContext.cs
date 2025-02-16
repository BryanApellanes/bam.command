namespace Bam.Command
{
    public class DefaultCommandContext : MenuCommandContext
    {
        public const string Name = "DEFAULT";
        public DefaultCommandContext(MenuCommandRunner commandRunner) : base(Name, commandRunner)
        {
        }

        public string ContextName
        {
            get
            {
                return Name;
            }
        }
    }
}
