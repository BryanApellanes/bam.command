using Bam.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam
{
    public abstract class CommandContextResolver : ICommandContextResolver
    {
        public abstract IDictionary<string, ICommandContext> LoadContexts();

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
