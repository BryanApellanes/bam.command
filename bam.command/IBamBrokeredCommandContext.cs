using Bam.Net.CoreServices;
using Bam.Net.Logging;
using Bam.Net;
using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;

namespace Bam.Command
{
    public interface IBamBrokeredCommandContext : IBamContext
    {
        ICommandBroker CommandBroker { get; }
    }
}
