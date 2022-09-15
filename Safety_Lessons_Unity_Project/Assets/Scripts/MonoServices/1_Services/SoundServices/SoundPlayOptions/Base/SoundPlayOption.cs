using System.Collections;
using UnityEngine;

namespace MonoServices.Sound
{
    public abstract class SoundPlayOption
    {
        public abstract SoundPlayOptionsEnum SoundType { get; }

        public abstract IEnumerator PlaySoundOption(AudioSource audioSource, SoundGroup soundGroup);
    }
}