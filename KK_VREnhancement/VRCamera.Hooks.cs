using HarmonyLib;
using UnityEngine;
using ADV;
using System.Collections.Generic;

namespace KK_VREnhancement
{
    public static class VRCameraHooks
    {
        internal static void InitHooks(Harmony harmonyInstance)
        {
            harmonyInstance.PatchAll(typeof(VRCameraHooks));
        }

        //When the heroine changes position (ADVScene like Going to lunch, exercising, Date)
        [HarmonyPostfix]
        [HarmonyPatch(typeof(ADVScene), "Update")]
        internal static void ADVScene_Update(ADVScene __instance)
        {
            if (!VRPlugin.VREnabled || !VRPlugin.MoveWithTalkScene.Value) return;

            MainScenario scenario = Traverse.Create(__instance).Field("scenario").GetValue<MainScenario>();
            if (scenario == null || scenario.commandController == null) return;

            System.Collections.Generic.Dictionary<int, ADV.CharaData> characters = scenario.commandController.Characters;
            if (characters == null || characters.Count <= 0 || characters[0] == null) return;

            //Get the main heroine (is it always at index 0, probably not)?
            ChaControl charCtrl = characters[0].chaCtrl;
            if (charCtrl == null || charCtrl.objHead == null) return;

            //Gets heroines head position.  Will place the user facing this position
            Transform heroineTransform = charCtrl.objHead.transform;
            if (heroineTransform == null) return;

            VRCameraController.MoveToFaceHeroine_ADVScene(heroineTransform.position, heroineTransform.rotation);                
        }
        
        //When the ADV scene (TalkScene) is done clear the last ADVScene position
        [HarmonyPostfix]
        [HarmonyPatch(typeof(TalkScene), "OnDestroy")]
        internal static void TalkScene_OnDestroy(TalkScene __instance)
        {
            if (!VRPlugin.MoveWithTalkScene.Value) return;

            VRCameraController.ClearLastPosition();                
        }

        //When heroine changes to a new location (ActionScene, HScene)
        [HarmonyPostfix]
        [HarmonyPatch(typeof(HSceneProc), "SetLocalPosition", typeof(HSceneProc.AnimationListInfo))]
        internal static void HSceneProc_SetLocalPosition(HSceneProc __instance)
        {
            if (!VRPlugin.VREnabled || !VRPlugin.MoveWithTalkScene.Value) return;
            
            List<ChaControl> lstFemale = Traverse.Create(__instance).Field("lstFemale").GetValue<List<ChaControl>>();
            if (lstFemale == null || lstFemale[0] == null) return;

            ChaControl female = lstFemale[0];
            if (female == null || female.objHead == null) return;

            //Gets heroines head position.  Will place the user facing this position
            Transform femaleTransform = female.objHead.transform;
            if (femaleTransform == null) return;

            VRCameraController.MoveToFaceHeroine_HScene(femaleTransform.position, femaleTransform.rotation);
        }        

        //When the HScene is done clear the last position
        [HarmonyPostfix]
        [HarmonyPatch(typeof(HSceneProc), "OnDestroy")]
        internal static void HSceneProc_OnDestroy(HSceneProc __instance)
        {
            if (!VRPlugin.VREnabled || !VRPlugin.MoveWithTalkScene.Value) return;
            
            VRCameraController.ClearLastPosition();                
        }

    }
}