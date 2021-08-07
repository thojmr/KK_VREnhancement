using BepInEx.Configuration;
using HarmonyLib;

namespace KK_VREnhancement 
{
    public partial class VRPlugin 
    {
        public static ConfigEntry<bool> EnableControllerColliders { get; private set; }
        public static ConfigEntry<bool> SqueezeToTurn { get; private set; }


        /// <summary>
        /// Init the Bepinex config manager options
        /// </summary>
        public void PluginConfigInit() 
        {
            EnableControllerColliders = Config.Bind<bool>("VR General", "Enable VR controller collision (boop!)", true, 
                "Allows collision of VR controllers with all dynamic bones.\n\nBoop!");
            EnableControllerColliders.SettingChanged += EnableControllerColliders_SettingsChanged;

            // SqueezeToTurn = Config.Bind<bool>("VR General", "Squeeze to Turn", false, 
            //     new ConfigDescription("Allows you to turn the headset by squeezing controllers."));
          
        }      


        internal void EnableControllerColliders_SettingsChanged(object sender, System.EventArgs e) 
        {
            if (!EnableControllerColliders.Value) 
            {            
                //Force recalculate all verts.  With balloon active it will automatically calaulcate the correct new boundaries
                VRControllerColliderHelper.StopHelperCoroutine();                                      
            } 
            else 
            {                
                VRControllerColliderHelper.TriggerHelperCoroutine();
            }
        }

    }
}