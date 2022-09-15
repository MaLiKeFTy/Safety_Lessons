using System;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Sound
{
    [Serializable]
    public class SoundGroup
    {
        [SerializeField] string groupName;
        [SerializeField] List<AudioClip> groupSounds;
        [SerializeField] SoundPlayOptionsEnum soundType;
        [SerializeField] bool isLooping;

        public string GroupName => groupName;
        public List<AudioClip> GroupSounds => groupSounds;
        public SoundPlayOptionsEnum TypeOfSound => soundType;
        public bool IsLooping => isLooping;
    }
}