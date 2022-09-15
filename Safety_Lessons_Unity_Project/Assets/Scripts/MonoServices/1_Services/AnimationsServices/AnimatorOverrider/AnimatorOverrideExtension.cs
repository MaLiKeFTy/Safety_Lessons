using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Animations
{
    public static class AnimatorOverrideExtension
    {
        public static List<AnimationClip> GetAllAnimations(this AnimatorOverrideController animatorOverrider)
        {
            var overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>(animatorOverrider.overridesCount);
            animatorOverrider.GetOverrides(overrides);
            for (int i = 0; i < overrides.Count; ++i)
                overrides[i] = new KeyValuePair<AnimationClip, AnimationClip>(overrides[i].Key, null);

            List<AnimationClip> tempAnimList = new List<AnimationClip>();

            foreach (var overrider in overrides)
                tempAnimList.Add(overrider.Key);

            return tempAnimList;
        }
    }
}