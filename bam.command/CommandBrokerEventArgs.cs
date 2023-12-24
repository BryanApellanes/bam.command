using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class CommandBrokerEventArgs : EventArgs
    {
        public string[] Arguments { get; set; }
        public ICommandBroker CommandBroker { get; set; }
        public ICommandContext CommandContext { get; set; }
        public IBrokeredCommand Command { get; set; }
        public Exception Exception { get; set; }
    }
}
