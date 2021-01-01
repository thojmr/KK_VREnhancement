using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using VRGIN.Helpers;

namespace KK_VREnhancement 
{
    [BepInPlugin(GUID, GUID, Version)]
    public partial class VRPlugin : BaseUnityPlugin 
    {
        public const string GUID = "KK_VREnhancement";
        public const string Version = "0.1";
        public static ConfigEntry<bool> MoveWithTalkScene { get; private set; }
        public static ConfigEntry<bool> EnableControllerColliders { get; private set; }

        internal static new ManualLogSource Logger { get; private set; }
        internal static bool VREnabled = false;

        internal void Start() 
        {
            Logger = base.Logger;

            MoveWithTalkScene = Config.Bind<bool>("", "Enable move with scene", true, 
                "Will move the VR camera view in front of the heroine as they move around during TalkScene/HScene\n\nWhen disabled, you stay put as the heroine moves around.");
            MoveWithTalkScene.SettingChanged += MoveWithTalkScene_SettingsChanged;

            EnableControllerColliders = Config.Bind<bool>("", "Enable VR controller colliders", true, 
                "Allows collision of VR controllers with all dynamic bones.\n\nBoop!");
            EnableControllerColliders.SettingChanged += EnableControllerColliders_SettingsChanged;

            //Get VR flags
            bool noVrFlag = Environment.CommandLine.Contains("--novr");
            bool vrFlag = Environment.CommandLine.Contains("--novr");
            VREnabled = !noVrFlag && (vrFlag || SteamVRDetector.IsRunning);            

            //IF not VR dont bother with VR hooks
            if (!VREnabled) return;

            //Harmony init.  It's magic!
            Harmony harmony = new Harmony(GUID);
                        
            VRCameraHooks.InitHooks(harmony);
            VRControllerColliderHooks.InitHooks(harmony, this);            
        }      

        internal void MoveWithTalkScene_SettingsChanged(object sender, System.EventArgs e) 
        {            
            if (!MoveWithTalkScene.Value) {
                VRCameraController.ClearLastPosition();
            }
        }

        internal void EnableControllerColliders_SettingsChanged(object sender, System.EventArgs e) 
        {
            if (!EnableControllerColliders.Value) {
                //Just stop the dynamic bone search loop
                VRControllerColliderHelper.StopHelperCoroutine(this);
            } else {
                if (!VREnabled) return;
                VRControllerColliderHelper.TriggerHelperCoroutine(this);
            }
        }

    }
}