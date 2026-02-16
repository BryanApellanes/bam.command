using Bam.Shell;
using System.Reflection;

namespace Bam.Command
{
    /// <summary>
    /// Provides method arguments for brokered commands by resolving raw command-line arguments
    /// into typed parameter values.
    /// </summary>
    public interface IBrokeredCommandArgumentProvider : IMethodArgumentProvider
    {
        /// <summary>
        /// Resolves and returns the arguments for the specified method from the given raw command-line arguments.
        /// </summary>
        /// <param name="methodInfo">The method whose parameters define the expected argument types.</param>
        /// <param name="arguments">The raw command-line arguments.</param>
        /// <returns>An array of resolved argument values matching the method's parameter list.</returns>
        object?[] GetCommandArguments(MethodInfo methodInfo, string[] arguments);
    }
}
