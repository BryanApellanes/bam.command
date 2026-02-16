namespace Bam.Command
{
    /// <summary>
    /// Represents parsed command-line arguments with the extracted context name, command name,
    /// and key-value argument pairs.
    /// </summary>
    public interface IParsedCommandArguments
    {
        /// <summary>
        /// Gets the context name extracted from the first argument.
        /// </summary>
        string ContextName { get; }

        /// <summary>
        /// Gets the command name extracted from the second argument.
        /// </summary>
        string CommandName { get; }

        /// <summary>
        /// Gets the value of the argument with the specified name.
        /// </summary>
        /// <param name="name">The argument name.</param>
        /// <returns>The argument value.</returns>
        string this[string name] { get; }

        /// <summary>
        /// Gets the argument names (keys) that were parsed.
        /// </summary>
        string[] Keys { get; }

        /// <summary>
        /// Gets the number of parsed key-value argument pairs.
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Gets the original raw argument strings before parsing.
        /// </summary>
        string[] OriginalStrings { get; }

        /// <summary>
        /// Determines whether the specified argument name is present in the parsed arguments.
        /// </summary>
        /// <param name="argumentToLookFor">The argument name to search for.</param>
        /// <returns>True if the argument is present; otherwise false.</returns>
        bool Contains(string argumentToLookFor);
    }
}