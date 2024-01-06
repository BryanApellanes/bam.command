using Bam.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam
{
    public interface IBrokeredCommand
    {
        string CommandName { get; }
        string CommandSelector { get; }
    }
}
