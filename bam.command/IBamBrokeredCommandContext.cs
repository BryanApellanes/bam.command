namespace Bam.Command
{
    /// <summary>
    /// Extends <see cref="IBamContext"/> with access to a command broker for brokered command execution.
    /// </summary>
    public interface IBamBrokeredCommandContext : IBamContext
    {
        /// <summary>
        /// Gets the command broker used to resolve and execute commands.
        /// </summary>
        ICommandBroker CommandBroker { get; }
    }
}
