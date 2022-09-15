using MonoServices.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace MonoServices.AR
{
    public class PlacementIndicator : MonoService
    {
        [Space, SerializeField] ARRaycastManager _arRaycastManager;
        [SerializeField] GameObject _indicatorVisual;
        [SerializeField] TrackableType _trackableType = TrackableType.Planes;


        List<ARRaycastHit> _hits = new List<ARRaycastHit>();

        protected override void Start()
        {
            base.Start();

            ActivateCoroutine(SurfaceCheck());
        }

        IEnumerator SurfaceCheck()
        {
            while (true)
            {
                _arRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), _hits, _trackableType);

                if (_hits.Count > 0)
                {
                    if (_indicatorVisual)
                        _indicatorVisual.SetActive(true);

                    transform.SetPositionAndRotation(_hits[0].pose.position, _hits[0].pose.rotation);

                    GetPlacementIndicatorTransformCommand();
                }

                yield return null;
            }
        }

        void GetPlacementIndicatorTransformCommand() =>
            InvokeCommand(0, transform);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
        }
    }
}