using LoggerProject.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoggerProject.Interfaces
{
    /// <summary>
    /// Интерфейс лога, который собирает логи в виде дерева для дальнейшей визуализации в интерфейсе
    /// </summary>
    public interface ILog
    {
        ILog Owner { get; }

        List<ILog> Sublogs { get; }

        DateTime Date { get; }

        uint LogLevel { get; }

        LogType LogType { get; }

        string Text { get; }

        InformationType Type { get; }

        Exception Exception { get; }

        ILog GetSublog(string name);

        void WriteLog(string text, InformationType info, Exception exception = null);

        void WriteAllToFile(string file, Encoding encoding, bool useOffset);
    }
}