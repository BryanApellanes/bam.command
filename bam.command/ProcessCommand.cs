namespace Bam.Command
{
    public class ProcessCommand : IBrokeredCommand
    {
        public ProcessCommand(string name, string exePath) 
        {
            this.CommandName = name;
            this.CommandSelector= name;
            this.ExePath= exePath;
        }

        public string CommandName { get; private set; }

        public string CommandSelector { get; private set; }

        public string ExePath { get; private set; }
    }
}
