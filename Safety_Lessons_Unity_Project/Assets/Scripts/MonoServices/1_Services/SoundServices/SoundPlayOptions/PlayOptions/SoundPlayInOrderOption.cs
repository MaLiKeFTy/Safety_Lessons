using System.Collections;
using UnityEngine;

namespace MonoServices.Sound
{
    public class SoundPlayInOrderOption : SoundPlayOption
    {
        public override SoundPlayOptionsEnum SoundType => SoundPlayOptionsEnum.InOrder;

        public override IEnumerator PlaySoundOption(AudioSource audioSource, SoundGroup soundGroup)
        {
            for (int i = 0; i < soundGroup.GroupSounds.Count; i++)
            {
                audioSource.clip = soundGroup.GroupSounds[i];
                audioSource.Play();

                yield return new WaitWhile(() => audioSource.isPlaying);
            }
        }
    }
}