using System;
using System.Text.RegularExpressions;
using Game.Core.Logs;

namespace Game.Core.Controls.CommandsLine.Commands
{
    /// <summary>
    /// Command format: display @text
    /// </summary>
    public class DisplayText:ICommand
    {
        private readonly ILogger _logger;
        public DisplayText(ILogger logger)
        {
            _logger = logger;
        }

        public string Text { get; private set; }

        public string GetHelp()
        {
            throw new NotImplementedException();
        }

        public void Execute()
        {
            _logger.WriteLine(Text);
        }

        public bool TryToLoadFromText(string line)
        {
            var match = Regex.Match(line, @"display ""?([\w ]+)""?");
            Text = match.Groups[1].Value;
            
            return match.Success;
        }
    }
}
