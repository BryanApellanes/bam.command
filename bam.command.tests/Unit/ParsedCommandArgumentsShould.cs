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
            ParsedCommandArguments args = new ParsedCommandArguments(new string[] { testContext, testCommand });

            args.ContextName.ShouldEqual(testContext, "ContextName was not properly set.");
            args.CommandName.ShouldEqual(testCommand, "CommandName was not properly set.");
        }

        [UnitTest]
        public void RemoveArgumentPrefix()
        {
            string testContext = "TestContext_".RandomLetters(6);
            string testCommand = "TestCommand_".RandomLetters(8);
            string argOneValue = "argOneValue_".RandomLetters(16);
            string argTwoValue = "argTwoValue_".RandomLetters(12);
            string aRandomKey = "--".RandomLetters(5);
            ParsedCommandArguments args = new ParsedCommandArguments(new string[] { testContext, testCommand, "--argOne", argOneValue, aRandomKey, argTwoValue });

            string testKey = aRandomKey.Substring(2);
            args.Contains("argOne").ShouldBeTrue("argOne key missing");
            args.Contains(testKey).ShouldBeTrue($"{aRandomKey} key missing");

            args["argOne"].ShouldEqual(argOneValue);
            args[testKey].ShouldEqual(argTwoValue);
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

            ParsedCommandArguments args = new ParsedCommandArguments(new string[] { testContext, testCommand, argOneKey, argOneValue, argTwoKey, argTwoValue });

            string testKey = argOneKey.Substring(1);
            args.Contains(testKey).ShouldBeTrue("test argument key was not present");
            args[testKey].ShouldEqual(argOneValue);
        }
    }
}
