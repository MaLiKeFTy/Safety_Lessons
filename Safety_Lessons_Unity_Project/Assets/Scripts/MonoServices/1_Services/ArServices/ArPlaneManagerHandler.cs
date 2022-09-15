using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace MonoServices.AR
{

    [RequireComponent(typeof(ARPlaneManager))]
    public class ArPlaneManagerHandler : MonoService
    {
        ARPlaneManager _arPlaneManager;
        List<ARPlane> _arPlanes = new List<ARPlane>();

        protected override void Awake()
        {
            base.Awake();

            _arPlaneManager = GetComponent<ARPlaneManager>();

            _arPlaneManager.planesChanged += OnPlaneChanged;
        }


        void StartScanningPlaneCommand()
        {
            _arPlaneManager.enabled = true;
        }

        void StopScanningPlaneCommand()
        {
            _arPlaneManager.enabled = false;
        }

        void HidePlanesCommand()
        {
            _arPlaneManager.enabled = false;


            foreach (var g in _arPlaneManager.trackables)
            {
                Destroy(g.gameObject);
            }
        }


        void OnPlaneChanged(ARPlanesChangedEventArgs arPlanesChangedEvent)
        {
            _arPlanes.AddRange(arPlanesChangedEvent.added);
        }


        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) StartScanningPlaneCommand();
            if (methodNumb == 1) StopScanningPlaneCommand();
            if (methodNumb == 2) HidePlanesCommand();
        }

    }
}