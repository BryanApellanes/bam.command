using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class ProcessCommandContext : BrokeredCommandContext
    {
        public ProcessCommandContext(FileInfo executableFile, ProcessCommandRunner commandRunner) : base(Path.GetFileNameWithoutExtension(executableFile.Name), new ProcessCommandInitializer(), commandRunner)
        {
            this.ExecutableFile = executableFile;
        }

        public FileInfo ExecutableFile { get; set; }
    }
}
