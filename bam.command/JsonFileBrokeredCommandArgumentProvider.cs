namespace Bam.Command
{
    /// <summary>
    /// Provides command arguments by deserializing JSON files from a specified directory.
    /// </summary>
    public class JsonFileBrokeredCommandArgumentProvider : FileBrokeredCommandArgumentProvider
    {
        /// <summary>
        /// Initializes a new instance with the specified argument parser and JSON file directory path.
        /// </summary>
        /// <param name="commandArgumentParser">The parser used to parse raw command-line arguments.</param>
        /// <param name="jsonFileDirectoryPath">The directory path where JSON argument files are located.</param>
        public JsonFileBrokeredCommandArgumentProvider(ICommandArgumentParser commandArgumentParser, string jsonFileDirectoryPath) : base(commandArgumentParser, jsonFileDirectoryPath)
        {
        }

        /// <summary>
        /// Gets the file extension for JSON files (".json").
        /// </summary>
        public override string FileExtension
        {
            get
            {
                return ".json";
            }
        }

        /// <summary>
        /// Deserializes the specified JSON file content into an object of the given type.
        /// </summary>
        /// <param name="file">The JSON file to deserialize.</param>
        /// <param name="type">The target type to deserialize into.</param>
        /// <returns>The deserialized object.</returns>
        public override object? DeserializeFileContent(FileInfo file, Type type)
        {
            return file.FromJsonFile(type);
        }
    }
}
