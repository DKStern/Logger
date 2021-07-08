using LoggerProject.Common;
using LoggerProject.Enums;
using LoggerProject.Models;
using System;
using System.Text;

namespace LogTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TestSysInfo();


        }

        private static void TestLogs()
        {
            Logger logger = new Logger(@"C:/temp.exe", Encoding.UTF8);

            logger.Debug("Debug");
            logger.Information("Information");
            logger.Warning("Warning");
            logger.Error("Error", new Exception("Тестовая ошибка!"));
            logger.Critical("Critical", new Exception("Типа критическая ошибка!"));


            Log log = new Log("Начало работы программы");
            log.WriteLog("Test", InformationType.INFORMATION);

            var sublog = log.GetSublog("Тест ошибки");
            sublog.WriteLog("Предупреждение!", InformationType.WARNING);
            sublog.WriteLog("Ошибка!", InformationType.ERROR, new Exception("Ошибка"));
            var subsublog = sublog.GetSublog("3 Уровень");
            subsublog.WriteLog("Критическая ошибка", InformationType.CRITICAL, new Exception("CRITICAL"));
            log.WriteLog("info", InformationType.INFORMATION);
            log.WriteLog("Test", InformationType.DEBUG);

            log.WriteAllToFile(@"C:/temp2.exe", Encoding.UTF8, true);
        }

        private static void TestSysInfo()
        {
            Logger logger = new Logger(@"C:/sys.sys", Encoding.UTF8);
            
            logger.Debug(SystemInfo.GetTickCount64());
        }
    }
}
