using Bam.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class BrokeredCommand : IBrokeredCommand
    {
        public BrokeredCommand(ICommandBroker broker, ICommandContext context, ICommand? command, ICommandExecutionResult? executionResult) 
        {
            this.Broker = broker;
            this.Context = context;
            this.Command = command;
            this.ExecutionResult = executionResult;
        }

        static IBrokeredCommand _empty;
        static object _emptyLock = new object();
        public static IBrokeredCommand Empty
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

        public ICommandContext Context
        {
            get;
            set;
        }

        public ICommand? Command
        {
            get;
            set;
        }

        public bool Success
        {
            get
            {
                return ExecutionResult?.Success == true;
            }
        }

        public ICommandExecutionResult? ExecutionResult
        {
            get;
            set;
        }
    }
}
