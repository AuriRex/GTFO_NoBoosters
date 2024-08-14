using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System.Reflection;

[assembly: AssemblyVersion(NoBoosters.EntryPoint.PLUGIN_VERSION)]
[assembly: AssemblyFileVersion(NoBoosters.EntryPoint.PLUGIN_VERSION)]
[assembly: AssemblyInformationalVersion(NoBoosters.EntryPoint.PLUGIN_VERSION)]

namespace NoBoosters
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    [BepInIncompatibility(DEVIOUSLICK_GUID)]
    public class EntryPoint : BasePlugin
    {
        public const string PLUGIN_GUID = "dev.aurirex.gtfo.noboosters";
        public const string PLUGIN_NAME = "No Boosters";
        public const string PLUGIN_VERSION = "1.1.0";

        public const string DEVIOUSLICK_GUID = "com.mccad00.AmongDrip";

        private Harmony _harmony;

        internal static ManualLogSource L;

        public override void Load()
        {
            L = Log;

            _harmony = new Harmony(PLUGIN_GUID);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());

            Log.LogInfo("Loaded and patched!");
        }
    }
}
