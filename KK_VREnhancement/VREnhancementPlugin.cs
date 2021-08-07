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
    [BepInProcess("Koikatu")]
    [BepInProcess("Koikatsu Party")]
    public partial class VRPlugin : BaseUnityPlugin 
    {
        public const string GUID = "KK_VREnhancement";
        public const string Version = "0.3";

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
            VRControllerColliderHelper.pluginInstance = this;

            PluginConfigInit();

            //Get VR flags
            bool noVrFlag = Environment.CommandLine.Contains("--novr");
            VREnabled = !noVrFlag && SteamVRDetector.IsRunning;            

            if (VRPlugin.debugLog) VRPlugin.Logger.LogInfo($" VREnabled {VREnabled}");
            
            //IF not VR dont bother with VR hooks
            if (!VREnabled) return;            

            //Set up game mode detectors to start certain logic when loading into main game
            GameAPI.RegisterExtraBehaviour<VRControllerGameController>(GUID + "_controller");

            //Harmony init.  It's magic!
        }      


        //Check for controller input changes
        internal void Update()
        {
             
        }

    }
}