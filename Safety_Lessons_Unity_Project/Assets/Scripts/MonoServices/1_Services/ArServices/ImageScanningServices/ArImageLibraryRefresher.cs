using MonoServices.Core;
using UnityEngine.XR.ARFoundation;

namespace MonoServices.AR
{
    public class ArImageLibraryRefresher : MonoService
    {
        ARTrackedImageManager _arTrackedImageManger;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0)
                GetArTrackedImageMangerCommand((ARTrackedImageManager)passedObj);
            else
                RefreshImageLibraryCommand();
        }

        void GetArTrackedImageMangerCommand(ARTrackedImageManager arTrackedImageManger)
        {
            _arTrackedImageManger = arTrackedImageManger;
        }

        void RefreshImageLibraryCommand()
        {
            _arTrackedImageManger.referenceLibrary = _arTrackedImageManger.CreateRuntimeLibrary();

            InvokeCommand(1);
        }

    }
}
