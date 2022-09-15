using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.Animations
{
    public class AnimatorInTransition : AnimatorMonoService
    {
        protected override void Start()
        {
            base.Start();

            ActivateCoroutine(AnimatorTransitionCheck());
        }

        IEnumerator AnimatorTransitionCheck()
        {
            while (true)
            {
                AnimatorTransitionInfo transitionInfo = _ThisAnimator.GetAnimatorTransitionInfo(0);

                if (transitionInfo.duration > 0)
                {
                    InTransitionCommand();

                    yield return new WaitForSeconds(transitionInfo.duration);
                }

                yield return null;
            }
        }

        void InTransitionCommand() =>
            InvokeCommand(0);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
        }
    }
}