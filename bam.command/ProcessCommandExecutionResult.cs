using Bam.CommandLine;

namespace Bam.Command
{
    /// <summary>
    /// Represents the result of executing an external process command, wrapping a <see cref="ProcessOutput"/>
    /// and exposing its standard output, error, and exit code through the <see cref="IBrokeredCommandRunResult"/> interface.
    /// </summary>
    public class ProcessCommandExecutionResult : IBrokeredCommandRunResult
    {
        /// <summary>
        /// Initializes a new instance with the specified process output.
        /// </summary>
        /// <param name="processOutput">The output captured from the executed process.</param>
        public ProcessCommandExecutionResult(ProcessOutput processOutput)
        {
            this.ProcessOutput = processOutput;

        }

        /// <summary>
        /// Gets or sets the captured process output.
        /// </summary>
        public ProcessOutput ProcessOutput { get; set; }

        /// <summary>
        /// Gets the standard error output from the process as the result message.
        /// </summary>
        public string Message
        {
            get
            {
                return ProcessOutput.StandardError.ToString();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the process exited with code 0 (success).
        /// </summary>
        public bool Success
        {
            get
            {
                return this.ProcessOutput.Process.ExitCode == 0;
            }
        }

        /// <summary>
        /// Gets the standard output from the process as the result value.
        /// </summary>
        public object Result
        {
            get
            {
                return this.ProcessOutput.StandardOutput.ToString();
            }
        }

        /// <summary>
        /// Gets the command name, derived from the process start info file name.
        /// </summary>
        public string CommandName
        {
            get
            {
                return this.ProcessOutput.Process.StartInfo.FileName;
            }
        }

        /// <summary>
        /// Gets the arguments, parsed from the process start info arguments string by splitting on spaces.
        /// </summary>
        public string[] Arguments
        {
            get
            {
                return this.ProcessOutput.Process.StartInfo.Arguments.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }
}
