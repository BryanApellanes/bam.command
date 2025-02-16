namespace Bam.Command
{
    public class RunExceptionResult : IBrokeredCommandRunResult
    {
        public RunExceptionResult(string commandName, Exception exception)
        {
            this.CommandName = commandName;
            this.Exception = exception;
        }

        public Exception Exception { get; private set; }

        public string Message
        {
            get
            {
                return Exception.Message;
            }
        }

        public bool Success
        {
            get
            {
                return false;
            }
        }

        public object Result
        {
            get
            {
                return this.Exception;
            }
        }


        public string CommandName
        {
            get;
            private set;
        }

        public string[] Arguments
        {
            get;
            set;
        }
    }
}
