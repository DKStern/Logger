using LoggerProject.Models;
using System;
using System.Text;

namespace LogTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger(@"C:/temp.txt", false, Encoding.UTF8);

            logger.Debug("Debug");
            logger.Information("Information");
            logger.Warning("Warning");
            logger.Error("Error", new Exception("Тестовая ошибка!"));
            logger.Critical("Critical", new Exception("Типа критическая ошибка!"));

        }
    }
}
