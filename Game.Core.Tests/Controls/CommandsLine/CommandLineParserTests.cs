using NSubstitute;
using NUnit.Framework;

using Game.Core.Controls.CommandsLine;
using Game.Core.Logs;

namespace Game.Core.Tests.Controls.CommandsLine
{
    [TestFixture]
    public class CommandLineParserTests
    {
        private ILogger _logger;

        [SetUp]
        public void Setup()
        {
            _logger = Substitute.For<ILogger>();
        }

        [Test]
        public void When_SecondCommandIsRecognized_Then_ItShouldBeExecuted()
        {
            ICommand wrongCommand = Substitute.For<ICommand>();
            ICommand goodCommand = Substitute.For<ICommand>();
            const string commanLine = "test";

            goodCommand.TryToLoadFromText(commanLine).Returns(true);

            var commandLineParser = new CommandLineExecuter(_logger,new[] {wrongCommand, goodCommand});
            commandLineParser.Execute(commanLine);

            wrongCommand.Received(0).Execute();
            goodCommand.Received(1).Execute();
        }

        [Test]
        public void When_ThereIsNoMatchedCommand_Then_NoCommandsShouldBeExecute_And_LogShouldBeAdded()
        {
            ICommand wrongCommand = Substitute.For<ICommand>();
            ICommand wrongCommand2 = Substitute.For<ICommand>();

            var commandLineParser = new CommandLineExecuter(_logger,new[] { wrongCommand, wrongCommand2 });
            commandLineParser.Execute("test");

            wrongCommand.Received(0).Execute();
            wrongCommand2.Received(0).Execute();
            _logger.Received(1).WriteLine(Arg.Any<string>());
        }

        [Test]
        public void When_AllCommandsAreMatched_Then_FirstOneShouldBeExecuted()
        {
            ICommand goodCommand1 = Substitute.For<ICommand>();
            ICommand goodCommand2 = Substitute.For<ICommand>();
            
            const string commanLine = "test";

            goodCommand1.TryToLoadFromText(commanLine).Returns(true);
            goodCommand2.TryToLoadFromText(commanLine).Returns(true);

            var commandLineParser = new CommandLineExecuter(_logger,new[] { goodCommand1, goodCommand2 });
            commandLineParser.Execute(commanLine);

            goodCommand1.Received(1).Execute();
            goodCommand2.Received(0).Execute();
        }
    }
}
