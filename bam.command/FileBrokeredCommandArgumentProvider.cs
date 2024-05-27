using Bam;
using Bam.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public abstract class FileBrokeredCommandArgumentProvider : Loggable, IBrokeredCommandArgumentProvider
    {
        public FileBrokeredCommandArgumentProvider(ICommandArgumentParser commandArgumentParser, string fileDirectoryPath)
        {
            this.CommandArgumentParser = commandArgumentParser;
            this.FileDirectoryPath = fileDirectoryPath;
        }

        public ICommandArgumentParser CommandArgumentParser { get; private set; }

        public string FileDirectoryPath { get; set; }

        public abstract string FileExtension { get; }

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

        public abstract object? DeserializeFileContent(FileInfo file, Type type);

        private object? GetMethodArgument(IParsedCommandArguments parsedCommandArguments, string key, ParameterInfo parameter)
        {
            FileInfo file = this.GetFile(parameter, parsedCommandArguments[key]);
            object? arg = this.DeserializeFileContent(file, parameter.ParameterType);
            return arg;
        }
    }
}
