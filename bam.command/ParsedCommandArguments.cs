namespace Bam.Command
{
    /// <summary>
    /// Parses raw command-line arguments into a structured format. The first argument is the context name,
    /// the second is the command name, and remaining arguments are parsed as --key value or -key value pairs.
    /// </summary>
    public class ParsedCommandArguments : IParsedCommandArguments
    {
        const string ArgumentPrefix = "--";
        const string ShortArgumentPrefix = "-";

        /// <summary>
        /// Initializes a new instance by parsing the specified raw arguments into key-value pairs.
        /// </summary>
        /// <param name="arguments">The raw command-line arguments to parse.</param>
        public ParsedCommandArguments(string[] arguments)
        {
            this.OriginalStrings = arguments;
            this._arguments = new Dictionary<string, string>();
            this.ReadArguments();
        }

        /// <summary>
        /// Gets the context name extracted from the first raw argument.
        /// </summary>
        public string ContextName
        {
            get
            {
                return this.OriginalStrings.Length > 0 ? this.OriginalStrings[0].Trim() : string.Empty;
            }
        }

        /// <summary>
        /// Gets the command name extracted from the second raw argument.
        /// </summary>
        public string CommandName
        {
            get
            {
                return this.OriginalStrings.Length > 1 ? this.OriginalStrings[1].Trim() : string.Empty;
            }
        }


        Dictionary<string, string> _arguments;
        /// <summary>
        /// Gets or sets the value of the argument with the specified name.
        /// </summary>
        /// <param name="name">The argument name (key).</param>
        /// <returns>The argument value.</returns>
        public string this[string name]

        {
            get
            {
                return _arguments[name];
            }
            internal set
            {
                _arguments[name] = value;
            }
        }

        /// <summary>
        /// Gets the names (keys) of all parsed arguments.
        /// </summary>
        public string[] Keys
        {
            get
            {
                return _arguments.Keys.ToArray();
            }
        }

        /// <summary>
        /// Gets the number of parsed key-value argument pairs.
        /// </summary>
        public int Length
        {
            get
            {
                return _arguments.Count;
            }
        }

        /// <summary>
        /// Gets the original raw argument strings that were parsed.
        /// </summary>
        public string[] OriginalStrings
        {
            get;
            private set;
        }

        /// <summary>
        /// Determines whether the specified argument name is present in the parsed arguments.
        /// </summary>
        /// <param name="argumentToLookFor">The argument name to search for.</param>
        /// <returns>True if the argument is present; otherwise false.</returns>
        public bool Contains(string argumentToLookFor)
        {
            return _arguments.ContainsKey(argumentToLookFor);
        }

        private void ReadArguments()
        {
            if (this.OriginalStrings == null || this.OriginalStrings.Length < 2)
            {
                return;
            }

            for(int i = 2; i  < this.OriginalStrings.Length; i++)
            {
                int valIndex = i + 1;
                if(valIndex >= this.OriginalStrings.Length)
                {
                    valIndex = -1;
                }
                string arg = this.OriginalStrings[i];
                string key = string.Empty;
                if (arg.StartsWith(ArgumentPrefix))
                {
                    key = arg.Substring(ArgumentPrefix.Length);
                }

                if(string.IsNullOrEmpty(key) && arg.StartsWith(ShortArgumentPrefix))
                {
                    key = arg.Substring(ShortArgumentPrefix.Length);
                }
                
                if(valIndex != -1 && !string.IsNullOrEmpty(key))
                {
                    string stringVal = this.OriginalStrings[valIndex];
                    string val = stringVal;
                    if (stringVal.StartsWith(ShortArgumentPrefix) || stringVal.StartsWith(ArgumentPrefix))
                    {
                        val = "True"; // the next argument is not a value for the current key which implies the current key is a valueless argument.
                    }

                    this._arguments[key] = val;
                }
            }
        }
    }
}
