using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Animations
{
    [CreateAssetMenu(fileName = "AnimationClipInfo", menuName = "AnimClipInfo")]
    public class AnimationClipInfoSO : ScriptableObject
    {

        [SerializeField] AnimationClip _animClip;
        [SerializeField] List<AnimationEventsSettings> _animClipEventTimes = new List<AnimationEventsSettings>();

        public AnimationClip AnimClip { get => _animClip; set => _animClip = value; }
        public List<AnimationEventsSettings> AnimClipEventTimes => _animClipEventTimes;
    }
}