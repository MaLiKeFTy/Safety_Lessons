using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.Animations
{
    public class CurrAnimatorStateGetter : AnimatorMonoService
    {
        [SerializeField] string[] _animStates;

        const float _refreshRate = 0.2f;

        protected override void Awake()
        {
            base.Awake();

            GetCurrAnimationStatesCommand();
        }

        void GetCurrAnimationStatesCommand() =>
            StartCoroutine(GettingCurrAnimState());

        IEnumerator GettingCurrAnimState()
        {
            while (true)
            {
                for (int i = 0; i < _animStates.Length; i++)
                {
                    if (_ThisAnimator.GetCurrentAnimatorStateInfo(0).IsName(_animStates[i]))
                        InvokeCommand(0);
                }

                yield return new WaitForSeconds(_refreshRate);
            }
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
        }
    }
}
