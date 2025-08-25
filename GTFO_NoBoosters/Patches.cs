using CellMenu;
using HarmonyLib;

namespace NoBoosters;

internal class Patches
{
    [HarmonyPriority(Priority.Last)]
    [HarmonyPatch(typeof(CM_PlayerLobbyBar), nameof(CM_PlayerLobbyBar.SetupFromPage))]
    public static class CM_PlayerLobbyBar_SetupFromPage_Patch
    {
        public static void Postfix(CM_PlayerLobbyBar __instance)
        {
            __instance.m_boosterImplantAlign.position = new UnityEngine.Vector3(10000, 0, 0);

            __instance.m_clothesButton.transform.localPosition = new UnityEngine.Vector3(170, -510, 0);
        }
    }

    [HarmonyPriority(Priority.Last)]
    [HarmonyPatch(typeof(PUI_BoosterIconActiveDisplay), nameof(PUI_BoosterIconActiveDisplay.UpdateBoosterIconsActiveState))]
    public static class PUI_BoosterIconActiveDisplay_UpdateBoosterIconsActiveState_Patch
    {
        public static bool Prefix(PUI_BoosterIconActiveDisplay __instance)
        {
            __instance.gameObject.SetActive(false);
            return false;
        }
    }

    [HarmonyPriority(Priority.Last)]
    [HarmonyPatch(typeof(PUI_BoosterDetails), nameof(PUI_BoosterDetails.SetupBoosterDetails))]
    public static class PUI_BoosterDetails_SetupBoosterDetails_Patch
    {
        public static void Postfix(PUI_BoosterDetails __instance)
        {
            __instance.gameObject.SetActive(false);
        }
    }

    [HarmonyPriority(Priority.Last)]
    [HarmonyPatch(typeof(PUI_BoosterDetails), nameof(PUI_BoosterDetails.UpdateButtonActiveCheck))]
    public static class PUI_BoosterDetails_UpdateButtonActiveCheck_Patch
    {
        // Runs every ~2 seconds
        public static bool Prefix(PUI_BoosterDetails __instance)
        {
            __instance.m_leftButton.gameObject.SetActive(false);
            __instance.m_rightButton.gameObject.SetActive(false);
            return false;
        }
    }
}