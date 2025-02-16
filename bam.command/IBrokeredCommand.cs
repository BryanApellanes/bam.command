namespace Bam
{
    public interface IBrokeredCommand
    {
        string CommandName { get; }
        string CommandSelector { get; }
    }
}
