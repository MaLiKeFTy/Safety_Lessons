using MonoServices.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace MonoServices.AR
{

    [RequireComponent(typeof(ARRaycastManager))]
    public class ArRaycaster : MonoService
    {
        [Space, SerializeField] TrackableType _trackableType = TrackableType.Planes;
        [SerializeField] bool _castOnStart;

        ARRaycastManager _arRaycastManager;
        bool _isRaycasting;
        Camera cam;

        protected override void Awake()
        {
            base.Awake();

            cam = Camera.main;
            _arRaycastManager = GetComponent<ARRaycastManager>();
        }

        protected override void Start()
        {
            base.Start();

            if (_castOnStart)
                StartRaycastCommand();

        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) StartRaycastCommand();
            if (methodNumb == 1) StopRaycastCommand();
        }

        void StartRaycastCommand()
        {
            _isRaycasting = true;

            ActivateCoroutine(Raycasting());

            InvokeCommand(0);
        }


        void StopRaycastCommand()
        {
            _isRaycasting = false;
            InvokeCommand(1);
        }


        IEnumerator Raycasting()
        {
            while (_isRaycasting)
            {
                var screenCenter = cam.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));

                var rayHits = new List<ARRaycastHit>();
                var raycastIsHit = _arRaycastManager.Raycast(screenCenter, rayHits, _trackableType);

                if (raycastIsHit)
                {

                    var rotPose = rayHits[0].pose.rotation;
                    var posPose = rayHits[0].pose.position;

                    var hitPose = new Pose(posPose, rotPose);


                    OnRaycastHitPoseCommand(hitPose);
                }


                yield return null;
            }
        }



        void OnRaycastHitPoseCommand(Pose rayHitPos)
        {
            if (rayHitPos != null)
                InvokeCommand(2, rayHitPos);

        }


    }
}