using System;
using System.IO;
using WWsystemLib.WWsystemLib;

namespace WWsystemLib
{
    public class Logger
    {
        private static readonly LoggerSettings _settings = new LoggerSettings()
        {
            Path = "C:\\wwsystem\\logs\\program",
            Prefix = "Program",
            ToConsole = true,
            ToFile = true
        };

        public static string Path()
        {
            if (!_settings.Path.EndsWith("\\"))
            {
                _settings.Path += "\\";
            }

            return _settings.Path + "logs.txt";
        }


        public static void SetPrefix(string prefix = "")
        {
            _settings.Prefix = prefix;
        }

        public static void ResetPrefix()
        {
            _settings.Prefix = _settings.DefaultPrefix;
        }

        public static void Init(Action<LoggerSettings> action)
        {
            action.Invoke(_settings);
            if (string.IsNullOrEmpty(_settings.DefaultPrefix))
            {
                _settings.DefaultPrefix = _settings.Prefix;
            }
        }

        public static string Error(string message, Exception exception = null)
        {
            if (exception == null)
            {
                return Format("Error", message);
            }

            return Format("Error", $"{message} \n {exception.Message} \n {exception.StackTrace}");
        }

        public static string Info(string message)
        {
            return Format("Info", message);
        }

        public static string Warning(string message)
        {
            return Format("Warning", message);
        }


        private static string Format(string level, string message)
        {
            var fullPath = Path();
            var file = FileUtils.GetFile(fullPath);
            var time = DateTime.Now.ToString();
            var msg = $"{time} [{level}] [{_settings.Prefix}] {message}";
            file.WriteLine(msg);
            file.Close();
            if (_settings.ToConsole)
            {
                Console.WriteLine(msg);
            }

            return message;
        }

        public static void Reset()
        {
            var sourceFilePath = Path();
            string destinationFilePath = Path().Replace("logs.txt", $"{DateTime.Now.ToString("M_d_yy")}_logs.txt");
            try
            {
                File.Copy(sourceFilePath, destinationFilePath, true);
                File.WriteAllText(sourceFilePath, string.Empty);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}