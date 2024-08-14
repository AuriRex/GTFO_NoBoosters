using CellMenu;
using HarmonyLib;

namespace NoBoosters
{
    internal class Patches
    {
        [HarmonyPatch(typeof(GameStateManager), nameof(GameStateManager.DoChangeState))]
        public static class GameStateManager_DoChangeState_Patch
        {
            public static void Postfix(eGameStateName nextState)
            {
                if(nextState == eGameStateName.Generating)
                {
                    EntryPoint.L.LogInfo($"Disabling {nameof(PersistentInventoryManager)}");
                    PersistentInventoryManager.Current.enabled = false;
                }
            }
        }

        [HarmonyPatch(typeof(PersistentInventoryManager), nameof(PersistentInventoryManager.OnLevelCleanup))]
        public static class PersistentInventoryManager_OnLevelCleanup_Patch
        {
            public static void Prefix(PersistentInventoryManager __instance)
            {
                EntryPoint.L.LogInfo($"Enabling {nameof(PersistentInventoryManager)}");
                __instance.enabled = true;
            }
        }

        [HarmonyPriority(Priority.Last)]
        [HarmonyPatch(typeof(PersistentInventoryManager), nameof(PersistentInventoryManager.CommitPendingTransactions))]
        public static class PersistentInventoryManager_CommitPendingTransactions_Patch
        {
            public static bool Prefix()
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(CM_PlayerLobbyBar), nameof(CM_PlayerLobbyBar.SetupFromPage))]
        public static class CM_PlayerLobbyBar_SetupFromPage_Patch
        {
            public static void Postfix(CM_PlayerLobbyBar __instance)
            {
                __instance.m_boosterImplantAlign.position = new UnityEngine.Vector3(10000, 0, 0);

                __instance.m_clothesButton.transform.localPosition = new UnityEngine.Vector3(170, -510, 0);
            }
        }

        [HarmonyPatch(typeof(PUI_BoosterIconActiveDisplay), nameof(PUI_BoosterIconActiveDisplay.UpdateBoosterIconsActiveState))]
        public static class PUI_BoosterIconActiveDisplay_UpdateBoosterIconsActiveState_Patch
        {
            public static bool Prefix(PUI_BoosterIconActiveDisplay __instance)
            {
                __instance.gameObject.SetActive(false);
                return false;
            }
        }

        [HarmonyPatch(typeof(PUI_BoosterDetails), nameof(PUI_BoosterDetails.SetupBoosterDetails))]
        public static class PUI_BoosterDetails_SetupBoosterDetails_Patch
        {
            public static void Postfix(PUI_BoosterDetails __instance)
            {
                __instance.gameObject.SetActive(false);
            }
        }

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
}
