using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Animations
{
    [System.Serializable]
    public class AnimatorOverrideHolder
    {
        [SerializeField] AnimationClip _animationClip;
        [SerializeField] List<AnimationClip> _overrideAnimations = new List<AnimationClip>();

        public List<AnimationClip> OverrideAnimations => _overrideAnimations;
        public AnimationClip AnimationClip { get => _animationClip; set => _animationClip = value; }
    }
}