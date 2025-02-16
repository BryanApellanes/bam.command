namespace Bam.Command
{
    public class ProcessCommandInitializer : IBrokeredCommandInitializer
    {
        public IDictionary<string, Bam.IBrokeredCommand> InitializeCommands()
        {
            // look in BamDir/commands for executables
            
            throw new NotImplementedException();
        }
    }
}
