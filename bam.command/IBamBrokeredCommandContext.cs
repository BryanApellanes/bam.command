namespace Bam.Command
{
    public interface IBamBrokeredCommandContext : IBamContext
    {
        ICommandBroker CommandBroker { get; }
    }
}
