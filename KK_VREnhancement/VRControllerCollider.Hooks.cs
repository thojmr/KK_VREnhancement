using HarmonyLib;
using ADV;

namespace KK_VREnhancement
{    
    public static class VRControllerColliderHooks
    {
        internal static VRPlugin _pluginInstance;

        internal static void InitHooks(Harmony harmonyInstance, VRPlugin instance)
        {
            _pluginInstance = instance;
            harmonyInstance.PatchAll(typeof(VRControllerColliderHooks));
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(ADVScene), "Update")]
        internal static void ADVScene_Update(ADVScene __instance)
        {
            if (!VRPlugin.VREnabled || !VRPlugin.EnableControllerColliders.Value) return;   

            VRControllerColliderHelper.TriggerHelperCoroutine(_pluginInstance);    
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(ActionScene), "NPCLoadAll", typeof(bool))]
        internal static void NPC_Create(bool isShuffle)
        {
            if (!VRPlugin.VREnabled || !VRPlugin.EnableControllerColliders.Value) return;
        
            VRControllerColliderHelper.TriggerHelperCoroutine(_pluginInstance);
        }

    }
}