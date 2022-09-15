using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Sound
{
    public class SoundPlayRandomOption : SoundPlayOption
    {
        public override SoundPlayOptionsEnum SoundType => SoundPlayOptionsEnum.Random;

        public override IEnumerator PlaySoundOption(AudioSource audioSource, SoundGroup soundGroup)
        {
            List<int> RandomOrder = RandomizeList(soundGroup);

            for (int i = 0; i < soundGroup.GroupSounds.Count; i++)
            {
                audioSource.clip = soundGroup.GroupSounds[RandomOrder[i]];
                audioSource.Play();

                yield return new WaitWhile(() => audioSource.isPlaying);
            }
        }

        List<int> RandomizeList(SoundGroup soundGroup)
        {
            List<int> randNums = new List<int>();

            randNums.Add(Random.Range(0, soundGroup.GroupSounds.Count));

            while (randNums.Count != soundGroup.GroupSounds.Count)
            {
                int rand = Random.Range(0, soundGroup.GroupSounds.Count);

                if (!randNums.Contains(rand))
                    randNums.Add(rand);
            }

            return randNums;
        }
    }

}