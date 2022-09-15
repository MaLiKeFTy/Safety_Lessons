using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.Transforms
{

    [RequireComponent(typeof(Transform))]
    public class TransformRotationFollower : TransformMonoService
    {
        [SerializeField] bool _rotateOnStart;
        [SerializeField] float _rotationSpeed = 4;


        bool _canFollowRotation;
        Quaternion _targetRotation = Quaternion.identity;
        IEnumerator _followingRorationCorotine;


        protected override void Start()
        {
            base.Start();

            if (_rotateOnStart)
                FollowRotationCommand();
        }

        void SetRotationToFollowCommand(Quaternion quaternionToFollow) =>
            _targetRotation = quaternionToFollow;


        void FollowRotationCommand()
        {
            _canFollowRotation = true;

            if (_followingRorationCorotine != null)
                StopCoroutine(_followingRorationCorotine);

            StartCoroutine(_followingRorationCorotine = FollowingRoration());
        }

        void StopFollowingCommand() =>
            _canFollowRotation = false;

        IEnumerator FollowingRoration()
        {
            while (_canFollowRotation)
            {
                _ThisTransform.rotation = Quaternion.Lerp(_ThisTransform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);

                yield return null;
            }
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) SetRotationToFollowCommand((Quaternion)passedObj);
            if (methodNumb == 1) FollowRotationCommand();
            if (methodNumb == 2) StopFollowingCommand();
        }
    }
}