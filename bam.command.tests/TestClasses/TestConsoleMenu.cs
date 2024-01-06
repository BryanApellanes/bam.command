using Bam.Console;
using Bam.Net.CoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
