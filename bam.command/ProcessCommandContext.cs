using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class ProcessCommandContext : CommandContext
    {
        public ProcessCommandContext(FileInfo executableFile) : base(Path.GetFileNameWithoutExtension(executableFile.Name), new ProcessCommandInitializer())
        {
            this.ExecutableFile = executableFile;
        }

        public FileInfo ExecutableFile { get; set; }
    }
}
