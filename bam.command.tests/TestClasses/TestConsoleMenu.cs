using Bam.Console;
using Bam.DependencyInjection;
using Bam.Services;

namespace Bam.Command.Tests.TestClasses
{
    [ConsoleMenu("TestContext")]
    public class TestConsoleMenu : ConsoleMenuContainer
    {
        public TestConsoleMenu(ServiceRegistry serviceRegistry) : base(serviceRegistry)
        {
        }
    }
}
