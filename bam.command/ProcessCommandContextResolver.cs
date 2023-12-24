﻿using Bam.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class ProcessCommandContextResolver : CommandContextResolver
    {
        const string bamPrefix = "bam";

        public ProcessCommandContextResolver()
        {
            this.FileFilter = this.IsValidCommand;
            this.SetDirectories();
        }

        public ProcessCommandContextResolver(Func<string, bool> fileFilter) 
        {
            this.FileFilter = fileFilter;
            this.SetDirectories();
        }

        /// <summary>
        /// Gets or sets the directories to search for executables in.
        /// </summary>
        public DirectoryInfo[] SearchDirectories 
        { 
            get; 
            set; 
        }

        public Func<string, bool> FileFilter { get; set; }

        public override IDictionary<string, ICommandContext> LoadContexts()
        {
            Dictionary<string, ICommandContext> commandContexts = new Dictionary<string, ICommandContext>();

            foreach(DirectoryInfo directoryInfo in SearchDirectories)
            {
                foreach(FileInfo file in directoryInfo.GetFiles())
                {
                    if (IsValidCommand(file.FullName))
                    {
                        string contextName = Path.GetFileNameWithoutExtension(file.Name);
                        if(contextName.Length > 3 && contextName.ToLowerInvariant().StartsWith(bamPrefix))
                        {
                            contextName = contextName.TruncateFront(bamPrefix.Length);
                        }
                        commandContexts.Add(contextName, new ProcessCommandContext(file));
                    }
                }
            }

            return commandContexts;
        }

        protected virtual void SetDirectories()
        {
            this.SearchDirectories = new DirectoryInfo[] { new DirectoryInfo(BamProfile.ToolkitPath) };
        }

        protected bool IsValidCommand(string path)
        {
            return Path.GetExtension(path).ToLowerInvariant().Equals(".exe") ||
                Path.GetFileName(path).ToLowerInvariant().StartsWith(bamPrefix);
        }
    }
}
