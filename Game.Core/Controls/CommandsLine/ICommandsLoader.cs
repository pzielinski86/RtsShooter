using System.Collections.Generic;

namespace Game.Core.Controls.CommandsLine
{
    public interface ICommandsLoader
    {
        IEnumerable<ICommand> Load();
    }
}