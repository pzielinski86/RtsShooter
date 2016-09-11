using System.Collections.Generic;

namespace Game.Core.Logs
{
    public interface ILogger
    {
        void WriteLine(string text);
        string Text { get; }
        IEnumerable<string> Lines { get; }
    }
}