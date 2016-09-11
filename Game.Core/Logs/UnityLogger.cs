using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.Logs
{
    public sealed class UnityLogger : ILogger
    {
        private readonly LogsLineContainer _logsLineContainer;

        public UnityLogger()
        {
            _logsLineContainer = new LogsLineContainer();
        }

        public void WriteLine(string text)
        {
            _logsLineContainer.Add(text);
            Debug.Log(string.Format("{0}{1}", text, Environment.NewLine));
        }

        public string Text
        {
            get { return _logsLineContainer.Text; }
        }

        public IEnumerable<string> Lines
        {
            get { return _logsLineContainer.Lines; }
        }
    }
}