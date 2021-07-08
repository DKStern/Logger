using LoggerProject.Common;
using LoggerProject.Enums;
using LoggerProject.Interfaces;
using LoggerProject.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LoggerProject.Models
{
    public class Log : ILog
    {
        public Log(string text)
        {
            Text = text;
            Sublogs = new List<ILog>();
            LogLevel = 0;
            LogType = LogType.Log;
            Date = DateTime.Now;
            Type = InformationType.INFORMATION;
        }

        public Log(string text, uint level, LogType type, ILog owner, InformationType info, Exception exception)
        {
            Text = text;
            LogLevel = level;
            LogType = type;
            Owner = owner;
            Date = DateTime.Now;
            Type = info;
            Exception = exception;

            if (type == LogType.Log)
                Sublogs = new List<ILog>();
        }

        public ILog Owner { get; }

        public List<ILog> Sublogs { get; }

        public DateTime Date { get; }

        public uint LogLevel { get; }

        public LogType LogType { get; }

        public string Text { get; }

        public Exception Exception { get; }

        public InformationType Type { get; }

        public ILog GetSublog(string name)
        {
            if (LogType == LogType.Record)
            {
                throw new Exception("Попытка создать sublog для записи, а не лога!");
            }

            var sublog = new Log(name, LogLevel + 1, LogType.Log, this, InformationType.INFORMATION, null);
            Sublogs.Add(sublog);
            
            return sublog;
        }

        public void WriteLog(string text, InformationType info, Exception exception = null)
        {
            if (LogType == LogType.Record)
                return;

            var sublog = new Log(text, LogLevel + 1, LogType.Record, this, info, exception);
            Sublogs.Add(sublog);
        }
        
        private void WriteLogToFile(ILog log, StreamWriter sw, bool useOffset)
        {
            string offset = string.Empty;

            if (useOffset) 
                offset = CommonMethods.GetOffset(log.LogLevel);


            var info = CommonMethods.GetPadRightInfo(log.Type);
            var sys = $@"{offset}[{log.Date.ToString(CommonResources.DateFormat)}]{info}";
            var str = $@"{sys} - {log.Text}";
            sw.WriteLine(str);

            if (log.Exception != null)
            {
                str = $@"{sys} - Текст ошибки: {log.Exception.Message}";
                sw.WriteLine(str);

                if (!string.IsNullOrEmpty(log.Exception.StackTrace))
                {
                    str = $"{sys} - Стек вызовов:" + Environment.NewLine + $@"{offset}{log.Exception.StackTrace}";
                    sw.WriteLine(str);
                }
            }

            sw.Flush();

            if (log.Sublogs == null || !log.Sublogs.Any())
                return;

            log.Sublogs.ForEach(x => WriteLogToFile(x, sw, useOffset));
        }

        public void WriteAllToFile(string file, Encoding encoding, bool useOffset = false)
        {
            var correctFile = CommonMethods.CheckFormat(file, LogFileFormats.log);

            using (var sw = new StreamWriter(correctFile,false, encoding))
            {
                WriteLogToFile(this, sw, useOffset);
            }
        }
    }
}