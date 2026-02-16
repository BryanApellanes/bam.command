namespace Bam.Command
{
    /// <summary>
    /// A command context backed by an external executable file, using <see cref="ProcessCommandRunner"/>
    /// to execute commands as system processes.
    /// </summary>
    public class ProcessCommandContext : BrokeredCommandContext
    {
        /// <summary>
        /// Initializes a new instance with the specified executable file and command runner.
        /// The context name is derived from the file name without extension.
        /// </summary>
        /// <param name="executableFile">The executable file backing this context.</param>
        /// <param name="commandRunner">The runner used to execute process commands.</param>
        public ProcessCommandContext(FileInfo executableFile, ProcessCommandRunner commandRunner) : base(Path.GetFileNameWithoutExtension(executableFile.Name), new ProcessCommandInitializer(), commandRunner)
        {
            this.ExecutableFile = executableFile;
        }

        /// <summary>
        /// Gets or sets the executable file backing this command context.
        /// </summary>
        public FileInfo ExecutableFile { get; set; }
    }
}
