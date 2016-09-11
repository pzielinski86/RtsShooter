using System;
using System.Linq;
using Game.Core.Unity;
using UnityEngine;
using ILogger = Game.Core.Logs.ILogger;

namespace Game.Core.Controls.CommandsLine
{
    public class CommandWindow
    {
        private const float CommandLineWindowMargin = 10;
        private readonly IInput _input;
        private readonly IGui _gui;
        private readonly ICommandLineExecuter _commandLineExecuter;
        private readonly ILogger _logger;

        private string _currentEditableCommandLine = string.Empty;

        public CommandWindow(IInput input, IGui gui, ICommandLineExecuter commandLineExecuter, ILogger logger)
        {
            _input = input;
            _gui = gui;
            _commandLineExecuter = commandLineExecuter;
            _logger = logger;     
        }

        public bool IsVisible { get; set; }

        public void UpdateGui(int screenWidth, int screenHeight)
        {
            if (_input.IsKeyDown(KeyCode.Tab))
                IsVisible = !IsVisible;

            if (!IsVisible)
                return;

            if (_input.IsKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(_currentEditableCommandLine))
            {
                _logger.WriteLine(_currentEditableCommandLine);
                _commandLineExecuter.Execute(_currentEditableCommandLine);
                _currentEditableCommandLine = string.Empty;
            }

            string[] linesToDisplay = GetLinesToDisplay(screenHeight);

            DisplayHistoricLogs(linesToDisplay,screenWidth, screenHeight);
            DisplayEditableCommandLine(linesToDisplay,screenWidth);
        }

        private int GetMaxLinesCapacity(float screenHeight)
        {
            float height = screenHeight - CommandLineWindowMargin;

            return (int)System.Math.Floor(height / _gui.GetLabelHeight());
        }

        private string[] GetLinesToDisplay(float screenHeight)
        {
            string[] lines = _logger.Lines.ToArray();
            int maxCapacity = GetMaxLinesCapacity(screenHeight);

            int linesToSkip = lines.Length > maxCapacity ? lines.Length - maxCapacity : 0;

            return lines.Skip(linesToSkip).ToArray();
        }

        private void DisplayHistoricLogs(string[] linesToDisplay, float screenWidth, float screenHeight)
        {
            var commandLineWindow = new Rect(CommandLineWindowMargin, CommandLineWindowMargin,
                screenWidth - CommandLineWindowMargin, screenHeight - CommandLineWindowMargin);

            string commandRawText = string.Join(Environment.NewLine, linesToDisplay);

            var backgroundColor = new Color(0.2f, 0.2f, 0.2f, 0.6f);
            _gui.Label(commandLineWindow, commandRawText, TextAnchor.UpperLeft, backgroundColor,CommandLineWindowMargin);

        }

        private void DisplayEditableCommandLine(string[] linesToDisplay, float screenWidth)
        {
            const float textFieldHeight = 23;

            float posY = 2*CommandLineWindowMargin + linesToDisplay.Length * _gui.GetLabelHeight();

            var editCommandLineWindow = new Rect(CommandLineWindowMargin, posY,
                screenWidth - CommandLineWindowMargin, textFieldHeight);

            _currentEditableCommandLine = _gui.TextField(editCommandLineWindow, _currentEditableCommandLine);
        }
    }
}
