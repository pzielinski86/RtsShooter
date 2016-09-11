using System.Collections.Generic;
using Game.Core.Logs;


namespace Game.Core.Controls.CommandsLine
{
    public class CommandLineExecuter : ICommandLineExecuter
    {
        private readonly ILogger _logger;
        private readonly IEnumerable<ICommand> _supportedCommands;

        public CommandLineExecuter(ILogger logger, IEnumerable<ICommand> supportedCommands)
        {
            _logger = logger;
            _supportedCommands = supportedCommands;
        }

        public void Execute(string commandLine)
        {
            foreach (ICommand command in _supportedCommands)
            {
                if (command.TryToLoadFromText(commandLine))
                {
                    command.Execute();
                    return;
                }
            }

            _logger.WriteLine("Command was not recognized.");
        }
    }
}
