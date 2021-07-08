using System;
using System.Text;

namespace LoggerProject.Interfaces
{
    /// <summary>
    /// Интерфейс логгера, который сразу пишет иформацию в файл
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Путь до файла с логом
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Дописывать ли файл или перезаписывать
        /// </summary>
        bool Append { get; }

        /// <summary>
        /// Кодировка файла
        /// </summary>
        Encoding Encoding { get; }

        /// <summary>
        /// Запись системной информации
        /// </summary>
        /// <param name="text">Текст сообщения</param>
        void Debug(string text);

        /// <summary>
        /// Запись информациии
        /// </summary>
        /// <param name="text">Текст сообщения</param>
        void Information(string text);

        /// <summary>
        /// Запись предупреждения
        /// </summary>
        /// <param name="text">Текст сообщения</param>
        void Warning(string text);

        /// <summary>
        /// Запись Ошибки
        /// </summary>
        /// <param name="text">Текст сообщения</param>
        /// <param name="exception">Исключение</param>
        void Error(string text, Exception exception);

        /// <summary>
        /// Запись критической ошибки
        /// </summary>
        /// <param name="text">Текст сообщения</param>
        /// <param name="exception">Исключение</param>
        void Critical(string text, Exception exception);
    }
}