using Bam.Command;
using Bam.DependencyInjection;
using Bam.Test;
using Bam.Services;

namespace Bam
{
    public class ParsedCommandArgumentsShould : UnitTestMenuContainer
    {
        public ParsedCommandArgumentsShould(ServiceRegistry serviceRegistry) : base(serviceRegistry)
        {
        }

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

        [UnitTest]
        public void RemoveArgumentPrefix()
        {
            string testContext = "TestContext_".RandomLetters(6);
            string testCommand = "TestCommand_".RandomLetters(8);
            string argOneValue = "argOneValue_".RandomLetters(16);
            string argTwoValue = "argTwoValue_".RandomLetters(12);
            string aRandomKey = "--".RandomLetters(5);
            string testKey = aRandomKey.Substring(2);

            When.A<ParsedCommandArguments>("removes argument prefix",
                new ParsedCommandArguments(new string[] { testContext, testCommand, "--argOne", argOneValue, aRandomKey, argTwoValue }),
                (args) => args)
            .TheTest
            .ShouldPass(because =>
            {
                because.TheResult.IsNotNull()
                    .As<ParsedCommandArguments>("contains argOne key", a => a?.Contains("argOne") == true)
                    .As<ParsedCommandArguments>($"contains {aRandomKey} key", a => a?.Contains(testKey) == true)
                    .As<ParsedCommandArguments>("argOne value equals expected", a => argOneValue.Equals(a?["argOne"]))
                    .As<ParsedCommandArguments>($"{testKey} value equals expected", a => argTwoValue.Equals(a?[testKey]));
            })
            .SoBeHappy()
            .UnlessItFailed();
        }

        [UnitTest]
        public void RemoveShortArgumentPrefix()
        {
            string testContext = "TestContext_".RandomLetters(8);
            string testCommand = "TestCommand_".RandomLetters(8);
            string argOneKey = "-".RandomLetters(12);
            string argOneValue = 12.RandomLetters();
            string argTwoKey = "--".RandomLetters(8);
            string argTwoValue = 6.RandomLetters();
            string testKey = argOneKey.Substring(1);

            When.A<ParsedCommandArguments>("removes short argument prefix",
                new ParsedCommandArguments(new string[] { testContext, testCommand, argOneKey, argOneValue, argTwoKey, argTwoValue }),
                (args) => args)
            .TheTest
            .ShouldPass(because =>
            {
                because.TheResult.IsNotNull()
                    .As<ParsedCommandArguments>("contains test key", a => a?.Contains(testKey) == true)
                    .As<ParsedCommandArguments>("value equals expected", a => argOneValue.Equals(a?[testKey]));
            })
            .SoBeHappy()
            .UnlessItFailed();
        }
    }
}
