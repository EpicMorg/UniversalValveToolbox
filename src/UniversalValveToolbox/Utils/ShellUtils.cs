using System.Diagnostics;

namespace UniversalValveToolbox.Utils {
    /// <summary>
    /// Process.Start as it behaved on .NET Framework: through the shell (UseShellExecute = true),
    /// so URLs, steam:// links and documents open in their associated app. .NET Core+ defaults to false.
    /// </summary>
    internal static class ShellUtils {
        public static Process Start(string fileName, string arguments = "") {
            return Process.Start(new ProcessStartInfo(fileName, arguments ?? string.Empty) { UseShellExecute = true });
        }
    }
}
