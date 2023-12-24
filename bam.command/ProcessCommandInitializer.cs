using Bam.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class ProcessCommandInitializer : ICommandInitializer
    {
        public IDictionary<string, ICommand> InitializeCommands()
        {
            // look in BamDir/commands for executables
            
            throw new NotImplementedException();
        }
    }
}
