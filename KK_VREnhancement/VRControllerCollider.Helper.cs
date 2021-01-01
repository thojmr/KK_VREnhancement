using UnityEngine;
using System.Collections;

namespace KK_VREnhancement
{    
    public static class VRControllerColliderHelper
    {
        internal static bool coroutineActive = false;

        internal static void TriggerHelperCoroutine(VRPlugin pluginInstance) 
        {
            //Only trigger if not already running
            if (coroutineActive) return;
            coroutineActive = true;

            pluginInstance.StartCoroutine(LoopEveryXSeconds());
        }
        internal static void StopHelperCoroutine(VRPlugin pluginInstance) 
        {
            pluginInstance.StopCoroutine(LoopEveryXSeconds());

            coroutineActive = false;
        }

        //Got tired of searching for the correct hooks, just check for new dynamic bones on a loop.  Genious!
        internal static IEnumerator LoopEveryXSeconds()
        {
            while(coroutineActive) {                
                VRControllerCollider.SetVRControllerColliderToDynamicBones();

                // VRPlugin.Logger.Log(LogLevel.Info, $"Camera distance {distance}");

                yield return new WaitForSeconds(3);
            }
        }

    }
}