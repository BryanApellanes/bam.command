using Bam.Shell;
using System.Reflection;

namespace Bam.Command
{
    public interface IBrokeredCommandArgumentProvider : IMethodArgumentProvider
    {
        object?[] GetCommandArguments(MethodInfo methodInfo, string[] arguments);
    }
}
