namespace Bam.Command
{
    public class YamlFileBrokeredCommandArgumentProvider : FileBrokeredCommandArgumentProvider
    {
        public YamlFileBrokeredCommandArgumentProvider(ICommandArgumentParser commandArgumentParser, string fileDirectoryPath) : base(commandArgumentParser, fileDirectoryPath)
        {
        }

        public override string FileExtension
        {
            get
            {
                return ".yaml";
            }
        }

        public override object? DeserializeFileContent(FileInfo file, Type type)
        {
            return file.FromYamlFile(type);
        }
    }
}
