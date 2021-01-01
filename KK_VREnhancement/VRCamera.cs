using System;
using UnityEngine;
using VRGIN.Helpers;
using VRGIN.Core;

namespace KK_VREnhancement 
{
    public static class VRCameraController 
    {
        //To keep track of the last heroine location
        internal static Vector3 lastIncommingPosition;
        internal static Quaternion lastIncommingRotation; 


        internal static void SetVRCamPosition(Transform advCam) 
        {
            float y = advCam.position.y;
            float y2 = VRCamera.Instance.SteamCam.head.position.y;
            Vector3 newPosition = new Vector3(advCam.position.x, y, advCam.position.z);
            Vector3 b = new Vector3(VRCamera.Instance.SteamCam.head.position.x, y2, VRCamera.Instance.SteamCam.head.position.z);
            VRCamera.Instance.SteamCam.origin.position += newPosition - b;       
        }

        internal static void SetVRCamRotation(Transform advCam, Boolean reverseRotate = false) 
        {                        
            Vector3 forwardVector = Calculator.GetForwardVector(advCam.rotation);
            Vector3 forwardVector2 = Calculator.GetForwardVector(VRCamera.Instance.SteamCam.head.rotation);
            VRCamera.Instance.SteamCam.origin.rotation *= Quaternion.FromToRotation(forwardVector2, forwardVector);

            if (reverseRotate) {
                ReverseRotate(VRCamera.Instance.SteamCam.origin);
            }
        }

        // Move the VR view to a new position and rotation (In front of the heroine) 
        internal static void MoveToFaceHeroine_ADVScene(Vector3 position, Quaternion rotation) 
        {
            //Only move VR view when distance is greater than xf (meters?) Don't need to move VR view when heroine is just standing/sitting.
            if (VRCameraHelper.IsNewPosition(lastIncommingPosition, position) == false) return;

            lastIncommingPosition = position;
            lastIncommingRotation = rotation;

            Transform newTransform = VRCameraHelper.ConvertPositionToTransform(position, rotation);

            SetVRCamRotation(newTransform, true);                   
            SetVRCamPosition(newTransform); 
            VRCamera.Instance.SteamCam.origin.Translate(new Vector3(-1f, 0.1f, 0f));//Make some personal space  
        }


        // Move the VR view to a new position and rotation (In front of the heroine you are defiling) 
        internal static void MoveToFaceHeroine_HScene(Vector3 position, Quaternion rotation) 
        {
            //Only move VR view when distance is greater than xf (meters?) Don't need to move VR view when heroine is just standing/sitting.
            if (VRCameraHelper.IsNewPosition(VRCamera.Instance.SteamCam.origin.position, position, 2f) == false) return; 

            lastIncommingPosition = position;
            lastIncommingRotation = rotation;

            Transform ActSceCamTransform = VRCameraHelper.ConvertPositionToTransform(position, rotation);
            SetVRCamRotation(ActSceCamTransform, true);
            SetVRCamPosition(ActSceCamTransform); 

            VRCamera.Instance.SteamCam.origin.Translate(new Vector3(-1f, 0.1f, 0f));//Move camera back from character, otherwise you get to see their insides :)
        }
        
        internal static void ReverseRotate(Transform camTransform) 
        {
            camTransform.Rotate(Vector3.up * -180);
        }

        internal static void ClearLastPosition() 
        {
            lastIncommingPosition = new Vector3();
            lastIncommingRotation = new Quaternion();
        }








        //****Test methods*****
        //Move VR camera to a specific Vector3 position
        internal static void MoveToPositionOnly(Vector3 position) 
        {
            GameObject camGameObject = new GameObject();
            Transform newTransform = camGameObject.transform;
            newTransform.position = position;

            if (VRCameraHelper.IsNewPosition(lastIncommingPosition, position) == false) return;

            SetVRCamPosition(newTransform);
        }

    }    
}