using MonoServices.Core;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace MonoServices.AR
{
    [RequireComponent(typeof(ARSessionOrigin))]
    public class ArRuntimeImageLibraryManagerAdder : MonoService
    {
        [SerializeField] int _movingImages = 3;
        [SerializeField] GameObject _prefab;
        [SerializeField] bool _addOnStart;

        ARTrackedImageManager _trackImageManager;

        protected override void Start()
        {
            base.Start();

            if (_addOnStart)
                AddRuntimeImageLibraryManagerCommand();
        }

        void AddRuntimeImageLibraryManagerCommand()
        {

            if (_trackImageManager)
            {
                InvokeCommand(0, _trackImageManager);
                return;
            }


            if (Application.isEditor)
                return;

            _trackImageManager = gameObject.AddComponent<ARTrackedImageManager>();

            _trackImageManager.referenceLibrary = _trackImageManager.CreateRuntimeLibrary();

            _trackImageManager.requestedMaxNumberOfMovingImages = _movingImages;

            if (_prefab)
                _trackImageManager.trackedImagePrefab = _prefab;

            _trackImageManager.enabled = true;

            InvokeCommand(0, _trackImageManager);

        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) AddRuntimeImageLibraryManagerCommand();
        }
    }
}
