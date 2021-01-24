using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using VRGIN.Helpers;
using KKAPI.MainGame;

namespace KK_VREnhancement 
{
    [BepInPlugin(GUID, GUID, Version)]
    public class VRPlugin : BaseUnityPlugin 
    {
        public const string GUID = "KK_VREnhancement";
        public const string Version = "0.1";
        public static ConfigEntry<bool> MoveWithTalkScene { get; private set; }
        public static ConfigEntry<bool> EnableControllerColliders { get; private set; }

        internal static new ManualLogSource Logger { get; private set; }
        internal static bool VREnabled = false;

#if DEBUG
        internal static bool debugLog = true;
#else
        internal static bool debugLog = false;
#endif

        internal void Start() 
        {
            Logger = base.Logger;

            MoveWithTalkScene = Config.Bind<bool>("", "Enable move with scene", true, 
                "Will move the VR camera view in front of the heroine as they move around during TalkScene/HScene.  This mimics the default KK behavior. \n\nWhen disabled, you stay put as the heroine moves around.");
            MoveWithTalkScene.SettingChanged += MoveWithTalkScene_SettingsChanged;

            EnableControllerColliders = Config.Bind<bool>("", "Enable VR controller collision (boop!)", true, 
                "Allows collision of VR controllers with all dynamic bones.\n\nBoop!");
            EnableControllerColliders.SettingChanged += EnableControllerColliders_SettingsChanged;

            //Get VR flags
            bool noVrFlag = Environment.CommandLine.Contains("--novr");
            bool vrFlag = Environment.CommandLine.Contains("--novr");
            VREnabled = !noVrFlag && (vrFlag || SteamVRDetector.IsRunning);            

            if (VRPlugin.debugLog) VRPlugin.Logger.LogInfo($" VREnabled {VREnabled}");
            
            //IF not VR dont bother with VR hooks
            if (!VREnabled) return;

            GameAPI.RegisterExtraBehaviour<VRCameraGameController>(GUID);

            //Harmony init.  It's magic!
            Harmony harmony = new Harmony(GUID);
                        
            VRCameraHooks.InitHooks(harmony);
            VRControllerColliderHooks.InitHooks(harmony, this);            
        }      

        internal void MoveWithTalkScene_SettingsChanged(object sender, System.EventArgs e) 
        {            
            if (!MoveWithTalkScene.Value) 
            {
                VRCameraController.ClearLastPosition();
            }
        }

        internal void EnableControllerColliders_SettingsChanged(object sender, System.EventArgs e) 
        {
            if (!EnableControllerColliders.Value) 
            {
                //Just stop the dynamic bone search loop
                VRControllerColliderHelper.StopHelperCoroutine(this);
            } 
            else 
            {
                if (!VREnabled) return;
                VRControllerColliderHelper.TriggerHelperCoroutine(this);
            }
        }

    }
}