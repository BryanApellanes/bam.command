using Bam.Logging;
using System.Reflection;

namespace Bam.Command
{
    /// <summary>
    /// Abstract base class for argument providers that resolve command method arguments from files
    /// on disk, deserializing file content into the appropriate parameter types.
    /// </summary>
    public abstract class FileBrokeredCommandArgumentProvider : Loggable, IBrokeredCommandArgumentProvider
    {
        /// <summary>
        /// Initializes a new instance of <see cref="FileBrokeredCommandArgumentProvider"/> with the specified argument parser and file directory.
        /// </summary>
        /// <param name="commandArgumentParser">The parser used to parse raw command-line arguments.</param>
        /// <param name="fileDirectoryPath">The directory path where argument files are located.</param>
        public FileBrokeredCommandArgumentProvider(ICommandArgumentParser commandArgumentParser, string fileDirectoryPath)
        {
            this.CommandArgumentParser = commandArgumentParser;
            this.FileDirectoryPath = fileDirectoryPath;
        }

        /// <summary>
        /// Gets the command argument parser used to parse raw arguments.
        /// </summary>
        public ICommandArgumentParser CommandArgumentParser { get; private set; }

        /// <summary>
        /// Gets or sets the directory path where argument files are located.
        /// </summary>
        public string FileDirectoryPath { get; set; }

        /// <summary>
        /// Gets the file extension (e.g., ".json", ".yaml") used to locate argument files.
        /// </summary>
        public abstract string FileExtension { get; }

        /// <summary>
        /// Resolves command arguments for the specified method by parsing the raw arguments and
        /// deserializing file content for complex types, or converting directly for numbers and strings.
        /// </summary>
        /// <param name="methodInfo">The method whose parameters define the expected argument types.</param>
        /// <param name="arguments">The raw command-line arguments to resolve.</param>
        /// <returns>An array of resolved argument values matching the method's parameter list.</returns>
        public object?[] GetCommandArguments(MethodInfo methodInfo, string[] arguments)
        {
            Args.ThrowIfNull(methodInfo, nameof(methodInfo));
            Args.ThrowIfNull(arguments, nameof(arguments));

            IParsedCommandArguments parsedCommandArguments = CommandArgumentParser.ParseCommandArguments(arguments);

            ParameterInfo[] parameters = methodInfo.GetParameters();
            if(parsedCommandArguments != null && parameters.Length != parsedCommandArguments.Length)
            {
                throw new InvalidOperationException($"The method {methodInfo.Name} expects {parameters.Length} arguments but received {parsedCommandArguments.Keys.Length}");
            }

            if(parsedCommandArguments != null)
            {
                object?[] results = new object?[parsedCommandArguments.Length];
                int index = 0;
                foreach (string key in parsedCommandArguments.Keys)
                {
                    ParameterInfo parameter = parameters[index];
                    if (parameter.ParameterType.IsNumberType())
                    {
                        results[index] = Convert.ChangeType(parsedCommandArguments[key], parameter.ParameterType);

                    }
                    else if (parameter.ParameterType == typeof(string))
                    {
                        results[index] = parsedCommandArguments[key];
                    }
                    else
                    {
                        object? arg = GetMethodArgument(parsedCommandArguments, key, parameter);

                        results[index] = arg;
                    }

                    index++;
                }
                return results;
            }
            return Array.Empty<object?>();
        }

        /// <summary>
        /// Resolves method arguments by looking up files named after each parameter and deserializing their content.
        /// </summary>
        /// <param name="methodInfo">The method whose parameters define the expected argument types.</param>
        /// <returns>An array of deserialized argument values.</returns>
        public object?[] GetMethodArguments(MethodInfo methodInfo)
        {
            ParameterInfo[] parameters = methodInfo.GetParameters();
            object?[] results = new object?[parameters.Length];
            int index = 0;
            foreach(ParameterInfo parameter in parameters)
            {
                FileInfo file = this.GetFile(parameter, parameter.Name);
                results[index] = this.DeserializeFileContent(file, parameter.ParameterType);

                index++;
            }

            return results;            
        }

        /// <summary>
        /// Determines whether a file exists for the specified parameter and argument combination.
        /// </summary>
        /// <param name="parameter">The parameter whose type determines the expected file.</param>
        /// <param name="argument">The argument value used to locate the file.</param>
        /// <returns>True if a matching file exists; otherwise false.</returns>
        public bool FileExistsFor(ParameterInfo parameter, string argument)
        {
            try
            {
                FileInfo fileInfo = this.GetFile(parameter, argument);
                return fileInfo.Exists;
            }
            catch (FileNotFoundException fnfe)
            {
                Error(fnfe.Message);

                return false;
            }
        }

        protected FileInfo GetFile(ParameterInfo parameter, string argument)
        {
            FileInfo file = new FileInfo(Path.Combine(FileDirectoryPath, argument));
            if (!file.Exists)
            {
                file = new FileInfo(Path.Combine(FileDirectoryPath, $"{argument}.{this.FileExtension}"));
                if (!file.Exists)
                {
                    throw new FileNotFoundException($"File doesn't exist for the specified parameter and argument (parameter={parameter.Name}) (argument={argument}): {file.FullName}");
                }
            }
            return file;
        }

        /// <summary>
        /// When overridden in a derived class, deserializes the content of the specified file into an object of the given type.
        /// </summary>
        /// <param name="file">The file to deserialize.</param>
        /// <param name="type">The target type to deserialize into.</param>
        /// <returns>The deserialized object, or null if deserialization fails.</returns>
        public abstract object? DeserializeFileContent(FileInfo file, Type type);

        private object? GetMethodArgument(IParsedCommandArguments parsedCommandArguments, string key, ParameterInfo parameter)
        {
            FileInfo file = this.GetFile(parameter, parsedCommandArguments[key]);
            object? arg = this.DeserializeFileContent(file, parameter.ParameterType);
            return arg;
        }
    }
}
