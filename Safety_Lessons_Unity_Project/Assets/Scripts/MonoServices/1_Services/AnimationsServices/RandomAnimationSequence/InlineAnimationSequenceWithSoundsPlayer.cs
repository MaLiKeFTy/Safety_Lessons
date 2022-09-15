using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Animations
{

    [RequireComponent(typeof(AudioSource))]

    public class InlineAnimationSequenceWithSoundsPlayer : GetNextAnimatorStateClip
    {
        [SerializeField] AnimationSequence _animationSequence;
        [SerializeField] RuntimeAnimatorController _animatorController;
        [SerializeField] bool _loopSequence;
        [SerializeField] bool _playSquenceOnStart = true;

        AnimatorOverrideController _animatorOverride;
        readonly List<AnimationClip> _originalAnimations = new List<AnimationClip>();

        int _currAnimIndex = 0;

        int _animIndexToOverride;

        AudioSource _audioSource;

        bool _endOnSequence;

        AnimationSequence _sequenceToChange;

        bool _sequenceIsChanged;

        protected override void Start()
        {
            SetUpAndResetOverrider();

            if (_playSquenceOnStart)
                _ThisAnimator.SetTrigger("Start");

            base.Start();


            _audioSource = GetComponent<AudioSource>();
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

        void SetUpAndResetOverrider()
        {

            _animatorOverride = new AnimatorOverrideController(_animatorController);
            _ThisAnimator.runtimeAnimatorController = _animatorOverride;

            _originalAnimations.Clear();

            foreach (var animation in _animatorOverride.animationClips)
                _originalAnimations.Add(animation);
        }

        void OverrideNoneCurrentStates()
        {

            List<KeyValuePair<AnimationClip, AnimationClip>> animtionsToOverride =
                new List<KeyValuePair<AnimationClip, AnimationClip>>();

            if (_currAnimIndex >= _animationSequence.Objs.Count)
            {
                if (_sequenceToChange)
                {

                    _animationSequence = _sequenceToChange;
                    _sequenceIsChanged = true;

                    _sequenceToChange = null;
                }
                else
                {
                    _sequenceIsChanged = false;
                }

                _currAnimIndex = 0;
            }

            var currAnimIndex = _currAnimIndex;

            string animName = _animationSequence.Objs[_currAnimIndex].Name;

            SendAnimNameCommand(animName);

            animtionsToOverride.Add(new KeyValuePair<AnimationClip, AnimationClip>(
            _originalAnimations[_animIndexToOverride],
            _animationSequence.Objs[_currAnimIndex].AnimClipToPlay()));

            _animIndexToOverride = _animIndexToOverride == 0 ? 1 : 0;

            if (!_animationSequence.Objs[_currAnimIndex].LoopThisSequence)
            {

                if ((_currAnimIndex + 1) < _animationSequence.Objs.Count)
                {
                    animtionsToOverride.Add(new KeyValuePair<AnimationClip, AnimationClip>(
                    _originalAnimations[_animIndexToOverride],
                    _animationSequence.Objs[_currAnimIndex + 1].AnimClipToPlay()));
                }

                _currAnimIndex++;
            }
            else
            {
                animtionsToOverride.Add(new KeyValuePair<AnimationClip, AnimationClip>(
                _originalAnimations[_animIndexToOverride],
                _animationSequence.Objs[_currAnimIndex].AnimClipToPlay()));
            }


            _animationSequence.Objs[currAnimIndex].PlayAudioClip(_audioSource, currAnimIndex);

            if (_currAnimIndex >= _animationSequence.Objs.Count)
            {
                if (_sequenceToChange)
                {
                    _animationSequence = _sequenceToChange;
                    _sequenceIsChanged = true;

                    _sequenceToChange = null;
                }
                else
                {
                    _sequenceIsChanged = false;
                }
                _currAnimIndex = 0;

                animtionsToOverride.Add(new KeyValuePair<AnimationClip, AnimationClip>(
                _originalAnimations[_animIndexToOverride],
                _animationSequence.Objs[_currAnimIndex].AnimClipToPlay()));

                _endOnSequence = true;
            }

            _animatorOverride.ApplyOverrides(animtionsToOverride);
        }

        void ChangeAnimationStateCommand()
        {
            _ThisAnimator.SetTrigger("ChangeState");
            InvokeCommand(0);
        }

        void ChangeAnimationSequenceCommand(AnimationSequence animationSequence)
        {
            if (_animationSequence.Objs[_currAnimIndex].LoopThisSequence)
                _currAnimIndex++;

            _sequenceToChange = animationSequence;

            InvokeCommand(1);
        }

        void ChangeAnimationSequenceAndStateCommand(AnimationSequence animationSequence)
        {
            ChangeAnimationSequenceCommand(animationSequence);
            _ThisAnimator.SetTrigger("ChangeState");

            InvokeCommand(2);
        }

        void ChangeAndOverideAnimationSequenceCommand(AnimationSequence animationSequence)
        {
            if (_currAnimIndex < _animationSequence.Objs.Count - 1)
            {
                _currAnimIndex++;
                _loopSequence = false;

                _sequenceToChange = animationSequence;
            }
            else
            {
                _currAnimIndex = 0;
                _animationSequence = animationSequence;
            }

            InvokeCommand(3);
        }

        void OnChangedAnimClipCommand(AnimationClip clip)
        {
            InvokeCommand(4, clip);
        }

        void TurnOffSequenceLoopCommand()
        {
            _loopSequence = false;

            InvokeCommand(5);
        }

        void TurnOnSequenceLoopCommand()
        {
            _loopSequence = true;

            InvokeCommand(6);
        }


        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) ChangeAnimationStateCommand();
            if (methodNumb == 1) ChangeAnimationSequenceCommand((AnimationSequence)passedObj);
            if (methodNumb == 2) ChangeAnimationSequenceAndStateCommand((AnimationSequence)passedObj);
            if (methodNumb == 3) ChangeAndOverideAnimationSequenceCommand((AnimationSequence)passedObj);
            if (methodNumb == 5) TurnOffSequenceLoopCommand();
            if (methodNumb == 6) TurnOnSequenceLoopCommand();
            if (methodNumb == 7) EndOfSequenceCommand();
            if (methodNumb == 8) RestartAnimationSequenceCommand();
            if (methodNumb == 9) PauseAnimationSequenceCommand();
            if (methodNumb == 10) ResumeAnimationSequenceCommand();
            if (methodNumb == 11) ChangeCurrAnimIndexCommand((int)passedObj);
            if (methodNumb == 13) TurnOffCurrAnimationLoopingCommand();
        }

        protected override void OnNextStateClip(AnimationClip nextClip)
        {
            OverrideNoneCurrentStates();
        }

        void ResetOverrider()
        {
            _animatorOverride = new AnimatorOverrideController(_animatorController);
            _ThisAnimator.runtimeAnimatorController = _animatorOverride;
        }

        void EndOfSequenceCommand()
        {
            InvokeCommand(7);
        }

        protected override void OnEndState()
        {
            if (!_endOnSequence)
                return;

            if (!_loopSequence && !_sequenceIsChanged)
            {
                _ThisAnimator.speed = 0;
                _sequenceIsChanged = true;
            }

        }

        public void RestartAnimationSequenceCommand()
        {

            _endOnSequence = false;
            _currAnimIndex = 0;
            _animIndexToOverride = _animIndexToOverride == 0 ? 1 : 0;

            _ThisAnimator.Play(_nextState, 0, 0);
            _audioSource.Stop();
        }

        public void PauseAnimationSequenceCommand()
        {
            _ThisAnimator.speed = 0;
            _audioSource.Pause();
        }

        public void ResumeAnimationSequenceCommand()
        {
            _ThisAnimator.SetTrigger("Start");
            _audioSource.Play();
            _ThisAnimator.speed = 1;
        }


        void ChangeCurrAnimIndexCommand(int index)
        {
            _currAnimIndex = index;
        }

        void SendAnimNameCommand(string animName)
        {
            InvokeCommand(12, animName);
        }

        void TurnOffCurrAnimationLoopingCommand()
        {
            _animationSequence.Objs[_currAnimIndex].LoopThisSequence = false;
        }
    }
}
