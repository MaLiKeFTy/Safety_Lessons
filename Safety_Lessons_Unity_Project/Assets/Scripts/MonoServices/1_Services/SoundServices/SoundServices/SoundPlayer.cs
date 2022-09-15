using System.Collections;
using UnityEngine;
using MonoServices.Core;

namespace MonoServices.Sound
{
    public class SoundPlayer : MonoService
    {
        AudioSource audioSource;
        SoundGroup soundGroup;
        bool loopSounds;
        Coroutine currentlyPlayingCoroutine;

        protected override void Awake()
        {
            base.Awake();

            audioSource = GetComponent<AudioSource>();
        }
            

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0)
                PlaySoundCommand();
            else if (methodNumb == 1)
                StopSoundCommand();
            else
                GetSoundGroupCommand((SoundGroup)passedObj);
        }

        void PlaySoundCommand()
        {
            if (soundGroup.GroupSounds.Count == 0) return;

            loopSounds = soundGroup.IsLooping;

            if (audioSource)
            {
                if (loopSounds)
                {
                    StartCoroutine(LoopingSounds());

                    return;
                }

                var playSoundWithType = SoundFactory.PlaySoundOfType(soundGroup, audioSource, soundGroup.TypeOfSound);

                currentlyPlayingCoroutine = StartCoroutine(playSoundWithType);
            }
        }

        IEnumerator LoopingSounds()
        {
            while (loopSounds)
            {
                var playSoundWithType = SoundFactory.PlaySoundOfType(soundGroup, audioSource, soundGroup.TypeOfSound);

                yield return StartCoroutine(playSoundWithType);
            }
        }

        void StopSoundCommand()
        {
            loopSounds = false;

            if (currentlyPlayingCoroutine != null)
                StopCoroutine(currentlyPlayingCoroutine);

            audioSource.Stop();
        }

        void GetSoundGroupCommand(SoundGroup soundgroup) =>
            soundGroup = soundgroup;
    }
}