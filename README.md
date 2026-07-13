# bam.command

Base command broker definitions for the Bam Framework — resolves, parses, and runs commands from menus, external processes, and argument files.

## Overview

`bam.command` provides the command-brokering layer that framework CLIs (such as the `bam` tool in bamtk) build on. Its central type, `BamCommandContext`, is a `BamContext` that resolves an `ICommandBroker` from a `ServiceRegistry` and uses it to broker command-line arguments into an executable command (`BamCommandContext.Main`).

Commands are represented as `IBrokeredCommand`/`BrokeredCommand`, resolved by an `IBrokeredCommandContextResolver`, and executed by an `IBrokeredCommandRunner`. Two concrete runner/resolver pairs ship here: `MenuCommandRunner`/`MenuCommandContextResolver` (interactive menu commands) and `ProcessCommandRunner`/`ProcessCommandContextResolver` (external executables). A third scenario — merging both sources — is `CompositeCommandContextResolver`, which lives in the `bam` CLI project itself (bamtk root), not in this repository.

Argument parsing is handled by `CommandArgumentParser`/`ICommandArgumentParser` and `ParsedCommandArguments`, with pluggable argument *providers* for loading arguments from a file: `JsonFileBrokeredCommandArgumentProvider`, `YamlFileBrokeredCommandArgumentProvider`, and `SerializedFileBrokeredCommandArgumentProvider`.

## Key Classes

| Class / Interface | Description |
|---|---|
| `BamCommandContext` | Entry-point `BamContext` implementation; brokers `args` via the registered `ICommandBroker` and exits with the resulting exit code. |
| `ICommandBroker` / `CommandBroker` | Resolves and runs a command from raw arguments. |
| `IBrokeredCommand` / `BrokeredCommand` | A resolved, runnable command. |
| `IBrokeredCommandContextResolver` | Resolves an `IBrokeredCommandContext` from a source (menu, process, composite). |
| `MenuCommandRunner` / `MenuCommandContextResolver` | Runs commands sourced from `[ConsoleCommand]`-decorated menu methods. |
| `ProcessCommandRunner` / `ProcessCommandContextResolver` | Runs commands sourced from external executables discovered on disk. |
| `ICommandArgumentParser` / `CommandArgumentParser` | Parses raw string arguments into `IParsedCommandArguments`. |
| `JsonFileBrokeredCommandArgumentProvider` / `YamlFileBrokeredCommandArgumentProvider` | Load command arguments from a JSON/YAML file. |

## Dependencies

**Project References:** `bam.base`, `bam.console`, `bam.shell`.

**Target Framework:** net10.0, packaged as the `bam.command` NuGet package (Three Headz).

## Running Tests

```bash
dotnet run --project bam.command.tests/bam.command.tests.csproj -- --ut
```
