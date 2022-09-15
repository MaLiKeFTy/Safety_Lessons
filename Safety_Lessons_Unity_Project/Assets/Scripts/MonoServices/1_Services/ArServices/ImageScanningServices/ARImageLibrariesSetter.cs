using MonoServices.Core;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace MonoServices.AR
{
    [RequireComponent(typeof(ARTrackedImageManager))]
    public class ARImageLibrariesSetter : MonoService
    {
        ARTrackedImageManager _arTrackedImageManager;

        protected override void Awake()
        {
            base.Awake();

            _arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        }


        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
            SetImageLibraryCommand((XRReferenceImageLibrary)passedObj);


        void SetImageLibraryCommand(XRReferenceImageLibrary imageLibrary)
        {
            _arTrackedImageManager.referenceLibrary = imageLibrary;

            InvokeCommand(0, _arTrackedImageManager);
        }
    }
}
