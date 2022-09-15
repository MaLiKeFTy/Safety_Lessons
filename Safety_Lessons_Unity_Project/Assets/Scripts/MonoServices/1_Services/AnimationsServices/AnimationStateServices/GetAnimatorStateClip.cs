using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.Animations
{
    public class GetAnimatorStateClip : AnimatorMonoService
    {
        AnimationClip _clip;

        protected override void Start()
        {
            base.Start();

            ActivateCoroutine(CurrAnimatorState());
        }

        IEnumerator CurrAnimatorState()
        {
            while (true)
            {
                var clipInfo = _ThisAnimator.GetCurrentAnimatorClipInfo(0);

                _clip = clipInfo.Length > 0 ? clipInfo[0].clip : null;

                GetStateClipCommand();

                yield return null;
            }
        }

        void GetStateClipCommand() =>
            InvokeCommand(0, _clip);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
        }
    }
}