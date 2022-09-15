using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Animations
{
    public class AnimatorSpeedChanger : AnimatorMonoService
    {
        [SerializeField] float[] _animatorSpeed = { 1 };
        [SerializeField] bool _changeSpeedOnStart;

        protected override void Start()
        {
            base.Start();

            if (_changeSpeedOnStart)
                ChangeAnimatorSpeedCommand(0);
        }

        void ChangeAnimatorSpeedCommand(int speedIndex)
        {
            _ThisAnimator.speed = _animatorSpeed[speedIndex];
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) ChangeAnimatorSpeedCommand(0);
        }


    }
}
