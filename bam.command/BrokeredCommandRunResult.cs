using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class BrokeredCommandRunResult : IBrokeredCommandRunResult
    {
        public string Message
        {
            get;
            set;
        }

        public bool Success
        {
            get;
            set;
        }


        public object? Result
        {
            get;
            set;
        }


        public string CommandName
        {
            get;
            set;
        }

        public string[] Arguments
        {
            get;
            set;
        }

    }
}
