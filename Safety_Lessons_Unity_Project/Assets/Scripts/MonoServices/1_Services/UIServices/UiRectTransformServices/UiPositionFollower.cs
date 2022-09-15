using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.MonoUI
{
    public sealed class UiPositionFollower : UiMonoService
    {
        bool _canMove = true;
        Vector3 _targetToFollow;

        RectTransform _thisRectTranform;

        protected override void Awake()
        {
            base.Awake();

            _targetToFollow = _thisRectTranform.anchoredPosition;
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) StartFollowingCommand();
            if (methodNumb == 1) StopFollowingCommand();
            if (methodNumb == 2) UpdatePositionCommand((Vector3)passedObj);
        }

        IEnumerator FollowingPosition()
        {
            while (_canMove)
            {
                _thisRectTranform.anchoredPosition = Vector3.Lerp(_thisRectTranform.anchoredPosition, new Vector3(_targetToFollow.x, _thisRectTranform.anchoredPosition.y), 8 * Time.deltaTime);

                yield return null;
            }

        }

        void StartFollowingCommand()
        {
            _canMove = true;

            _targetToFollow = _thisRectTranform.anchoredPosition;
            StartCoroutine(FollowingPosition());
        }

        void StopFollowingCommand() =>
            _canMove = false;


        void UpdatePositionCommand(Vector3 position) =>
            _targetToFollow += position;

    }
}
