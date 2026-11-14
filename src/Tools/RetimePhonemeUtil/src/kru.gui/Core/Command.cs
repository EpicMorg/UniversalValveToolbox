namespace kru.gui.Core;

using System;
using System.Diagnostics;

internal record Command(string InputFile, string OutputDirectory, string? CleanDirectory, bool SaveLog)
{
    private string ExecutablePath =>
        Path.Combine(
            Path.GetDirectoryName(AppContext.BaseDirectory)!,
            $@"Tools\{BitnessProvider.BitnessName}\retime_phoneme.exe");

    public string GetBatchLine() => string.Join(" ", new[] { $"\"{this.ExecutablePath}\"" }.Concat(this.GetArguments().Select(arg => $"\"{arg}\"")));

    public Process GetProcess()
    {
        var proc = new Process()
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = this.ExecutablePath,
            },
            EnableRaisingEvents = true,
        };
        foreach (var item in this.GetArguments())
        {
            proc.StartInfo.ArgumentList.Add(item);
        }

        return proc;
    }

    private IEnumerable<string> GetArguments()
    {
        var filename = Path.GetFileName(this.InputFile);
        yield return "-s";
        yield return this.InputFile;
        yield return "-d";
        yield return Path.Combine(this.OutputDirectory, filename);
        if (this.CleanDirectory is not null)
        {
            yield return "-oc";
            yield return Path.Combine(this.CleanDirectory, filename);
        }

        if (this.SaveLog)
        {
            yield return "-l";
        }
    }
}
