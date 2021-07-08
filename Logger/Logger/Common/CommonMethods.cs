﻿using LoggerProject.Enums;
using LoggerProject.Resources;
using System.Text.RegularExpressions;

namespace LoggerProject.Common
{
    public static class CommonMethods
    {
        public static string CheckFormat(string path, LogFileFormats format)
        {
            string extension;

            switch (format)
            {
                case LogFileFormats.log:
                    extension = @"\.log";
                    break;
                case LogFileFormats.txt:
                    extension = @"\.txt";
                    break;
                default:
                    extension = @"\.log";
                    break;
            }

            return Regex.Replace(path, @"\.\w+", extension);
        }

        public static string GetOffset(uint count)
        {
            string res = string.Empty;

            for (int i = 0; i < count; i++)
            {
                res += CommonResources.OffsetChar;
            }

            return res;
        }
    }
}