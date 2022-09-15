using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Animations
{
    public class AnimationParameterHandler : AnimatorMonoService
    {
        [SerializeField] string _paramName;

        void ChangeBoolParamCommand(bool value) =>
            _ThisAnimator.SetBool(_paramName, value);

        void ChangeFloatParamCommand() =>
            _ThisAnimator.SetFloat(_paramName, Random.Range(0, 2));

        void SetTriggerCommand() =>
            _ThisAnimator.SetTrigger(_paramName);

        void ChangeIntParamCommand(int value) =>
            _ThisAnimator.SetInteger(_paramName, value);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) ChangeBoolParamCommand((bool)passedObj);
            if (methodNumb == 1) ChangeFloatParamCommand();
            if (methodNumb == 2) SetTriggerCommand();
            if (methodNumb == 3) ChangeIntParamCommand((int)passedObj);
        }
    }
}