namespace Bam.Command
{
    /// <summary>
    /// Provides command arguments by deserializing files from the Bam data directory,
    /// supporting both JSON and YAML formats based on file extension.
    /// </summary>
    public class SerializedFileBrokeredCommandArgumentProvider : FileBrokeredCommandArgumentProvider
    {
        /// <summary>
        /// The name of the subdirectory within the Bam data path where argument files are stored.
        /// </summary>
        public const string ArgumentsFolderName = "arguments";

        /// <summary>
        /// Initializes a new instance using the default Bam data path for argument files,
        /// setting up both JSON and YAML sub-providers.
        /// </summary>
        /// <param name="commandArgumentParser">The parser used to parse raw command-line arguments.</param>
        public SerializedFileBrokeredCommandArgumentProvider(ICommandArgumentParser commandArgumentParser) : base(commandArgumentParser, Path.Combine(BamProfile.DataPath, ArgumentsFolderName))
        {
            string dir = Path.Combine(BamProfile.DataPath, ArgumentsFolderName);
            this.JsonFileBrokeredCommandArgumentProvider = new JsonFileBrokeredCommandArgumentProvider(commandArgumentParser, Path.Combine(dir, "json"));
            this.YamlFileBrokeredCommandArgumentProvider = new YamlFileBrokeredCommandArgumentProvider(commandArgumentParser, Path.Combine(dir, "yaml"));
        }

        protected JsonFileBrokeredCommandArgumentProvider JsonFileBrokeredCommandArgumentProvider { get; set; }
        protected YamlFileBrokeredCommandArgumentProvider YamlFileBrokeredCommandArgumentProvider { get; set; }

        /// <summary>
        /// Gets an empty file extension since this provider delegates to format-specific sub-providers.
        /// </summary>
        public override string FileExtension
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Deserializes the file content using the appropriate sub-provider based on the file extension (.json or .yaml).
        /// Returns null if the file extension is not recognized.
        /// </summary>
        /// <param name="file">The file to deserialize.</param>
        /// <param name="type">The target type to deserialize into.</param>
        /// <returns>The deserialized object, or null if the format is not supported.</returns>
        public override object? DeserializeFileContent(FileInfo file, Type type)
        {
            string extension = Path.GetExtension(file.FullName).ToLowerInvariant();
            if (extension.Equals(".json"))
            {
                return JsonFileBrokeredCommandArgumentProvider.DeserializeFileContent(file, type);
            }
            else if (extension.Equals(".yaml"))
            {
                return YamlFileBrokeredCommandArgumentProvider.DeserializeFileContent(file, type);
            }
            return null;
        }
    }
}
