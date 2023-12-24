using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam
{
    public interface ICommand
    {
        string CommandName { get; }
        string CommandSelector { get; }
        ICommandExecutionResult? Execute(string[] arguments);
    }
}
