using KKAPI.MainGame;

namespace KK_VREnhancement 
{
    public class VRControllerGameController : GameCustomFunctionController
    {
        protected override void OnGameLoad(GameSaveLoadEventArgs args)
        {
            if (VRPlugin.debugLog) VRPlugin.Logger.LogInfo($" OnGameLoad for ControllerColliders");

            //Set up controller colliders when main game starts
            VRControllerColliderHelper.TriggerHelperCoroutine();
        }
    }
}