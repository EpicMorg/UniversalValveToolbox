using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace UniversalValveToolbox.Utils {
    internal static class ShellUtils {
        /// <summary>
        /// Process.Start as it behaved on .NET Framework: through the shell (UseShellExecute = true),
        /// so URLs, steam:// links and documents open in their associated app. .NET Core+ defaults to false.
        /// </summary>
        public static Process Start(string fileName, string arguments = "") {
            return Process.Start(new ProcessStartInfo(fileName, arguments ?? string.Empty) { UseShellExecute = true });
        }

        /// <summary>
        /// Starts a program with extra environment variables (for this process only) and a working directory.
        /// Executables are started directly (the shell cannot pass environment variables); anything else
        /// (.bat, .lnk, documents) goes through the shell without them.
        /// </summary>
        public static Process Launch(string fileName, string arguments, IDictionary<string, string> environment, string workingDirectory) {
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            var direct = ext == ".exe" || ext == ".com";
            var psi = new ProcessStartInfo(fileName, arguments ?? string.Empty) {
                UseShellExecute = !direct,
                WorkingDirectory = Directory.Exists(workingDirectory) ? workingDirectory : Path.GetDirectoryName(fileName)
            };
            if (direct && environment != null) {
                foreach (var pair in environment)
                    psi.Environment[pair.Key] = pair.Value;
            }
            return Process.Start(psi);
        }
    }
}
