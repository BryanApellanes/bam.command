namespace Bam.Command
{
    /// <summary>
    /// Placeholder initializer for process commands. Currently throws <see cref="NotImplementedException"/>.
    /// </summary>
    public class ProcessCommandInitializer : IBrokeredCommandInitializer
    {
        /// <summary>
        /// Initializes commands by scanning for executables. Not yet implemented.
        /// </summary>
        /// <returns>This method currently throws <see cref="NotImplementedException"/>.</returns>
        public IDictionary<string, Bam.IBrokeredCommand> InitializeCommands()
        {
            // look in BamDir/commands for executables
            
            throw new NotImplementedException();
        }
    }
}
