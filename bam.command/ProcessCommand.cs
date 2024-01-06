using Bam.Net;
using Bam.Net.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class ProcessCommand : IBrokeredCommand
    {
        public ProcessCommand(string name, string exePath) 
        {
            this.CommandName = name;
            this.CommandSelector= name;
            this.ExePath= exePath;
        }

        public string CommandName { get; private set; }

        public string CommandSelector { get; private set; }

        public string ExePath { get; private set; }
    }
}
