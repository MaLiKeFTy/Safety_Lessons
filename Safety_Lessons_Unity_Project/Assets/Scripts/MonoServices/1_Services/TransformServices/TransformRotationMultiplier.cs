using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Transforms
{
    public class TransformRotationMultiplier : TransformMonoService
    {
        [SerializeField] float _speedMultiplier = 0.2f;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) ChangeRotationCommand((Vector3)passedObj);
            if (methodNumb == 1) AddToRotationCommand((Vector3)passedObj);
        }


        void ChangeRotationCommand(Vector3 rotMultipler)
        {
            _ThisTransform.eulerAngles = _ThisTransform.eulerAngles + (new Vector3(_ThisTransform.eulerAngles.x, -rotMultipler.x, transform.eulerAngles.z) * _speedMultiplier);

            InvokeCommand(0);
        }


        void AddToRotationCommand(Vector3 rotationToAdd)
        {
            transform.eulerAngles += rotationToAdd;

            InvokeCommand(1);
        }

    }
}
