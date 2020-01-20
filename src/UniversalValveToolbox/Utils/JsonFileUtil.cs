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
        public static void WriteValue<T>(string path, T value) => File.WriteAllText(path, JsonConvert.SerializeObject(value, Formatting.Indented));
    }
}
