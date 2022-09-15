using System.Collections.Generic;
using UnityEngine;
using MonoServices.Core;

namespace MonoServices.Animations
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorOverrider : MonoService
    {
        [SerializeField] AnimatorOverrideController _animatorOverrideController;
        [SerializeField] List<AnimatorOverrideHolder> _animatorOverrideHolders;

        Animator _thisAnimator;
        AnimationClip _clip;
        AnimatorOverrideHolder _holderToOverride;

        protected override void Awake()
        {
            base.Awake();

            _thisAnimator = GetComponent<Animator>();
        }
            
        protected override void OnValidate()
        {
            base.OnValidate();

            AnimatorOverridHolderPopulator.PopulateHoldersAnimations
                (_animatorOverrideController, _animatorOverrideHolders);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            ResetOverrider();
        }

        void OverrideAnimatorCommand(AnimationClip clip)
        {
            if (!(_clip = clip)) return;

            var overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>
                (_animatorOverrideController.overridesCount);

            if (_holderToOverride == null) return;

            if (_holderToOverride.OverrideAnimations.Count > 0)
                _holderToOverride.OverrideAnimations.Clear();

            _holderToOverride.OverrideAnimations.Add(clip);

            int randomAnimation = Random.Range(0, _holderToOverride.OverrideAnimations.Count);

            var animKeyPair = new KeyValuePair<AnimationClip, AnimationClip>
                (_holderToOverride.AnimationClip,
                _holderToOverride.OverrideAnimations[randomAnimation]);

            if (animKeyPair.Key && animKeyPair.Value)
                OveriddenKeyPairCommand(animKeyPair);

            overrides.Add(animKeyPair);

            _animatorOverrideController.ApplyOverrides(overrides);
            _thisAnimator.runtimeAnimatorController = _animatorOverrideController;

            _clip = null;
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0)
                OverrideAnimatorCommand((AnimationClip)passedObj);
            else
                AnimationToOverrideCommand((AnimationClip)passedObj);
        }

        void AnimationToOverrideCommand(AnimationClip clip)
        {
            if (clip)
                OverrideAnimatorCommand(_clip);

            foreach (var holder in _animatorOverrideHolders)
            {
                if (holder.AnimationClip == clip)
                {
                    _holderToOverride = holder;
                    return;
                }
            }

            _holderToOverride = null;
        }

        void OveriddenKeyPairCommand(KeyValuePair<AnimationClip, AnimationClip> overrides) =>
            InvokeCommand(2, overrides);

        void ResetOverrider()
        {
            var overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>
                        (_animatorOverrideController.overridesCount);

            _animatorOverrideController.GetOverrides(overrides);

            for (int i = 0; i < overrides.Count; i++)
                overrides[i] = new KeyValuePair<AnimationClip, AnimationClip>(overrides[i].Key, null);

            _animatorOverrideController.ApplyOverrides(overrides);
        }
    }
}
