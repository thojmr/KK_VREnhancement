using UnityEngine;
using Valve.VR;
using VRGIN.Core;
using VRGIN.Controls;

namespace KK_VREnhancement
{    
    public static class VRControllerInput
    {

        internal static void OnGameLoad()
        {      
            // //Make vr origin same as head position's x,z
            // // VRCamera.Instance.SteamCam.head.position = new Vector3(VRCamera.Instance.SteamCam.head.position.x, VRCamera.Instance.SteamCam.origin.position.y , VRCamera.Instance.SteamCam.head.position.z);
            // DebugTools.DrawSphereAndAttach(VRCamera.Instance.SteamCam.origin, 0.1f);
            // DebugTools.DrawLineAndAttach(VRCamera.Instance.SteamCam.origin, 0.1f);      
        }

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
                VRCamera.Instance.SteamCam.origin.RotateAround(VRCamera.Instance.SteamCam.head.position, new Vector3(0, 1, 0), -(aVelocity.y * Time.deltaTime * 100)/4);
            } 

            var lCtrl = GameObject.FindObjectOfType<LeftController>();
            if (lCtrl != null && lCtrl.Input.GetPress(EVRButtonId.k_EButton_Grip))
            {
                var aVelocity = lCtrl.Input.angularVelocity;
                //Turn head based on angular veolcity
                VRCamera.Instance.SteamCam.origin.RotateAround(VRCamera.Instance.SteamCam.head.position, new Vector3(0, 1, 0), -(aVelocity.y * Time.deltaTime * 100)/4);
            }

        }
    }
}