using System;
using System.Collections.Generic;
using LoggerProject.Resources;

namespace LoggerProject.Common
{
    public static class SystemInfo
    {
        public static string GetSysInfo()
        {
            string str = string.Empty;

            str += $@"Имя компьютера: {GetMachineName()}" + Environment.NewLine;
            str += $@"Количество потоков: {GetProcessorsCount()}" + Environment.NewLine;
            str += $@"С момента запуска системы прошло: {GetTickCount64()}" + Environment.NewLine;

            return str;
        }

        public static List<string> GetSysInfoList()
        {
            List<string> str = new List<string>();

            str.Add( $@"Имя компьютера: {GetMachineName()}");
            str.Add($@"Количество потоков: {GetProcessorsCount()}");
            str.Add($@"{GetTickCount64()}");
            
            return str;
        }

        public static int GetProcessorsCount()
        {
            return Environment.ProcessorCount;
        }

        public static int GetCoresCount()
        {
            return 0;
        }

        public static string GetMachineName()
        {
            return Environment.MachineName;
        }
        
        public static string GetTickCount64()
        {
            var span = TimeSpan.FromMilliseconds(Environment.TickCount64);
            
            var days = span.Days;
            string daysName;
            if (days == 1)
                daysName = "день";
            if (days > 1 && days < 5)
                daysName = "дня";
            else
                daysName = "дней";

            var hours = span.Hours;
            string hoursName;
            if (hours == 1)
                hoursName = "час";
            if (hours > 1 && hours < 5)
                hoursName = "часа";
            else
                hoursName = "часов";

            var minutes = span.Minutes;
            string minName;
            if (minutes == 1)
                minName = "минута";
            if (minutes > 1 && minutes < 5)
                minName = "минуты";
            else
                minName = "минут";

            var seconds = span.Seconds;
            string secName;
            if (seconds == 1)
                secName = "секунда";
            if (seconds > 1 && seconds < 5)
                secName = "секунды";
            else
                secName = "секунд";

            var mil = span.Milliseconds;

            var start = DateTime.Now
                .AddDays(-days)
                .AddHours(-hours)
                .AddMinutes(-minutes)
                .AddSeconds(-seconds)
                .AddMilliseconds(-mil);

            var st = span.ToString()
            
            var res = $@"Компьютер запущен: {start.ToString(CommonResources.DateFormat)}. Прошло: {days} {daysName} {hours} {hoursName} {minutes} {minName} {seconds} {secName}";
            
            return res;
        }
    }
}