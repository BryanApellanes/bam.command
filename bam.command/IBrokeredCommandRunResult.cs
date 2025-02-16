namespace Bam
{
    public interface IBrokeredCommandRunResult
    {
        string Message { get; }
        bool Success { get; }
        object Result { get; }
        string CommandName { get; }
        string[] Arguments { get; }
    }
}
