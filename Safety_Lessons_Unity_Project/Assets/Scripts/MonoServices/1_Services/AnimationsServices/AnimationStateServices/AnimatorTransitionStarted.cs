using MonoServices.Core;
using System.Collections;

namespace MonoServices.Animations
{
    public class AnimatorTransitionStarted : AnimatorMonoService
    {
        bool _TransitionStarted;

        protected override void Start()
        {
            base.Start();

            ActivateCoroutine(TransitionStartCheck());
        }

        IEnumerator TransitionStartCheck()
        {
            while (true)
            {
                if (_ThisAnimator.IsInTransition(0))
                {
                    if (!_TransitionStarted)
                    {
                        TransitionStartedCommand();
                        _TransitionStarted = true;
                    }
                }
                else
                    _TransitionStarted = false;

                yield return null;
            }
        }

        void TransitionStartedCommand() =>
            InvokeCommand(0);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
        }
    }
}