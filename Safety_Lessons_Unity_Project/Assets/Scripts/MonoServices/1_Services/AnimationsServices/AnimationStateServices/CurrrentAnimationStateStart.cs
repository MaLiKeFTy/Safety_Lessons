using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.Animations
{
    public class CurrrentAnimationStateStart : AnimatorMonoService
    {
        [SerializeField] string[] _animStateNames;

        bool _stateStarted = false;

        protected override void Start()
        {
            base.Start();

            ActivateCoroutine(CheckAnimState());
        }

        IEnumerator CheckAnimState()
        {
            while (true)
            {
                for (int i = 0; i < _animStateNames.Length; i++)
                {
                    if (_ThisAnimator.GetCurrentAnimatorStateInfo(0).IsName(_animStateNames[i]))
                    {
                        var stateInfo = _ThisAnimator.GetCurrentAnimatorStateInfo(0);
                        var normalizedTime = stateInfo.normalizedTime;

                        if (normalizedTime <= 0.5 && !_stateStarted && !_ThisAnimator.IsInTransition(0))
                        {
                            StateStartedCommand(stateInfo);
                            _stateStarted = true;

                            yield return new WaitForSeconds(stateInfo.length);
                        }
                        else
                            _stateStarted = false;
                    }
                }

                yield return null;
            }
        }

        void StateStartedCommand(AnimatorStateInfo stateInfo) =>
            InvokeCommand(0, stateInfo);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
        }
    }
}