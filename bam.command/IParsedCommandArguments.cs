namespace Bam.Command
{
    public interface IParsedCommandArguments
    {
        string ContextName { get; }
        string CommandName { get; }
        string this[string name] { get; }

        string[] Keys { get; }
        int Length { get; }
        string[] OriginalStrings { get; }

        bool Contains(string argumentToLookFor);
    }
}