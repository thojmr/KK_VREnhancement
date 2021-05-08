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
            //Right hand
            var rCtrl = GameObject.FindObjectOfType<RightController>();
            var deviceR = rCtrl?.Input;
            //Detect grip squeeze
            if (deviceR != null && deviceR.GetPressDown(EVRButtonId.k_EButton_Grip))
            {
                var aVelocity = deviceR.angularVelocity;
                //Turn head based on angular veolcity
                VRCamera.Instance.SteamCam.origin.Rotate(0f, -(aVelocity.y * Time.deltaTime * 100), 0f, Space.Self);
            }

            var lCtrl = GameObject.FindObjectOfType<LeftController>();
            var deviceL = lCtrl?.Input;
            if (deviceL != null && deviceL.GetPressDown(EVRButtonId.k_EButton_Grip))
            {
                var aVelocity = deviceL.angularVelocity;
                VRCamera.Instance.SteamCam.origin.Rotate(0f, -(aVelocity.y * Time.deltaTime * 100), 0f, Space.Self);
            }
        }
    }
}