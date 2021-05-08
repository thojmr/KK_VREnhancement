using UnityEngine;
using Valve.VR;
using VRGIN.Core;
using VRGIN.Controls;

namespace KK_VREnhancement
{    
    public static class VRControllerInput
    {

        /// <summary>
        /// When user squeezes the grip, turn the camera via wrists angular veolcity
        /// </summary>
        internal static void CheckInputForSqueezeTurn()
        {
            if (!VRPlugin.SqueezeToTurn.Value) return;

            //Right hand
            var rCtrl = GameObject.FindObjectOfType<RightController>();
            //Detect grip squeeze
            if (rCtrl != null && rCtrl.Input.GetPress(EVRButtonId.k_EButton_Grip))
            {
                var aVelocity = rCtrl.Input.angularVelocity;
                //Turn head based on angular veolcity
                VRCamera.Instance.SteamCam.origin.Rotate(0f, -(aVelocity.y * Time.deltaTime * 100)/3, 0f, Space.Self);
            } 

            var lCtrl = GameObject.FindObjectOfType<LeftController>();
            if (lCtrl != null && lCtrl.Input.GetPress(EVRButtonId.k_EButton_Grip))
            {
                var aVelocity = lCtrl.Input.angularVelocity;
                //Turn head based on angular veolcity
                VRCamera.Instance.SteamCam.origin.Rotate(0f, -(aVelocity.y * Time.deltaTime * 100)/3, 0f, Space.Self);
            }

        }
    }
}