using Bam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class SerializedFileBrokeredCommandArgumentProvider : FileBrokeredCommandArgumentProvider
    {
        public const string ArgumentsFolderName = "arguments";
        
        public SerializedFileBrokeredCommandArgumentProvider(ICommandArgumentParser commandArgumentParser) : base(commandArgumentParser, Path.Combine(BamProfile.DataPath, ArgumentsFolderName))
        {
            string dir = Path.Combine(BamProfile.DataPath, ArgumentsFolderName);
            this.JsonFileBrokeredCommandArgumentProvider = new JsonFileBrokeredCommandArgumentProvider(commandArgumentParser, Path.Combine(dir, "json"));
            this.YamlFileBrokeredCommandArgumentProvider = new YamlFileBrokeredCommandArgumentProvider(commandArgumentParser, Path.Combine(dir, "yaml"));
        }

        protected JsonFileBrokeredCommandArgumentProvider JsonFileBrokeredCommandArgumentProvider { get; set; }
        protected YamlFileBrokeredCommandArgumentProvider YamlFileBrokeredCommandArgumentProvider { get; set; }

        public override string FileExtension
        {
            get
            {
                return string.Empty;
            }
        }

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
