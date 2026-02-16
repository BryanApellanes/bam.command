namespace Bam.Command
{
    /// <summary>
    /// Defines a runner that executes a brokered command with the given arguments.
    /// </summary>
    public interface IBrokeredCommandRunner
    {
        /// <summary>
        /// Executes the specified command with the given arguments and returns the run result.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="arguments">The command-line arguments to pass to the command.</param>
        /// <returns>An <see cref="IBrokeredCommandRunResult"/> containing the execution outcome.</returns>
        IBrokeredCommandRunResult Run(IBrokeredCommand command, string[] arguments);
    }
}
