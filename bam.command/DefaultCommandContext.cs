﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public class DefaultCommandContext : MenuCommandContext
    {
        public const string Name = "DEFAULT";
        public DefaultCommandContext(MenuCommandRunner commandRunner) : base(Name, commandRunner)
        {
        }

        public string ContextName
        {
            get
            {
                return Name;
            }
        }
    }
}
