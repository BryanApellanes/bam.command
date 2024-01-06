using Bam.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class BrokeredCommand : IBrokeredCommandResult
    {
        public BrokeredCommand(ICommandBroker broker, IBrokeredCommandContext context, Bam.IBrokeredCommand? command, IBrokeredCommandRunResult? executionResult) 
        {
            this.Broker = broker;
            this.Context = context;
            this.Command = command;
            this.RunResult = executionResult;
        }

        static IBrokeredCommandResult _empty;
        static object _emptyLock = new object();
        public static IBrokeredCommandResult Empty
        {
            get
            {
                return _emptyLock.DoubleCheckLock(ref _empty, () => new BrokeredCommand(null, null, null, null));
            }
        }

        public ICommandBroker Broker
        {
            get;
            set;
        }

        public IBrokeredCommandContext Context
        {
            get;
            set;
        }

        public Bam.IBrokeredCommand? Command
        {
            get;
            set;
        }

        public bool Success
        {
            get
            {
                return RunResult?.Success == true;
            }
        }

        public IBrokeredCommandRunResult? RunResult
        {
            get;
            set;
        }
    }
}
