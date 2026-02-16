using Bam.CommandLine;

namespace Bam.Command
{
    /// <summary>
    /// Executes brokered commands by running them as external system processes, capturing standard output and error.
    /// </summary>
    public class ProcessCommandRunner : IBrokeredCommandRunner
    {
        /// <summary>
        /// Runs the specified command as an external process with the given arguments.
        /// The command must be a <see cref="ProcessCommand"/>.
        /// </summary>
        /// <param name="command">The brokered command to execute (must be a <see cref="ProcessCommand"/>).</param>
        /// <param name="arguments">The command-line arguments to pass to the process.</param>
        /// <returns>A <see cref="ProcessCommandExecutionResult"/> containing the process output and exit code.</returns>
        public IBrokeredCommandRunResult Run(IBrokeredCommand command, string[] arguments)
        {
            Args.ThrowIfNull(command, nameof(command));

            ProcessCommand? processCommand = command as ProcessCommand;
            if (processCommand == null)
            {
                throw new ArgumentException($"Unsupported command type, should be of type {nameof(ProcessCommand)} but was {command.GetType().Name}");
            }

            ProcessOutput processOutput = $"{processCommand.ExePath} {string.Join(" ", arguments)}".Run();

            return new ProcessCommandExecutionResult(processOutput);
        }
    }
}
