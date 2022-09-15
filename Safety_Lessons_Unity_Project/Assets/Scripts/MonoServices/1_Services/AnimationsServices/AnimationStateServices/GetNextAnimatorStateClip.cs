using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.Animations
{
    public class GetNextAnimatorStateClip : AnimatorMonoService
    {
        AnimationClip _clip;

        const string STATE_NAME_1 = "State1";
        const string STATE_NAME_2 = "State2";

        protected string _nextState;

        protected override void Start()
        {
            base.Start();


            _nextState = STATE_NAME_1;

            StartCoroutine(NextAnimatorState());
        }

        IEnumerator NextAnimatorState()
        {
            while (true)
            {
                GetNextClip();
                GetNextState();
                OnEndOfState();
                yield return null;
            }
        }


        void GetNextClip()
        {
            if (_ThisAnimator.GetNextAnimatorClipInfo(0) != null)
            {
                var clipInfo = _ThisAnimator.GetNextAnimatorClipInfo(0);

                _clip = clipInfo.Length > 0 ? clipInfo[0].clip : null;

            }
        }

        void GetNextState()
        {

            if (_ThisAnimator.GetCurrentAnimatorStateInfo(0).IsName(_nextState))
            {
                _nextState = _nextState == STATE_NAME_2 ? STATE_NAME_1 : STATE_NAME_2;

                NextStateClipCommand();
            }
        }

        void OnEndOfState()
        {
            var normalisedTime = _ThisAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (normalisedTime >= 0.98f && normalisedTime <= 1)
            {
                OnEndState();
            }
        }

        void NextStateClipCommand()
        {
            OnNextStateClip(_clip);
            InvokeCommand(0, _clip);
        }

        protected virtual void OnNextStateClip(AnimationClip nextClip) { }
        protected virtual void OnEndState() { }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
        }
    }
}