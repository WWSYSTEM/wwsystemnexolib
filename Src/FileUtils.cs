using System;
using System.Xml;
using Microsoft.Win32;

namespace WWsystemLib
{
    using System.IO;
    using System.Xml;

    namespace WWsystemLib
    {
        public class FileUtils
        {
            public static void SaveFileOrRegistry(string filePath, string registryKey, string content)
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using (var key = Registry.CurrentUser.CreateSubKey(registryKey))
                {
                    key.SetValue(registryKey, content);
                }
            }

            public static string LoadFromRegistry( string registryKey)
            {
                using (var key = Registry.CurrentUser.OpenSubKey(registryKey))
                {
                    if (key == null)
                    {
                        return string.Empty;
                    }
                    var value = key.GetValue(registryKey);
                    if (value == null)
                    {
                        return string.Empty;
                    }
                   return value as string;
                }
            }

            public static void EnsurePath(string path)
            {
                string directoryPath = Path.GetDirectoryName(path);

                // If the directory path is not empty and the directory doesn't exist, create it
                if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // If the specified path is a file, create an empty file
                if (!Directory.Exists(path) && !File.Exists(path))
                {
                    File.Create(path).Close();
                }
            }

            public static void Save(string path, string content)
            {
                // Save content to file
                EnsurePath(path);
                File.WriteAllText(path, content);
            }

            public static void Save(string path, string name, string content)
            {
                Save($"{path}\\{name}", content);
            }

            public static string Load(string path)
            {
                if (File.Exists(path))
                {
                    return File.ReadAllText(path);
                }
                else
                {
                    return string.Empty;
                }
            }

            public static StreamWriter GetFile(string path)
            {
                EnsurePath(path);
                return File.AppendText(path);
            }

            public static string Load(string path, string name)
            {
                return Load($"{path}\\{name}");
            }

            public static string FindByExtension(string path, string extension, bool deep = true)
            {
                EnsurePath(path);
                try
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(path);
                    FileInfo[] files = directoryInfo.GetFiles("*" + extension);
                    if (files.Length > 0)
                    {
                        return File.ReadAllText(files[0].FullName);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("File by extension not found!", ex);
                }

                return string.Empty;
            }

            public static XmlDocument FindXml(string path, string extension = ".xml")
            {
                var content = FindByExtension(path, extension);
                var xml = new XmlDocument();
                xml.Load(content);
                return xml;
            }
        }
    }
}