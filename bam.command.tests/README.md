# bam.command.tests

Unit tests for the bam.command project, validating command argument parsing and context resolution.

## Overview

bam.command.tests is an executable test project that uses the Bam Framework's menu-driven test runner. Tests are organized into `[UnitTestMenu]`-adorned classes containing `[UnitTest]`-attributed methods, and the entry point calls `BamConsoleContext.StaticMain(args)`.

The current test suite focuses on `ParsedCommandArguments`, verifying that positional context and command names are correctly extracted, that `--` and `-` argument prefixes are properly stripped, and that key-value pairs are parsed from the argument array. The project also includes a `TestConsoleMenu` fixture class demonstrating an empty `ConsoleMenuContainer` for test context.

## Key Classes

| Class | Description |
|---|---|
| `ParsedCommandArgumentsShould` | Unit tests for `ParsedCommandArguments`: validates `ContextName`/`CommandName` extraction, `--` prefix removal, `-` short prefix removal, and key-value pair parsing. |
| `TestConsoleMenu` | Empty `ConsoleMenuContainer` subclass used as a test fixture, adorned with `[ConsoleMenu("TestContext")]`. |

## Dependencies

**Project References:**
- `bam.base` -- Core framework primitives
- `bam.console` -- Console context, menu containers
- `bam.data.repositories` -- Repository abstractions
- `bam.data.schema` -- Schema management
- `bam.data` -- Data framework
- `bam.test` -- Test framework (`UnitTestMenuContainer`, `When`, `Because`, etc.)
- `bam.command` -- The project under test

**Target Framework:** net10.0
**Output Type:** Exe

## Usage Examples

### Run all unit tests
```bash
dotnet run --project bam.command.tests.csproj -- --ut
```

### Test structure example
```csharp
[UnitTest]
public void SetContextAndCommand()
{
    string testContext = "TestContext_".RandomLetters(6);
    string testCommand = "TestCommand_".RandomLetters(8);

    When.A<ParsedCommandArguments>("sets context and command",
        new ParsedCommandArguments(new string[] { testContext, testCommand }),
        (args) => args)
    .TheTest
    .ShouldPass(because =>
    {
        because.TheResult.IsNotNull()
            .As<ParsedCommandArguments>("ContextName equals expected", a => testContext.Equals(a?.ContextName))
            .As<ParsedCommandArguments>("CommandName equals expected", a => testCommand.Equals(a?.CommandName));
    })
    .SoBeHappy()
    .UnlessItFailed();
}
```

## Known Gaps / Not Yet Implemented

- No tests exist for `CommandBroker`, `MenuCommandRunner`, `ProcessCommandRunner`, or end-to-end command dispatch.
- No tests exist for `MenuCommandContext`, `ProcessCommandContext`, or the context resolver chain.
- The test project references `bam.data.repositories`, `bam.data.schema`, and `bam.data` but does not currently contain any data-related tests.
