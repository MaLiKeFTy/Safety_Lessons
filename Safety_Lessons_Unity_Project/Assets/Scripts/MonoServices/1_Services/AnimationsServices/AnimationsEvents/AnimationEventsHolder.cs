using System;
using UnityEngine;

namespace MonoServices.Animations
{
    [Serializable]
    public class AnimationEventsHolder
    {
        [SerializeField] AnimationClip _anim;
        [SerializeField] AnimationEventsSettings[] _events;

        public AnimationClip Anim => _anim;
        public AnimationEventsSettings[] Events => _events;
    }

    [Serializable]
    public class AnimationEventsSettings
    {
        [SerializeField] string _eventName;

        [Tooltip("Event Time in seconds")]
        [SerializeField] float _eventTime;

        public float EventTime => _eventTime;
        public string EventName => _eventName;
    }
}