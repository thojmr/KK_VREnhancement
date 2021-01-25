using HarmonyLib;
using ADV;

namespace KK_VREnhancement
{    
    public static class VRControllerColliderHooks
    {
        internal static VRPlugin pluginInstance;
        internal static Harmony harmonyInstance;

        internal static void InitHooks(Harmony _harmonyInstance = null, VRPlugin _pluginInstance = null)
        {
            if (_pluginInstance != null) pluginInstance = _pluginInstance;
            if (_harmonyInstance != null) harmonyInstance = _harmonyInstance;

            if (harmonyInstance == null) return;
            harmonyInstance.PatchAll(typeof(VRControllerColliderHooks));
        }

        internal static void UnInitHooks(string harmonyGUID)
        {
            if (harmonyInstance == null) return;
            harmonyInstance.UnpatchAll(harmonyGUID);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(ADVScene), "Update")]
        internal static void ADVScene_Update(ADVScene __instance)
        {
            if (!VRPlugin.VREnabled || !VRPlugin.EnableControllerColliders.Value) return;   

            VRControllerColliderHelper.TriggerHelperCoroutine(pluginInstance);    
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(ActionScene), "NPCLoadAll", typeof(bool))]
        internal static void NPC_Create(bool isShuffle)
        {
            if (!VRPlugin.VREnabled || !VRPlugin.EnableControllerColliders.Value) return;
            if (VRPlugin.debugLog) VRPlugin.Logger.LogInfo($" NPCLoadAll ");
        
            VRControllerColliderHelper.TriggerHelperCoroutine(pluginInstance);
        }




        // 		private void Awake()
		// {
		// 	SceneManager.sceneLoaded += new UnityAction<Scene, LoadSceneMode>(this.SceneManager_sceneLoaded);
		// 	this.SceneManager_sceneLoaded(SceneManager.GetActiveScene(), 0);
		// }

		// // Token: 0x06000002 RID: 2 RVA: 0x00002074 File Offset: 0x00000274
		// private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode lsm)
		// {

    }
}