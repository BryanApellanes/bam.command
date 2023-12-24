﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Command
{
    public interface ICommandInitializer
    {
        IDictionary<string, ICommand> InitializeCommands();
    }
}
