using ActionGame;
using KKAPI.MainGame;

namespace KK_VREnhancement 
{
    public class VRCameraGameController : GameCustomFunctionController
    {
        protected override void OnDayChange(Cycle.Week day)
        {
            if (VRPlugin.debugLog) VRPlugin.Logger.LogInfo($" OnDayChange {day}");
            VRCameraController.ClearLastPosition();
        }

        protected override void OnStartH(HSceneProc proc, bool freeH)
        {
            if (VRPlugin.debugLog) VRPlugin.Logger.LogInfo($" OnStartH ");
            VRCameraController.ClearLastPosition();
        }

        protected override void OnEndH(HSceneProc proc, bool freeH)
        {
            if (VRPlugin.debugLog) VRPlugin.Logger.LogInfo($" OnEndH ");
            VRCameraController.ClearLastPosition();
        }

        protected override void OnGameLoad(GameSaveLoadEventArgs args)
        {
            VRCameraController.OnGameLoad();
        }

        protected override void OnPeriodChange(Cycle.Type period)
        {
            if (VRPlugin.debugLog) VRPlugin.Logger.LogInfo($" OnPeriodChange ");
            VRCameraController.ClearLastPosition();
        }
    }
}