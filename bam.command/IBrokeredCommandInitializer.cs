namespace Bam.Command
{
    public interface IBrokeredCommandInitializer
    {
        IDictionary<string, IBrokeredCommand> InitializeCommands();
    }
}
