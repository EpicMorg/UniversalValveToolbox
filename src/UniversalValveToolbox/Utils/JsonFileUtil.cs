namespace UniversalValveToolbox.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;

    internal static class JsonFileUtil
    {
        public static T ReadValue<T>(string path)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                return result;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static T ReadValue<T>(string path, T fileDefaultValue)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                return result;
            }
            catch (Exception)
            {
                WriteValue(path, fileDefaultValue);

                return fileDefaultValue;
            }
        }

        public static T[] ReadValues<T>(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            return Directory.GetFiles(directoryPath, "*").Select(path => ReadValue<T>(path)).Where(value => value != null).ToArray();
        }

        public static List<T> ReadListValues<T>(string directoryPath) => new List<T>(ReadValues<T>(directoryPath));

        public static void WriteValue<T>(string path, T value) => File.WriteAllText(path, JsonConvert.SerializeObject(value, Formatting.Indented));

        public static void SaveValues<T>(string folderPath, string fileExtension, List<T> values)
        {
            var di = new DirectoryInfo(folderPath);

            foreach (var file in di.GetFiles())
            {
                file.Delete();
            }

            foreach (var dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

            foreach (var item in values)
            {
                var fileName = new StringBuilder(item.ToString().ToLower());
                foreach (var c in Path.GetInvalidFileNameChars())
                {
                    fileName = fileName.Replace(c, '_');
                }

                var path = Path.Combine(folderPath, $"{fileName}.{fileExtension}");

                File.WriteAllText(path, JsonConvert.SerializeObject(item, Formatting.Indented));
            }
        }
    }
}
