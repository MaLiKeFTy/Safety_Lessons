using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Animations
{
    public class AnimationStateLength : MonoService
    {
        float _stateLength;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
            SetStateLengthCommand((AnimatorStateInfo)passedObj);

        void SetStateLengthCommand(AnimatorStateInfo stateInfo)
        {
            _stateLength = stateInfo.length;
            GetStateLengthCommand();
        }

        void GetStateLengthCommand() =>
            InvokeCommand(1, _stateLength);
    }
}