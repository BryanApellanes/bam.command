using Bam.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
