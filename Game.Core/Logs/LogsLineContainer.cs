using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Core.Logs
{
    public class LogsLineContainer
    {
        private readonly int _maxLines;
        private readonly LinkedList<string> _lines;

        public LogsLineContainer(int maxLines = 100)
        {
            _maxLines = maxLines;
            _lines = new LinkedList<string>();
        }

        public void Add(string line)
        {
            if (_lines.Count == _maxLines)
                _lines.RemoveFirst();

            _lines.AddLast(line);
        }

        public IEnumerable<string> Lines => _lines;

        public string Text => string.Join(Environment.NewLine, _lines.ToArray());
    }
}