namespace Bam.Command
{
    public interface IBrokeredCommandContextResolver
    {
        IDictionary<string, IBrokeredCommandContext> LoadContexts();
        string ResolveContextName(string[] arguments);
    }
}
