using Bam.Net;
using Bam.Net.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class ProcessCommand : ICommand
    {
        public ProcessCommand(string name, string exePath) 
        {
            this.CommandName = name;
            this.CommandSelector= name;
            this.ExePath= exePath;
        }

        public string CommandName { get; private set; }

        public string CommandSelector { get; private set; }

        protected string ExePath { get; private set; }

        public ICommandExecutionResult Execute(string[] arguments)
        {
            ProcessOutput processOutput = $"{this.ExePath} {string.Join(" ", arguments)}".Run();
            return new ProcessCommandExecutionResult(processOutput);
        }
    }
}
