using System.Collections;
using UnityEngine;

namespace MonoServices.Sound
{
    public class SoundPlayOneShopOption : SoundPlayOption
    {
        public override SoundPlayOptionsEnum SoundType => SoundPlayOptionsEnum.OneShot;

        public override IEnumerator PlaySoundOption(AudioSource audioSource, SoundGroup soundGroup)
        {

            foreach (var clip in soundGroup.GroupSounds)
                audioSource.PlayOneShot(clip);

            yield return null;
        }
    }
}