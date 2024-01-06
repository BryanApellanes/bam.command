using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public interface IBrokeredCommandArgumentProvider : IMethodArgumentProvider
    {
        object?[] GetCommandArguments(MethodInfo methodInfo, string[] arguments);
    }
}
