namespace Bam.Command
{
    /// <summary>
    /// Provides command arguments by deserializing YAML files from a specified directory.
    /// </summary>
    public class YamlFileBrokeredCommandArgumentProvider : FileBrokeredCommandArgumentProvider
    {
        /// <summary>
        /// Initializes a new instance with the specified argument parser and YAML file directory path.
        /// </summary>
        /// <param name="commandArgumentParser">The parser used to parse raw command-line arguments.</param>
        /// <param name="fileDirectoryPath">The directory path where YAML argument files are located.</param>
        public YamlFileBrokeredCommandArgumentProvider(ICommandArgumentParser commandArgumentParser, string fileDirectoryPath) : base(commandArgumentParser, fileDirectoryPath)
        {
        }

        /// <summary>
        /// Gets the file extension for YAML files (".yaml").
        /// </summary>
        public override string FileExtension
        {
            get
            {
                return ".yaml";
            }
        }

        /// <summary>
        /// Deserializes the specified YAML file content into an object of the given type.
        /// </summary>
        /// <param name="file">The YAML file to deserialize.</param>
        /// <param name="type">The target type to deserialize into.</param>
        /// <returns>The deserialized object.</returns>
        public override object? DeserializeFileContent(FileInfo file, Type type)
        {
            return file.FromYamlFile(type);
        }
    }
}
