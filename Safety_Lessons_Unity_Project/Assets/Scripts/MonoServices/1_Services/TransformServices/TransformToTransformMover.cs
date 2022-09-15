using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.Transforms
{
    public sealed class TransformToTransformMover : TransformMonoService
    {
        [SerializeField] float _moveSpeed = 0.5f;
        [SerializeField] AnimationCurve _speedCurve;

        Transform _transToMoveTo;

        void GetTransformCommand(Transform transToMove)
        {
            _transToMoveTo = transToMove;
        }

        void SetPositionCommand()
        {
            ActivateCoroutine(MovingTrans(_transToMoveTo));
        }

        void GetAndSetPositionCommand(Transform transToMove)
        {
            ActivateCoroutine(MovingTrans(transToMove));
        }

        void SetPositionAndRotationCommand()
        {
            ActivateCoroutine(MovingTrans(_transToMoveTo, true));
        }

        void GetAndSetPositionAndRotationCommand(Transform transToMoveTo)
        {
            ActivateCoroutine(MovingTrans(transToMoveTo, true));
        }

        void StopMovingCommand()
        {
            DisableCoroutine();
        }

        void OnArrivalCommand()
        {
            InvokeCommand(6, null);
        }

        IEnumerator MovingTrans(Transform transToMoveTo, bool applyRotation = false)
        {
            var startPos = _ThisTransform.position;
            var startRot = _ThisTransform.rotation;

            float currentLerpTime = 0;
            float targetLerpTime = 1;

            while (currentLerpTime != targetLerpTime)
            {
                currentLerpTime = Mathf.MoveTowards(currentLerpTime, targetLerpTime, _moveSpeed * Time.deltaTime);

                _ThisTransform.position = Vector3.Lerp(startPos, transToMoveTo.position, _speedCurve.Evaluate(currentLerpTime));

                if (applyRotation)
                    _ThisTransform.rotation = Quaternion.Lerp(startRot, transToMoveTo.rotation, _speedCurve.Evaluate(currentLerpTime));

                yield return null;
            }

            OnArrivalCommand();
            yield return null;
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) GetTransformCommand((Transform)passedObj);

            if (methodNumb == 1) SetPositionCommand();
            if (methodNumb == 2) GetAndSetPositionCommand((Transform)passedObj);

            if (methodNumb == 3) SetPositionAndRotationCommand();
            if (methodNumb == 4) GetAndSetPositionAndRotationCommand((Transform)passedObj);

            if (methodNumb == 5) StopMovingCommand();
        }
    }
}
