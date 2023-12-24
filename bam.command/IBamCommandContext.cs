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
using Bam.Shell.Console;

namespace Bam.Command
{
    public interface IBamCommandContext : IBamContext
    {
        ICommandBroker CommandBroker { get; }
    }
}
