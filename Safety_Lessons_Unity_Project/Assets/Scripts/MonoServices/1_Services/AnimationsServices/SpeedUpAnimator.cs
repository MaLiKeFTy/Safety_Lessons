using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Animations
{
    public class SpeedUpAnimator : AnimatorMonoService
    {
        [SerializeField] float _speed;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0)
                SpeedUpAnimatorCommand();
            else
                ResetAnimatorSpeedCommand();
        }

        void SpeedUpAnimatorCommand() { _ThisAnimator.speed = _speed; }

        void ResetAnimatorSpeedCommand() { _ThisAnimator.speed = 1; }
    }
}