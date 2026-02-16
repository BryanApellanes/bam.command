namespace Bam.Command
{
    /// <summary>
    /// Resolves command contexts by scanning directories for executable files with a "bam" prefix,
    /// creating a <see cref="ProcessCommandContext"/> for each discovered executable.
    /// </summary>
    public class ProcessCommandContextResolver : CommandContextResolver
    {
        const string bamPrefix = "bam";

        /// <summary>
        /// Initializes a new instance with the specified command runner, setting up default search
        /// directories and file filters.
        /// </summary>
        /// <param name="commandRunner">The runner used to execute process commands.</param>
        public ProcessCommandContextResolver(ProcessCommandRunner commandRunner) : base()
        {
            this.FileFilter = this.IsValidCommand;
            this.SetDirectories();
            this.CommandRunner = commandRunner;
        }

        protected ProcessCommandRunner CommandRunner { get; set; }

        /// <summary>
        /// Gets or sets the directories to search for executables in.
        /// </summary>
        public DirectoryInfo[] SearchDirectories 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets the filter function used to determine whether a file path represents a valid command.
        /// </summary>
        public Func<string, bool> FileFilter { get; set; }

        /// <summary>
        /// Scans the search directories for executable files, stripping the "bam" prefix from context names,
        /// and returns a dictionary of process command contexts.
        /// </summary>
        /// <returns>A dictionary mapping context names to their <see cref="ProcessCommandContext"/> instances.</returns>
        public override IDictionary<string, IBrokeredCommandContext> LoadContexts()
        {
            Dictionary<string, IBrokeredCommandContext> commandContexts = new Dictionary<string, IBrokeredCommandContext>();

            foreach(DirectoryInfo directoryInfo in SearchDirectories)
            {
                foreach(FileInfo file in directoryInfo.GetFiles())
                {
                    if (IsValidCommand(file.FullName))
                    {
                        string contextName = Path.GetFileNameWithoutExtension(file.Name);
                        if(contextName.Length > bamPrefix.Length && contextName.ToLowerInvariant().StartsWith(bamPrefix))
                        {
                            contextName = contextName.TruncateFront(bamPrefix.Length);
                        }
                        commandContexts.Add(contextName, new ProcessCommandContext(file, this.CommandRunner));
                    }
                }
            }

            return commandContexts;
        }

        protected virtual void SetDirectories()
        {
            this.SearchDirectories = new DirectoryInfo[] { new DirectoryInfo(BamProfile.ToolkitPath) };
        }

        protected virtual bool IsValidCommand(string path)
        {
            return Path.GetExtension(path).ToLowerInvariant().Equals(".exe") ||
                Path.GetFileName(path).ToLowerInvariant().StartsWith(bamPrefix);
        }
    }
}
