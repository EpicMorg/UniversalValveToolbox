using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UniversalValveToolbox.Utils {
    static class JsonFileUtil {
        public static T ReadValue<T>(string path) {
            try {
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            } catch (Exception) {
                return default(T);
            }
        }

        /// <summary>
        /// Reads a single JSON file. A missing file is created with <paramref name="fileDefaultValue"/>;
        /// a file that exists but cannot be parsed is left untouched (the user's config is not overwritten),
        /// <paramref name="error"/> says why and the default is returned.
        /// </summary>
        public static T ReadValue<T>(string path, T fileDefaultValue, out string error) {
            error = null;
            if (!File.Exists(path)) {
                WriteValue(path, fileDefaultValue);
                return fileDefaultValue;
            }
            try {
                var result = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                return result == null ? fileDefaultValue : result;
            } catch (Exception ex) {
                error = ex.Message;
                return fileDefaultValue;
            }
        }

        /// <summary>All *.json files directly in the folder (subfolders such as addons\template are not read).</summary>
        public static T[] ReadValues<T>(string directoryPath) {
            if (!Directory.Exists(directoryPath)) {
                Directory.CreateDirectory(directoryPath);
            }

            return Directory.GetFiles(directoryPath, "*.json").Select(path => ReadValue<T>(path)).Where(value => value != null).ToArray();
        }

        public static List<T> ReadListValues<T>(string directoryPath) => new List<T>(ReadValues<T>(directoryPath));

        public static void WriteValue<T>(string path, T value) => File.WriteAllText(path, JsonConvert.SerializeObject(value, Formatting.Indented));

        /// <summary>
        /// Replaces the *.json files directly in <paramref name="folderPath"/> with one file per value (named after
        /// ToString()). Other files and subfolders (e.g. addons\template) are kept. Equal names get " (2)", " (3)", ...
        /// so two items with the same name no longer overwrite each other.
        /// </summary>
        public static void SaveValues<T>(string folderPath, string fileExtension, List<T> values) {
            Directory.CreateDirectory(folderPath);

            // serialize everything first, so a failure does not leave the folder half-deleted
            var files = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var item in values) {
                var name = new StringBuilder((item.ToString() ?? string.Empty).ToLower());
                foreach (char c in Path.GetInvalidFileNameChars()) {
                    name = name.Replace(c, '_');
                }
                var baseName = name.Length == 0 ? "item" : name.ToString();
                var fileName = $"{baseName}.{fileExtension}";
                for (var n = 2; files.ContainsKey(fileName); n++) {
                    fileName = $"{baseName} ({n}).{fileExtension}";
                }
                files[fileName] = JsonConvert.SerializeObject(item, Formatting.Indented);
            }

            foreach (var old in Directory.GetFiles(folderPath, $"*.{fileExtension}")) {
                if (!files.ContainsKey(Path.GetFileName(old))) {
                    File.Delete(old);
                }
            }
            foreach (var file in files) {
                File.WriteAllText(Path.Combine(folderPath, file.Key), file.Value);
            }
        }
    }
}
