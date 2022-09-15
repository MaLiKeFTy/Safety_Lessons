using MonoServices.Core;

namespace MonoServices.Animations
{
    public class AnimatorPlayer : AnimatorMonoService
    {
        void PlayAnimatorCommand()
        {
            _ThisAnimator.enabled = true;
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            PlayAnimatorCommand();
        }
    }
}
