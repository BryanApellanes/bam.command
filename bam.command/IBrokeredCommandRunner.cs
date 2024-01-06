using Bam.Services;
using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public interface IBrokeredCommandRunner
    {
        IBrokeredCommandRunResult Run(IBrokeredCommand command, string[] arguments);
    }
}
