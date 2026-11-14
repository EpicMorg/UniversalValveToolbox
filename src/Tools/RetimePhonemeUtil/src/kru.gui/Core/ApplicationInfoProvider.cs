namespace kru.gui.Core;
using System.Reflection;

internal static class ApplicationInfoProvider
{
    public static ApplicationInfo GetInfo()
    {
        var assembly = Assembly.GetEntryAssembly()!;

        var version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()!.InformationalVersion;
        var name = assembly.GetCustomAttribute<AssemblyProductAttribute>()!.Product;
        var copyright = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()!.Copyright;

        return new ApplicationInfo(version, name, copyright);
    }
}
