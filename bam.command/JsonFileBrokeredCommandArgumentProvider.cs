using Bam.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class JsonFileBrokeredCommandArgumentProvider : FileBrokeredCommandArgumentProvider
    {
        public JsonFileBrokeredCommandArgumentProvider(ICommandArgumentParser commandArgumentParser, string jsonFileDirectoryPath) : base(commandArgumentParser, jsonFileDirectoryPath)
        {
        }

        public override string FileExtension
        {
            get
            {
                return ".json";
            }
        }

        public override object? DeserializeFileContent(FileInfo file, Type type)
        {
            return file.FromJsonFile(type);
        }
    }
}
