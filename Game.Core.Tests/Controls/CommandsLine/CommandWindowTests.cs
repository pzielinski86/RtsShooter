using NSubstitute;
using NUnit.Framework;
using Game.Core.Controls.CommandsLine;
using Game.Core.Unity;
using UnityEngine;
using ILogger = Game.Core.Logs.ILogger;

namespace Game.Core.Tests.Controls.CommandsLine
{
    [TestFixture]
    public class CommandWindowTests
    {
        private IGui _gui;
        private IInput _inputMock;
        private CommandWindow _commandWindow;
        private ILogger _logger;
        private ICommandLineExecuter _commandLineExecuter;


        [SetUp]
        public void SetUp()
        {
            _logger = Substitute.For<ILogger>();
            _inputMock = Substitute.For<IInput>();
            _gui = Substitute.For<IGui>();
            _commandLineExecuter = Substitute.For<ICommandLineExecuter>();

            _commandWindow = new CommandWindow(_inputMock, _gui, _commandLineExecuter,_logger);
        }

        [Test]
        public void When_MaximalWindowCapacityIsExceeded_Then_FirstLine_Should_BeRemoved()
        {
            const int screenHeight = 250;

            _gui.GetLabelHeight().Returns(100);
            _logger.Lines.Returns(new[] {"A", "B","C"});
            _commandWindow.IsVisible = true;
            
            // exceed a maximal capacity
            _commandWindow.UpdateGui(0, screenHeight);

            _gui.Received(1).Label(Arg.Any<Rect>(), "B\r\nC", Arg.Any<TextAnchor>(), Arg.Any<Color>(),Arg.Any<float>());

        }

        [Test]
        public void When_EnterIsPressed_And_ThereIsContent_Then_ItShouldBeExecuted()
        {
            const string line = "line";
            _commandWindow.IsVisible = true;

            _gui.TextField(Arg.Any<Rect>(), Arg.Any<string>()).Returns(line);
            _inputMock.IsKeyDown(KeyCode.Return).Returns(true);

            _commandWindow.UpdateGui(0, 0);
            _commandWindow.UpdateGui(0, 0);

            _commandLineExecuter.Received(1).Execute(line);
            _logger.Received(1).WriteLine(line);
        }

        [Test]
        public void When_EnterIsPressed_And_ThereIsNoCommandText_Then_ItShouldNotBeExecuted()
        {
            string line = string.Empty;
            _commandWindow.IsVisible = true;

            _gui.TextArea(Arg.Any<Rect>(), Arg.Any<string>()).Returns(line);
            _inputMock.IsKeyDown(KeyCode.Return).Returns(true);

            _commandWindow.UpdateGui(0, 0);

            _commandLineExecuter.Received(0).Execute(line);
        }

        [Test]
        public void When_EnterIsPressed_And_ThereIsCommandText_Then_TextFieldShouldBeCleared()
        {
            _commandWindow.IsVisible = true;

            _gui.TextArea(Arg.Any<Rect>(), Arg.Any<string>()).Returns("test");
            _inputMock.IsKeyDown(KeyCode.Return).Returns(true);

            _commandWindow.UpdateGui(0, 0);

            _gui.Received(1).TextField(Arg.Any<Rect>(), string.Empty);
        }

        [Test]
        public void When_WindowIsDisplayed_And_TabIsPressed_Then_WindowShouldDisappear()
        {
            _commandWindow.IsVisible = true;
            _inputMock.IsKeyDown(KeyCode.Tab).Returns(true);

            _commandWindow.UpdateGui(0, 0);

            Assert.That(_commandWindow.IsVisible, Is.False);
        }

        [Test]
        public void When_WindowIsHidden_And_TabIsPressed_Then_WindowShouldBeDisplayed()
        {
            _commandWindow.IsVisible = false;
            _inputMock.IsKeyDown(KeyCode.Tab).Returns(true);

            _commandWindow.UpdateGui(0, 0);

            Assert.That(_commandWindow.IsVisible, Is.True);
        }

        [Test]
        public void When_WindowIsHidden_And_EnterIsPressed_Then_WindowShouldNotBeDisplayed()
        {
            _commandWindow.IsVisible = false;
            _inputMock.IsKeyDown(KeyCode.Tab).Returns(false);

            _commandWindow.UpdateGui(0, 0);

            Assert.That(_commandWindow.IsVisible, Is.False);
        }

        [Test]
        public void When_WindowIsDisplayed_Then_TextAreaShouldBeCalled()
        {
            _inputMock.IsKeyDown(KeyCode.Tab).Returns(true);

            _commandWindow.UpdateGui(0, 0);

            _gui.Received(1).TextField(Arg.Any<Rect>(), Arg.Any<string>());
        }
    }
}
