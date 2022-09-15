using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Animations
{
    public class RestartAnimationState : AnimatorMonoService
    {
        [SerializeField] string _stateName;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
            RestartStateCommand();

        void RestartStateCommand() =>
            _ThisAnimator.Play(_stateName, -1, 0);
    }
}
