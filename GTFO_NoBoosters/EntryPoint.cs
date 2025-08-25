using System;
using System.Linq;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System.Reflection;

[assembly: AssemblyVersion(NoBoosters.EntryPoint.PLUGIN_VERSION)]
[assembly: AssemblyFileVersion(NoBoosters.EntryPoint.PLUGIN_VERSION)]
[assembly: AssemblyInformationalVersion(NoBoosters.EntryPoint.PLUGIN_VERSION)]

namespace NoBoosters;

[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
[BepInIncompatibility(DEVIOUSLICK_GUID)]
[BepInDependency(SIMPLEPROGRESSION_GUID, BepInDependency.DependencyFlags.SoftDependency)]
public class EntryPoint : BasePlugin
{
    public const string PLUGIN_GUID = "dev.aurirex.gtfo.noboosters";
    public const string PLUGIN_NAME = "No Boosters";
    public const string PLUGIN_VERSION = "1.1.0";

    public const string DEVIOUSLICK_GUID = "com.mccad00.AmongDrip";
    public const string SIMPLEPROGRESSION_GUID = "dev.aurirex.gtfo.simpleprogression";

    private Harmony _harmony;
    
    private bool simpleProgressionLoaded;

    internal static ManualLogSource L;

    public override void Load()
    {
        L = Log;

        simpleProgressionLoaded = IL2CPPChainloader.Instance.Plugins.Any(kvp => string.Equals(kvp.Key, SIMPLEPROGRESSION_GUID, StringComparison.InvariantCultureIgnoreCase));

        _harmony = new Harmony(PLUGIN_GUID);
        _harmony.PatchAll(typeof(Patches));

        if (!simpleProgressionLoaded)
        {
            Log.LogWarning("Simple Progression is not loaded, patching like usual.");
            _harmony.PatchAll(typeof(PersistentInventoryManager_Patches));
        }
            
        Log.LogInfo("Loaded and patched!");
    }
}