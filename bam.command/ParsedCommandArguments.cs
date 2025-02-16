namespace Bam.Command
{
    public class ParsedCommandArguments : IParsedCommandArguments
    {
        const string ArgumentPrefix = "--";
        const string ShortArgumentPrefix = "-";

        public ParsedCommandArguments(string[] arguments)
        {
            this.OriginalStrings = arguments;
            this._arguments = new Dictionary<string, string>();
            this.ReadArguments();
        }

        public string ContextName
        {
            get
            {
                return this.OriginalStrings.Length > 0 ? this.OriginalStrings[0].Trim() : string.Empty;
            }
        }

        public string CommandName
        {
            get
            {
                return this.OriginalStrings.Length > 1 ? this.OriginalStrings[1].Trim() : string.Empty;
            }
        }


        Dictionary<string, string> _arguments;
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

        public string[] Keys
        {
            get
            {
                return _arguments.Keys.ToArray();
            }
        }

        public int Length
        {
            get
            {
                return _arguments.Count;
            }
        }

        public string[] OriginalStrings
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the specified argument is present.
        /// </summary>
        /// <param name="argumentToLookFor"></param>
        /// <returns></returns>
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
