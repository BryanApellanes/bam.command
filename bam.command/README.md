# bam.command

Command broker framework for dispatching named commands through contextual resolution, supporting both menu-based and external process execution.

## Overview

bam.command provides a brokered command execution model where command-line arguments are resolved to a context, a command within that context, and then executed by the appropriate runner. The `CommandBroker` is the central orchestrator: it resolves a `BrokeredCommandContext` from arguments using an `IBrokeredCommandContextResolver`, then dispatches execution to the context's `IBrokeredCommandRunner`. This architecture allows different command types (menu items, external processes) to coexist under a unified dispatch model.

The framework defines two concrete command types. `MenuCommand` wraps an `IMenuItem` from the bam.shell menu system, and `MenuCommandRunner` executes it by resolving the declaring type via DI, gathering method arguments from an `IBrokeredCommandArgumentProvider`, and invoking the method reflectively. `ProcessCommand` wraps an external executable path, and `ProcessCommandRunner` executes it as a child process via bam.console's `string.Run()` extension method. Command arguments can be provided from files via `JsonFileBrokeredCommandArgumentProvider`, `YamlFileBrokeredCommandArgumentProvider`, or `SerializedFileBrokeredCommandArgumentProvider`.

`BamCommandContext` provides a default service registry and a `Main` method for running command-based applications. The `ParsedCommandArguments` class parses positional arguments (context name at index 0, command name at index 1) and `--key value` / `-key value` named arguments from index 2 onward.

## Key Classes

| Class | Description |
|---|---|
| `CommandBroker` | Abstract broker that resolves contexts and dispatches commands. Fires lifecycle events for initialization, context resolution, and command execution. |
| `ICommandBroker` | Interface for command broking: `BrokerCommand(args)`, `ResolveContext(args)`, lifecycle events. |
| `BrokeredCommandContext` | Abstract base for command contexts. Holds a set of named commands and an `IBrokeredCommandRunner`. Resolves command selectors from arguments. |
| `DefaultCommandContext` | Default context used when no specific context is resolved. |
| `BrokeredCommand` | Result object from command execution: holds broker, context, command, and run result. |
| `MenuCommand` | `IBrokeredCommand` wrapping an `IMenuItem` from the shell menu system. |
| `MenuCommandRunner` | Executes `MenuCommand` by resolving instance via DI and invoking the menu item method. |
| `MenuCommandContext` | Context holding menu commands. |
| `MenuCommandContextResolver` | Resolves context name and loads `MenuCommandContext` instances from `MenuSpecs`. |
| `MenuCommandInitializer` | Initializes menu commands from a menu's items. |
| `ProcessCommand` | `IBrokeredCommand` wrapping an external executable path and name. |
| `ProcessCommandRunner` | Executes `ProcessCommand` by running the external process. |
| `ProcessCommandContext` | Context for process-based commands. |
| `ProcessCommandContextResolver` | Resolves process command contexts. |
| `ProcessCommandInitializer` | Initializes process commands (stub -- not yet implemented). |
| `BamCommandContext` | Singleton application context for command-based applications. Provides default service registry and `Main` entry point. |
| `ParsedCommandArguments` | Parses positional + named arguments: `ContextName` (index 0), `CommandName` (index 1), `--key value` pairs (index 2+). |
| `CommandArgumentParser` | `ICommandArgumentParser` implementation for parsing command arguments. |
| `CommandContextResolver` | Base implementation of `IBrokeredCommandContextResolver`. |
| `JsonFileBrokeredCommandArgumentProvider` | Provides command arguments from a JSON file. |
| `YamlFileBrokeredCommandArgumentProvider` | Provides command arguments from a YAML file. |
| `FileBrokeredCommandArgumentProvider` | Base class for file-based argument providers. |

## Dependencies

**Project References:**
- `bam.base` -- Core framework primitives, DI, logging
- `bam.console` -- Console context, argument parsing, menu containers
- `bam.shell` -- Menu system abstractions (`IMenuItem`, `MenuSpecs`, etc.)

**Target Framework:** net10.0
**Output Type:** Library (NuGet package, v2.0.0)

## Usage Examples

### Command-based application entry point
```csharp
using Bam.Command;

class Program
{
    static void Main(string[] args)
    {
        BamCommandContext.Main(args);
    }
}
```
Run with: `myapp contextName commandName --argKey argValue`

### Parse command arguments
```csharp
var parsed = new ParsedCommandArguments(new[] { "myContext", "doStuff", "--verbose", "true" });
string context = parsed.ContextName;  // "myContext"
string command = parsed.CommandName;  // "doStuff"
string verbose = parsed["verbose"];   // "true"
```

### Define a menu command context
```csharp
[ConsoleMenu("My Context")]
public class MyCommandContainer : ConsoleMenuContainer
{
    public MyCommandContainer(ServiceRegistry sr) : base(sr) { }

    [ConsoleCommand("Run task", "Runs the specified task")]
    public void RunTask(string taskName)
    {
        Console.WriteLine($"Running {taskName}");
    }
}
```

## Known Gaps / Not Yet Implemented

- `ProcessCommandInitializer.InitializeCommands()` throws `NotImplementedException`. The intended behavior is to scan `BamDir/commands` for executables, but this is not yet implemented.
- `CommandBroker.Initialize()` contains `// TODO: review the best way to switch the logger subscription on and off for diagnostics` -- the logger subscription call is commented out.
