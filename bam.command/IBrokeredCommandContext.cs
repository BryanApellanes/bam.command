using Bam.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam
{
    public interface IBrokeredCommandContext
    {
        IBrokeredCommandRunner CommandRunner { get; }
        string ContextName { get; }
        IDictionary<string, IBrokeredCommand> Commands { get; }
        string ResolveCommandSelector(string[] arguments);
        IBrokeredCommandResult Execute(string[] arguments);
    }
}
