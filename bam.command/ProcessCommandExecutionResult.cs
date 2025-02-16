using Bam.CommandLine;

namespace Bam.Command
{
    public class ProcessCommandExecutionResult : IBrokeredCommandRunResult
    {
        public ProcessCommandExecutionResult(ProcessOutput processOutput)
        {
            this.ProcessOutput = processOutput;
            
        }

        public ProcessOutput ProcessOutput { get; set; }

        public string Message 
        {
            get
            {
                return ProcessOutput.StandardError.ToString();
            } 
        }

        public bool Success 
        {
            get
            {
                return this.ProcessOutput.Process.ExitCode == 0;
            }
        }

        public object Result
        {
            get
            {
                return this.ProcessOutput.StandardOutput.ToString();
            }
        }

        public string CommandName 
        {
            get
            {
                return this.ProcessOutput.Process.StartInfo.FileName;
            }
        }

        public string[] Arguments 
        {
            get
            {
                return this.ProcessOutput.Process.StartInfo.Arguments.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }
}
