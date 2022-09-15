using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.Cam
{
    public class CameraLooker : MonoService
    {
        [SerializeField] bool _lookAtOnStart;

        Camera _camToLookAt;
        bool _isLooking;

        protected override void Start()
        {
            base.Start();

            _camToLookAt = Camera.main;

            if (_lookAtOnStart)
                StartLookingAtCameraCommand();
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) StartLookingAtCameraCommand();
            if (methodNumb == 1) StopLookingAtCameraCommand();
        }

        void StartLookingAtCameraCommand()
        {
            _isLooking = true;

            ActivateCoroutine(LookingAtCamera());
        }

        void StopLookingAtCameraCommand() =>
            _isLooking = false;

        IEnumerator LookingAtCamera()
        {
            while (_isLooking)
            {
                transform.LookAt(_camToLookAt.transform);

                yield return null;
            }
        }

    }
}