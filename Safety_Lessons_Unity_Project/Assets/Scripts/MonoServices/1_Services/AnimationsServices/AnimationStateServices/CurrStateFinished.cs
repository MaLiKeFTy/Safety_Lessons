using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.Animations
{
    public class CurrStateFinished : AnimatorMonoService
    {
        [SerializeField] string[] _animStateNames;

        bool _stateFinished = false;

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

                        if (stateInfo.normalizedTime >= 0.95 && !_stateFinished)
                        {
                            StateFinishedCommand(stateInfo);
                            _stateFinished = true;
                            yield return new WaitForSeconds(1f);
                        }
                        else
                            _stateFinished = false;
                    }
                }
                yield return null;
            }
        }

        void StateFinishedCommand(AnimatorStateInfo stateInfo) =>
            InvokeCommand(0, stateInfo);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
        }

    }
}