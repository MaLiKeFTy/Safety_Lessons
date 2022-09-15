using MonoServices.Core;
using System.Collections;

namespace MonoServices.Animations
{
    public class AnimationCompletionPercentageGetter : AnimatorMonoService
    {
        protected override void Start()
        {
            base.Start();

            ActivateCoroutine(NormalizedTimeCheck());
        }

        IEnumerator NormalizedTimeCheck()
        {
            while (true)
            {
                GetNormalizedTimeCommand(_ThisAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);

                yield return null;
            }
        }

        void GetNormalizedTimeCommand(float normalizedtime) =>
            InvokeCommand(0, normalizedtime);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
        }
    }
}