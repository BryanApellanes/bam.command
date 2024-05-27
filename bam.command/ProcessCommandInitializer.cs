using Bam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class ProcessCommandInitializer : IBrokeredCommandInitializer
    {
        public IDictionary<string, Bam.IBrokeredCommand> InitializeCommands()
        {
            // look in BamDir/commands for executables
            
            throw new NotImplementedException();
        }
    }
}
