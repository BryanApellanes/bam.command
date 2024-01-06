using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public interface IBrokeredCommandContextResolver
    {
        IDictionary<string, IBrokeredCommandContext> LoadContexts();
        string ResolveContextName(string[] arguments);
    }
}
