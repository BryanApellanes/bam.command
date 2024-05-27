using Bam.CoreServices;
using Bam.Logging;
using Bam;
using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Configuration;

namespace Bam.Command
{
    public interface IBamBrokeredCommandContext : IBamContext
    {
        ICommandBroker CommandBroker { get; }
    }
}
