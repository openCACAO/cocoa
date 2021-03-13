using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Xamarin.ExposureNotifications
{
    /// <summary>
    /// System.Diagnostics.Trace の拡張メソッド
    /// </summary>
    public static class LogEx
    {
        private enum LogLevel
        {
            Verbose,
            Debug,
            Info,
            Warning,
            Error
        }

        public static void StartMethod(
            [CallerMemberName] string method = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Output("Start", method, filePath, lineNumber, LogLevel.Info);
        }

        public static void EndMethod(
            [CallerMemberName] string method = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Output("End", method, filePath, lineNumber, LogLevel.Info);
        }

        public static void Verbose(
            string message,
            [CallerMemberName] string method = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Output(message, method, filePath, lineNumber, LogLevel.Verbose);
        }

        public static void Debug(
            string message,
            [CallerMemberName] string method = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Output(message, method, filePath, lineNumber, LogLevel.Debug);
        }

        public static void Info(
            string message,
            [CallerMemberName] string method = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Output(message, method, filePath, lineNumber, LogLevel.Info);
        }

        public static void Warning(
            string message,
            [CallerMemberName] string method = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Output(message, method, filePath, lineNumber, LogLevel.Warning);
        }

        public static void Error(
            string message,
            [CallerMemberName] string method = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Output(message, method, filePath, lineNumber, LogLevel.Error);
        }

        public static void Exception(
            string message,
            Exception ex,
            [CallerMemberName] string method = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            Output(message + ", Exception: " + ex.ToString(), method, filePath, lineNumber, LogLevel.Error);
        }


        private static DateTime JstNow()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, JstTimeZoneInfo());
        }
        private static TimeZoneInfo JstTimeZoneInfo()
        {
            // iOS/Android/Unix
            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Asia/Tokyo");
            }
            catch (TimeZoneNotFoundException)
            {
                // Not iOS/Android/Unix
            }

            // Windows
            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
            }
            catch (TimeZoneNotFoundException)
            {
                // Not Windows
            }

            // Emergency fallback
            return TimeZoneInfo.CreateCustomTimeZone("JST", new TimeSpan(9, 0, 0), "(GMT+09:00) JST", "JST");
        }

        private static void Output(string message, string method, string filePath, int lineNumber, LogLevel logLevel)
        {
            var now = JstNow().ToString("yyyy/MM/dd HH:mm:ss");
            var level = logLevel.ToString();
            var line = $"\"{now}\",\"{level}\",\"{message}\",\"{method}\",\"{filePath}\",\"{lineNumber}\",\"Android\",\"\",\"\",\"\",\"\",\"\"";
            System.Diagnostics.Trace.WriteLine(line);
            return;
        }
    }
}
