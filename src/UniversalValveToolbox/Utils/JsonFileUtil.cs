using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalValveToolbox.Utils {
    static class JsonFileUtil {
        public static T ReadValue<T>(string path) => JsonConvert.DeserializeObject<T>(File.ReadAllText(path));

        public static T[] ReadValues<T>(string directoryPath) {
            if (!Directory.Exists(directoryPath)) {
                Directory.CreateDirectory(directoryPath);
            }

            return Directory.GetFiles(directoryPath, "*").Select(path => ReadValue<T>(path)).ToArray();
        }

        public static List<T> ReadListValues<T>(string directoryPath) => new List<T>(ReadValues<T>(directoryPath));

        public static void WriteValue<T>(string path, T value) => File.WriteAllText(path, JsonConvert.SerializeObject(value, Formatting.Indented));

        public static void SaveValues<T>(string folderPath, string fileExtension, List<T> values) {
            DirectoryInfo di = new DirectoryInfo(folderPath);

            foreach (FileInfo file in di.GetFiles()) {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories()) {
                dir.Delete(true);
            }

            foreach (var item in values) {
                StringBuilder fileName = new StringBuilder(item.ToString().ToLower());
                foreach (char c in System.IO.Path.GetInvalidFileNameChars()) {
                    fileName = fileName.Replace(c, '_');
                }

                var path = Path.Combine(folderPath, $"{fileName}.{fileExtension}");

                File.WriteAllText(path, JsonConvert.SerializeObject(item, Formatting.Indented));
            }
        }
    }
}
