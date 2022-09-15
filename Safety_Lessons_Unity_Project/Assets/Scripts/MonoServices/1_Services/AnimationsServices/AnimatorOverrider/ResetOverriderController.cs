using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Animations
{
    public class ResetOverriderController : MonoService
    {
        [SerializeField] AnimatorOverrideController _animatorOverrideController;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0)
                ResetOverrideAnimationPairCommand((KeyValuePair<AnimationClip, AnimationClip>)passedObj);
            else
                ResetOverriderCommand();
        }

        void ResetOverrideAnimationPairCommand(KeyValuePair<AnimationClip, AnimationClip> clip)
        {
            var overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>
                (_animatorOverrideController.overridesCount);

            _animatorOverrideController.GetOverrides(overrides);

            for (int i = 0; i < overrides.Count; i++)
            {
                var isKeyPairSame = overrides[i].Value == clip.Value && overrides[i].Key == clip.Key;

                if (isKeyPairSame)
                    overrides[i] = new KeyValuePair<AnimationClip, AnimationClip>(overrides[i].Key, null);
            }

            _animatorOverrideController.ApplyOverrides(overrides);
        }

        void ResetOverriderCommand()
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