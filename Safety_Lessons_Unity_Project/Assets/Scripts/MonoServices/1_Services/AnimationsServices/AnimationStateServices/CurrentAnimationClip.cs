using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.Animations
{
    public class CurrentAnimationClip : AnimatorMonoService
    {
        AnimationClip _clip;

        protected override void Start()
        {
            base.Start();

            ActivateCoroutine(CurrentClipCheck());
        }

        IEnumerator CurrentClipCheck()
        {
            while (true)
            {
                var clipInfo = _ThisAnimator.GetCurrentAnimatorClipInfo(0);

                if (clipInfo.Length > 0)
                {
                    _clip = clipInfo[0].clip;
                    GetCurrentClipCommand();
                }

                yield return null;
            }
        }

        void GetCurrentClipCommand() =>
            InvokeCommand(0, _clip);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
        }
    }
}