using HarmonyLib;
using UnityEngine;
using System.Reflection.Emit;
using System.Collections.Generic;
using KKAPI.MainGame;
using VRGIN.Controls.Tools;

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
        [HarmonyPatch(typeof(MenuTool), "OnUpdate")]
        internal static IEnumerable<CodeInstruction> PreventTakeMenuTook_Transpiler(IEnumerable<CodeInstruction> instructions)
        {           
            List<CodeInstruction> instructionsList = new List<CodeInstruction>(instructions);

            for (var i = 0; i < instructionsList.Count; i++)
            {
                if (VRPlugin.debugLog) VRPlugin.Logger.LogInfo($" {instructionsList[i].operand} ");
                
                if (instructionsList[i].opcode == OpCodes.Ldc_I4_4)
                {
                    if (VRPlugin.debugLog) VRPlugin.Logger.LogInfo($" Transpiler matching operand found ");
                    instructionsList[i].opcode = OpCodes.Ldc_I4_7;
                    break;
                }               
            }     

            return instructionsList;       
        }
        
    
    }
}