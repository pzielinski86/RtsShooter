namespace Game.Core.Controls.CommandsLine
{
    public interface ICommand
    {
        string GetHelp();
        void Execute();

        bool TryToLoadFromText(string line);
    }
}
