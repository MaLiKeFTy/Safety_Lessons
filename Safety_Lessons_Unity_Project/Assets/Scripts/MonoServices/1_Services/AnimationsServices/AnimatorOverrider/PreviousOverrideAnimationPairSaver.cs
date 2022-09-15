using MonoServices.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Animations
{
    public class PreviousOverrideAnimationPairSaver : MonoService
    {
        [SerializeField] AnimatorOverrideController _animatorOverrideController;

        KeyValuePair<AnimationClip, AnimationClip> _storedOverride;
        KeyValuePair<AnimationClip, AnimationClip> _nextOverride;
        bool _isOverridesStored;
        bool _gottenNextOverride;

        protected override void Start()
        {
            base.Start();

            var overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>
                (_animatorOverrideController.overridesCount);

            _animatorOverrideController.GetOverrides(overrides);
            _storedOverride = overrides[0];

            _isOverridesStored = true;

            StartCoroutine(StoreNextClipCheck());
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0)
                GetStoredOverrideKeyPairCommand();
            else if (methodNumb == 1)
                SetNextOverrideKeyPairCommand((KeyValuePair<AnimationClip, AnimationClip>)passedObj);
            else if (methodNumb == 2)
                ChangeBoolCommand((bool)passedObj);
        }

        void GetStoredOverrideKeyPairCommand()
        {
            if (!_isOverridesStored)
                return;

            InvokeCommand(0, _storedOverride);
            _isOverridesStored = false;
        }

        void SetNextOverrideKeyPairCommand(KeyValuePair<AnimationClip, AnimationClip> overrides)
        {
            if (!_gottenNextOverride)
                _nextOverride = overrides;

            _gottenNextOverride = true;
        }

        void ChangeBoolCommand(bool clipstored)
        {
            _isOverridesStored = clipstored;
        }

        IEnumerator StoreNextClipCheck()
        {
            while (true)
            {
                if (!_isOverridesStored)
                {
                    _storedOverride = _nextOverride;
                    _isOverridesStored = true;
                    _gottenNextOverride = false;
                }

                yield return null;
            }
        }
    }
}