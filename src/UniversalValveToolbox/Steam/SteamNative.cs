using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace UniversalValveToolbox.Steam {
    /// <summary>
    /// P/Invoke declarations for the flat C API of steam_api.dll / steam_api64.dll (Steamworks SDK 1.65).
    /// Only what the launcher uses. Interface accessor suffixes (_v009 etc.) must match the shipped dll.
    /// </summary>
    internal static class SteamNative {
        private const string Lib = "steam_api";

        // ESteamAPIInitResult
        public const int InitOk = 0;
        public const int InitFailedGeneric = 1;
        public const int InitNoSteamClient = 2;
        public const int InitVersionMismatch = 3;

        // SteamErrMsg is char[1024]
        public const int ErrMsgSize = 1024;

        static SteamNative() {
            // one name in the declarations, the right file for the process bitness
            NativeLibrary.SetDllImportResolver(typeof(SteamNative).Assembly, Resolve);
        }

        private static IntPtr Resolve(string name, Assembly assembly, DllImportSearchPath? searchPath) {
            if (name != Lib)
                return IntPtr.Zero;

            var file = Environment.Is64BitProcess ? "steam_api64.dll" : "steam_api.dll";
            var path = Path.Combine(AppContext.BaseDirectory, file);
            return NativeLibrary.TryLoad(path, out var handle) ? handle : IntPtr.Zero;
        }

        /// <summary>Forces the static constructor (resolver registration) before the first native call.</summary>
        public static void EnsureResolver() { }

        [DllImport(Lib, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SteamAPI_InitFlat(byte[] errMsg);

        [DllImport(Lib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SteamAPI_Shutdown();

        [DllImport(Lib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool SteamAPI_IsSteamRunning();

        [DllImport(Lib, CallingConvention = CallingConvention.Cdecl)]
        public static extern void SteamAPI_RunCallbacks();

        // ISteamApps
        [DllImport(Lib, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SteamAPI_SteamApps_v009();

        [DllImport(Lib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool SteamAPI_ISteamApps_BIsAppInstalled(IntPtr self, uint appId);

        [DllImport(Lib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool SteamAPI_ISteamApps_BIsSubscribedApp(IntPtr self, uint appId);

        [DllImport(Lib, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SteamAPI_ISteamApps_GetAppInstallDir(IntPtr self, uint appId, byte[] folder, uint folderSize);

        // ISteamUser
        [DllImport(Lib, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SteamAPI_SteamUser_v023();

        [DllImport(Lib, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool SteamAPI_ISteamUser_BLoggedOn(IntPtr self);

        [DllImport(Lib, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong SteamAPI_ISteamUser_GetSteamID(IntPtr self);

        // ISteamFriends
        [DllImport(Lib, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SteamAPI_SteamFriends_v018();

        [DllImport(Lib, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SteamAPI_ISteamFriends_GetPersonaName(IntPtr self);

        // ISteamUtils
        [DllImport(Lib, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SteamAPI_SteamUtils_v011();

        [DllImport(Lib, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint SteamAPI_ISteamUtils_GetAppID(IntPtr self);
    }
}
