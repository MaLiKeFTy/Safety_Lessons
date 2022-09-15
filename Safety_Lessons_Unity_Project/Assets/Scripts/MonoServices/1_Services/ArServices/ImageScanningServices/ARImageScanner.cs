using UnityEngine;
using UnityEngine.XR.ARFoundation;
using MonoServices.Core;

namespace MonoServices.AR
{

    [DisallowMultipleComponent]
    public class ARImageScanner : MonoService
    {
        ARTrackedImageManager _arTrackedImageManager;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) SetARTrackedImageManagerCommand((ARTrackedImageManager)passedObj);
            if (methodNumb == 1) StartScanningCommand();
            if (methodNumb == 2) StopScanningCommand();
        }

        void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
        {

            foreach (ARTrackedImage trackedImage in eventArgs.added)
            {
                GetAddedScannedImageTextureCommand(trackedImage.referenceImage.texture);
            }

            foreach (ARTrackedImage trackedImage in eventArgs.updated)
            {
                if (trackedImage.trackingState != UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
                    continue;

                GetUpdatedScannedImageTransformCommand(trackedImage.transform);
                GetUpdatedScannedImageTextureCommand(trackedImage.referenceImage.texture);
            }

            foreach (ARTrackedImage trackedImage in eventArgs.removed)
            {
                Debug.Log(trackedImage + " " + "removed");
            }

        }

        void SetARTrackedImageManagerCommand(ARTrackedImageManager arTrackedImageManager)
        {
            _arTrackedImageManager = arTrackedImageManager;
            InvokeCommand(0);
        }

        void StartScanningCommand()
        {
            _arTrackedImageManager.trackedImagesChanged += ImageChanged;
            InvokeCommand(1);
        }


        void StopScanningCommand()
        {
            if (!_arTrackedImageManager)
                return;

            _arTrackedImageManager.trackedImagesChanged -= ImageChanged;
            InvokeCommand(2);
        }

        void GetAddedScannedImageTextureCommand(Texture2D texture)
        {
            InvokeCommand(3, texture);
        }

        void GetUpdatedScannedImageTextureCommand(Texture2D texture) =>
            InvokeCommand(4, texture);

        void GetUpdatedScannedImageTransformCommand(Transform scannedImageTransform) =>
            InvokeCommand(5, scannedImageTransform);
    }
}
