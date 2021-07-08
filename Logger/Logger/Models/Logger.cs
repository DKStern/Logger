using LoggerProject.Common;
using LoggerProject.Enums;
using LoggerProject.Interfaces;
using System;
using System.IO;
using System.Text;
using LoggerProject.Resources;

namespace LoggerProject.Models
{
    public class Logger : ILogger
    {
        private StreamWriter sw;

        /// <summary>
        /// Конструктор логгера
        /// </summary>
        /// <param name="path">Путь с названием файла, в котором нужно сохранить лог</param>
        /// <param name="append">Дописывать в файл или перезаписывать</param>
        /// <param name="encoding">Кодировка файла</param>
        public Logger(string path, Encoding encoding, bool append = false)
        {
            Path = CommonMethods.CheckFormat(path, LogFileFormats.log);
            Append = append;
            Encoding = encoding;

            sw = new StreamWriter(Path, Append, Encoding)
            {
                AutoFlush = false
            };
        }

        /// <summary>
        /// Деструктор
        /// </summary>
        ~Logger()
        {
            sw.Flush();
            sw.Dispose();
        }
        
        public string Path { get; }

        public bool Append { get; }

        public Encoding Encoding { get; }

        private string GetCurDateTime()
        {
            return DateTime.Now.ToString(CommonResources.DateFormat);
        }

        private void WriteLog(string text, InformationType type, Exception exception = null)
        {
            var info = CommonMethods.GetPadRightInfo(type);
            var sys = $@"[{GetCurDateTime()}]{info}";
            var str = $@"{sys} - {text}";
            sw.WriteLine(str);

            if (exception != null)
            {
                str = $@"{sys} - Текст ошибки: {exception.Message}";
                sw.WriteLine(str);

                if (!string.IsNullOrEmpty(exception.StackTrace))
                {
                    str = $"{sys} - Стек вызовов:" + Environment.NewLine + $@"{exception.StackTrace}";
                    sw.WriteLine(str);
                }
            }

            sw.Flush();
        }

        public void Debug(string text)
        {
            WriteLog(text, InformationType.DEBUG);
        }

        public void Information(string text)
        {
            WriteLog(text, InformationType.INFORMATION);
        }

        public void Warning(string text)
        {
            WriteLog(text, InformationType.WARNING);
        }

        public void Error(string text, Exception exception)
        {
            WriteLog(text, InformationType.ERROR, exception);
        }

        public void Critical(string text, Exception exception)
        {
            WriteLog(text, InformationType.CRITICAL, exception);
        }
    }
}