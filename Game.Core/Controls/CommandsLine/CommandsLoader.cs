using System.Collections.Generic;
using Game.Core.Controls.CommandsLine.Commands;
using Game.Core.Logs;

namespace Game.Core.Controls.CommandsLine
{
    public class CommandsLoader : ICommandsLoader
    {
        private readonly ILogger _logger;

        public CommandsLoader(ILogger logger)
        {
            _logger = logger;
        }

        public IEnumerable<ICommand> Load()
        {
            yield return new DisplayText(_logger);
        }
    }
}
