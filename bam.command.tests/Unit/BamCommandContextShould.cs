using Bam.Command;
using Bam.DependencyInjection;
using Bam.Test;
using Bam.Services;

namespace Bam.Command.Tests.Unit
{
    public class BamCommandContextShould : UnitTestMenuContainer
    {
        public BamCommandContextShould(ServiceRegistry serviceRegistry) : base(serviceRegistry)
        {
        }

        [UnitTest]
        public void BuildDiagnosticForNullRunResult()
        {
            string[] args = new string[] { "nope", "whatever" };

            When.A<BamCommandContext>("builds a diagnostic failure message when RunResult is null",
                BamCommandContext.Current,
                (context) =>
                {
                    string message = BamCommandContext.GetFailureMessage(BrokeredCommand.Empty, args);
                    return new FailureMessageOutcome(
                        !string.IsNullOrEmpty(message),
                        message.Contains("nope whatever"));
                })
            .TheTest
            .ShouldPass<FailureMessageOutcome>((because, outcome) =>
            {
                because.ItsTrue("the failure message is non-empty", outcome.MessageIsNonEmpty);
                because.ItsTrue("the failure message names the unresolved arguments", outcome.MessageNamesArguments);
            })
            .SoBeHappy()
            .UnlessItFailed();
        }

        [UnitTest]
        public void PassThroughRunResultMessage()
        {
            string expectedMessage = "the command failed for a specific reason";
            BrokeredCommand failedCommand = new BrokeredCommand(null!, null!, null, new BrokeredCommandRunResult
            {
                Success = false,
                Message = expectedMessage
            });

            When.A<BamCommandContext>("passes through the RunResult message of a failed command",
                BamCommandContext.Current,
                (context) => BamCommandContext.GetFailureMessage(failedCommand, new string[] { "some", "args" }))
            .TheTest
            .ShouldPass<string>((because, message) =>
            {
                because.ItsTrue("the RunResult message is passed through unchanged", expectedMessage.Equals(message));
            })
            .SoBeHappy()
            .UnlessItFailed();
        }

        [UnitTest]
        public void BuildDiagnosticForEmptyRunResultMessage()
        {
            BrokeredCommand failedCommand = new BrokeredCommand(null!, null!, null, new BrokeredCommandRunResult
            {
                Success = false,
                Message = string.Empty
            });

            When.A<BamCommandContext>("builds a diagnostic failure message when the RunResult message is empty",
                BamCommandContext.Current,
                (context) => BamCommandContext.GetFailureMessage(failedCommand, new string[] { "orphan" }))
            .TheTest
            .ShouldPass<string>((because, message) =>
            {
                because.ItsTrue("the failure message is non-empty", !string.IsNullOrEmpty(message));
                because.ItsTrue("the failure message names the unresolved arguments", message.Contains("orphan"));
            })
            .SoBeHappy()
            .UnlessItFailed();
        }

        private sealed record FailureMessageOutcome(bool MessageIsNonEmpty, bool MessageNamesArguments);
    }
}
