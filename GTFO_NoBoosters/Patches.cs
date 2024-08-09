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

    }
}
