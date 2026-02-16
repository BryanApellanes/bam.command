namespace Bam.Command
{
    /// <summary>
    /// Defines a parser that converts raw command-line argument strings into structured parsed arguments.
    /// </summary>
    public interface ICommandArgumentParser
    {
        /// <summary>
        /// Parses the specified raw arguments into a structured representation.
        /// </summary>
        /// <param name="arguments">The raw command-line arguments to parse.</param>
        /// <returns>An <see cref="IParsedCommandArguments"/> instance containing the parsed results.</returns>
        IParsedCommandArguments ParseCommandArguments(string[] arguments);
    }
}
