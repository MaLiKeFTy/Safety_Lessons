using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Cam
{
    [RequireComponent(typeof(Camera))]
    public class CameraCordsGetter : MonoService
    {
        Camera _cam;
        protected override void Awake()
        {
            base.Awake();

            _cam = GetComponent<Camera>();
        }

        void MultiplyVectorWithCamCordsCommand(Vector3 vectorToMultiply)
        {
            var forwardCamCord = _cam.transform.forward.normalized;
            var rightCamCord = _cam.transform.right.normalized;

            float horisontalAxis = vectorToMultiply.normalized.z;
            float verticalAxis = vectorToMultiply.normalized.x;

            Vector3 vectorToSend = forwardCamCord * horisontalAxis + rightCamCord * verticalAxis;

            vectorToSend.y = vectorToMultiply.y;

            InvokeCommand(0, vectorToSend);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) MultiplyVectorWithCamCordsCommand((Vector3)passedObj);
        }
    }

}

