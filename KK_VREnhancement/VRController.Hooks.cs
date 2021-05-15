using HarmonyLib;
using UnityEngine;
using System.Reflection.Emit;
using System.Collections.Generic;
using KKAPI.MainGame;
using VRGIN.Controls.Handlers;

namespace KK_VREnhancement
{
    public static class VRControllerHooks
    {
        public static Harmony harmonyInstance;

        internal static void InitHooks(Harmony _harmonyInstance = null)
        {
            if (_harmonyInstance != null) harmonyInstance = _harmonyInstance;

            if (harmonyInstance == null) return;
            harmonyInstance.PatchAll(typeof(VRControllerHooks));
        }

        internal static void UnInitHooks(string harmonyGUID)
        {
            if (harmonyInstance == null) return;
            harmonyInstance.UnpatchAll(harmonyGUID);
        }

        //Prevent controller grip from taking MenuTool back (since it does rotation now)
        [HarmonyTranspiler]
        [HarmonyPatch(typeof(MenuHandler), "CheckInput")]
        internal static IEnumerable<CodeInstruction> PreventTakeMenuTook_Transpiler(IEnumerable<CodeInstruction> instructions)
        {           
            List<CodeInstruction> instructionsList = new List<CodeInstruction>(instructions);

            for (var i = 0; i < instructionsList.Count; i++)
            {
                //When the takeMenu op is found, set it to nop
                if (instructionsList[i].operand != null && instructionsList[i].operand.ToString().Contains("TakeGUI(VRGIN.Visuals.GUIQuad)"))
                {
                    if (VRPlugin.debugLog) VRPlugin.Logger.LogInfo($" Transpiler matching operand found ");
                    instructionsList.RemoveAt(i);
                    instructionsList.RemoveAt(i -1);
                    instructionsList.RemoveAt(i -2);
                    instructionsList.RemoveAt(i -3);
                }               
            }     

            return instructionsList;       
        }
        
    
    }
}