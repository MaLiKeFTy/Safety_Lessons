using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Animations
{
    public class RandomAnimatoionSequencePlayer : GetNextAnimatorStateClip
    {
        [SerializeField] AnimationSequence _animationSequence;
        [SerializeField] RuntimeAnimatorController _animatorController;

        AnimatorOverrideController _animatorOverride;
        List<AnimationClip> _originalAnimations = new List<AnimationClip>();

        protected override void Start()
        {
            base.Start();

            _animatorOverride = new AnimatorOverrideController(_animatorController);
            _ThisAnimator.runtimeAnimatorController = _animatorOverride;

            foreach (var animation in _animatorOverride.animationClips)
                _originalAnimations.Add(animation);

            OverrideFirstState();
        }


        protected override void OnValidate()
        {
            base.OnValidate();

            if (!_ThisAnimator)
            {
                _ThisAnimator = GetComponent<Animator>();
                _ThisAnimator.applyRootMotion = true;
            }

            if (_ThisAnimator.runtimeAnimatorController != _animatorController)
                _ThisAnimator.runtimeAnimatorController = _animatorController;
        }

        void OverrideFirstState()
        {
            List<KeyValuePair<AnimationClip, AnimationClip>> animtionsToOverride =
                new List<KeyValuePair<AnimationClip, AnimationClip>>();

            _animatorOverride.ApplyOverrides(animtionsToOverride);

        }

        void OverrideNoneCurrentStates(AnimationClip clip)
        {
            List<KeyValuePair<AnimationClip, AnimationClip>> animtionsToOverride =
                new List<KeyValuePair<AnimationClip, AnimationClip>>();

            foreach (var originalAnimation in _originalAnimations)
            {
                animtionsToOverride.Add(new KeyValuePair<AnimationClip, AnimationClip>(
                originalAnimation,
                null));
            }

            _animatorOverride.ApplyOverrides(animtionsToOverride);
        }

        void ChangeAnimationStateCommand()
        {

            _ThisAnimator.SetTrigger("ChangeState");
        }

        void ChangeAnimationSequenceCommand(AnimationSequence animationSequence)
        {
            _animationSequence = animationSequence;
        }

        void ChangeAnimationSequenceAndStateCommand(AnimationSequence animationSequence)
        {
            ChangeAnimationSequenceCommand(animationSequence);
            _ThisAnimator.SetTrigger("ChangeState");
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) ChangeAnimationStateCommand();
            if (methodNumb == 1) ChangeAnimationSequenceCommand((AnimationSequence)passedObj);
            if (methodNumb == 2) ChangeAnimationSequenceAndStateCommand((AnimationSequence)passedObj);
        }

        protected override void OnNextStateClip(AnimationClip nextClip)
        {
            OverrideNoneCurrentStates(nextClip);
        }
    }
}
