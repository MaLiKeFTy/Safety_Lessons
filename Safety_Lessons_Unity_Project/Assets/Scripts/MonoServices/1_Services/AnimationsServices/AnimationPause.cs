using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Animations
{
    public class AnimationPause : AnimatorMonoService
    {
        [SerializeField] bool _pauseOnStart;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0)
                PauseAnimationCommand();
            else
                PlayAnimationCommand();
        }

        protected override void Start()
        {
            base.Start();

            if (_pauseOnStart)
                PauseAnimationCommand();
        }

        void PauseAnimationCommand()
        {
            _ThisAnimator.speed = 0;
        }

        void PlayAnimationCommand()
        {
            _ThisAnimator.speed = 1;
        }
    }
}