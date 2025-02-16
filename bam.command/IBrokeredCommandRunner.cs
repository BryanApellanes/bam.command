namespace Bam.Command
{
    public interface IBrokeredCommandRunner
    {
        IBrokeredCommandRunResult Run(IBrokeredCommand command, string[] arguments);
    }
}
