using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.Transforms
{
    public sealed class TransformSingleAxisEulerAnglesRotator : TransformMonoService
    {
        [SerializeField] float _rotationSpeed = 8;

        bool _canRotate;
        IEnumerator _rotatingEulerAnglesCorotine;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) MoveEulerAnglesCommand((float)passedObj);
            if (methodNumb == 1) StopRotatingCommand();
        }

        void MoveEulerAnglesCommand(float rotationValue)
        {
            _canRotate = true;

            if (_rotatingEulerAnglesCorotine != null)
                StopCoroutine(_rotatingEulerAnglesCorotine);

            StartCoroutine(_rotatingEulerAnglesCorotine = MovingEulerAngles(rotationValue));
        }

        IEnumerator MovingEulerAngles(float rotationValue)
        {
            Vector3 targetRotation = new Vector3(_ThisTransform.eulerAngles.x, rotationValue, _ThisTransform.eulerAngles.z);


            while (_canRotate)
            {

                if (Vector3.Distance(_ThisTransform.eulerAngles, targetRotation) > 0.1f)
                    _ThisTransform.eulerAngles = Vector3.Lerp(_ThisTransform.eulerAngles, targetRotation, _rotationSpeed * Time.deltaTime);

                yield return null;

            }
        }

        void StopRotatingCommand() =>
            _canRotate = false;
    }
}