using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Animations
{
    public static class AnimatorOverridHolderPopulator
    {
        public static void PopulateHoldersAnimations
            (AnimatorOverrideController animatorOverride,
            List<AnimatorOverrideHolder> animatorOverrideHolders)
        {
            if (!animatorOverride)
            {
                animatorOverrideHolders.Clear();
                return;
            }

            var furtherPopulate = animatorOverrideHolders.Count != animatorOverride.overridesCount;

            var overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>
                (animatorOverride.overridesCount);

            animatorOverride.GetOverrides(overrides);

            foreach (var over in overrides)
            {
                AnimatorOverrideHolder animationHolder = new AnimatorOverrideHolder();

                animationHolder.AnimationClip = over.Key;

                if (furtherPopulate)
                {
                    animationHolder.OverrideAnimations.Add(over.Value);
                    animatorOverrideHolders.Add(animationHolder);
                }
            }

            for (int i = 0; i < overrides.Count; i++)
            {
                foreach (var holder in animatorOverrideHolders)
                {
                    if (overrides[i].Key == holder.AnimationClip)
                        if (holder.OverrideAnimations.Count > 0)
                            overrides[i] = new KeyValuePair<AnimationClip, AnimationClip>(holder.AnimationClip, holder.OverrideAnimations[0]);
                }
            }

            animatorOverride.ApplyOverrides(overrides);
        }
    }
}