using Bam.Command;

namespace Bam
{
    public abstract class CommandContextResolver : IBrokeredCommandContextResolver
    {
        public CommandContextResolver()
        {
        }

        public abstract IDictionary<string, IBrokeredCommandContext> LoadContexts();

        public string ResolveContextName(string[] arguments)
        {
            if(arguments == null || arguments.Length == 0)
            {
                return DefaultCommandContext.Name;
            }

            return arguments[0];
        }
    }
}
