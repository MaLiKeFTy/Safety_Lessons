using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Animations
{
    public class AnimatorSequencePauser : MonoService
    {
        [SerializeField, Range(0, 1)] float _pausePoint;

        void PauseAnimatorCommand()
        {
            foreach (var animationSequence in GetComponentsInChildren<InlineAnimationSequenceWithSoundsPlayer>())
                animationSequence.PauseAnimationSequenceCommand();
        }

        void ResumeAnimatorCommand()
        {
            foreach (var animationSequence in GetComponentsInChildren<InlineAnimationSequenceWithSoundsPlayer>())
                animationSequence.ResumeAnimationSequenceCommand();
        }

        void RestartAnimatorCommand()
        {
            foreach (var animationSequence in GetComponentsInChildren<InlineAnimationSequenceWithSoundsPlayer>())
                animationSequence.RestartAnimationSequenceCommand();
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) PauseAnimatorCommand();
            if (methodNumb == 1) ResumeAnimatorCommand();
            if (methodNumb == 2) RestartAnimatorCommand();
        }
    }
}

