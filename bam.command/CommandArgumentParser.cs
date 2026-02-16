namespace Bam.Command
{
    /// <summary>
    /// Default implementation of <see cref="ICommandArgumentParser"/> that parses command-line arguments
    /// into a <see cref="ParsedCommandArguments"/> instance.
    /// </summary>
    public class CommandArgumentParser : ICommandArgumentParser
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CommandArgumentParser"/>.
        /// </summary>
        public CommandArgumentParser() { }

        /// <summary>
        /// Parses the specified raw arguments into a structured <see cref="IParsedCommandArguments"/> representation.
        /// </summary>
        /// <param name="arguments">The raw command-line arguments to parse.</param>
        /// <returns>A <see cref="IParsedCommandArguments"/> instance containing the parsed argument key-value pairs.</returns>
        public IParsedCommandArguments ParseCommandArguments(string[] arguments)
        {
            return new ParsedCommandArguments(arguments);
        }
    }
}
