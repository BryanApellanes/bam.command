namespace Bam.Command
{
    public class CommandBrokerEventArgs : EventArgs
    {
        public string[] Arguments { get; set; }
        public ICommandBroker CommandBroker { get; set; }
        public IBrokeredCommandContext CommandContext { get; set; }
        public IBrokeredCommandResult Command { get; set; }
        public Exception Exception { get; set; }
    }
}
