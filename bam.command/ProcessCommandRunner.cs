using Bam;
using Bam.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class ProcessCommandRunner : IBrokeredCommandRunner
    {
        public IBrokeredCommandRunResult Run(IBrokeredCommand command, string[] arguments)
        {
            Args.ThrowIfNull(command, nameof(command));

            ProcessCommand? processCommand = command as ProcessCommand;
            if (processCommand == null)
            {
                throw new ArgumentException($"Unsupported command type, should be of type {nameof(ProcessCommand)} but was {command.GetType().Name}");
            }

            ProcessOutput processOutput = $"{processCommand.ExePath} {string.Join(" ", arguments)}".Run();

            return new ProcessCommandExecutionResult(processOutput);
        }
    }
}
