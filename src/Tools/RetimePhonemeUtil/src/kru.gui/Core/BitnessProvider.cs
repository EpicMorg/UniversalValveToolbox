namespace kru.gui.Core;
using System;

internal static class BitnessProvider
{
    public static string BitnessName => Environment.Is64BitProcess ? "x64" : "x86";
}
