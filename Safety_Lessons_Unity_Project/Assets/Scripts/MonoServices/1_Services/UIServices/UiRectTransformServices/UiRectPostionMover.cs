using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.MonoUI
{
    public sealed class UiRectPostionMover : UiMonoService
    {
        Vector3 _targetPostion;
        bool stopMoving;

        void GetRectPositionCommand(Vector3 targetPostion) =>
            _targetPostion = targetPostion;

        IEnumerator MovingRectPosition()
        {
            stopMoving = false;

            while (Vector2.Distance(_ThisRectTransform.anchoredPosition, _targetPostion) > float.Epsilon)
            {
                if (stopMoving)
                {
                    stopMoving = false;
                    break;
                }

                _ThisRectTransform.anchoredPosition = Vector2.Lerp(_ThisRectTransform.anchoredPosition, _targetPostion, 8 * Time.deltaTime);
                yield return null;
            }
        }

        void MoveRectPostionCommand() =>
            ActivateCoroutine(MovingRectPosition());

        void StopMovingRectCommand() =>
            stopMoving = true;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) GetRectPositionCommand((Vector3)passedObj);
            if (methodNumb == 1) MoveRectPostionCommand();
            if (methodNumb == 2) StopMovingRectCommand();
        }
    }
}