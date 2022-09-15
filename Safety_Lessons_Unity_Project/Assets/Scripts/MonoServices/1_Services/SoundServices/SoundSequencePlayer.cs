using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundSequencePlayer : MonoService
    {

        [SerializeField] SoundsSO _soundsSO;

        AudioSource _thisAudioSource;

        protected override void Awake()
        {
            base.Awake();

            _thisAudioSource = GetComponent<AudioSource>();
        }

        void PlaySoundSequenceCommand()
        {
            ActivateCoroutine(PlayingSoundsSequence());
        }

        IEnumerator PlayingSoundsSequence()
        {
            foreach (var audioClipHolder in _soundsSO.AudioClipHolders)
            {
                _thisAudioSource.clip = audioClipHolder.AudioClip;
                _thisAudioSource.loop = audioClipHolder.Loop;

                if (!audioClipHolder.UseAudioClipVolume)
                    _thisAudioSource.volume = audioClipHolder.Volume;

                _thisAudioSource.Play();

                yield return new WaitWhile(() => _thisAudioSource.isPlaying);
            }


            yield return null;
        }

        void StopSoundSequenceCommand()
        {
            _thisAudioSource.clip = null;
        }

        void ChangeSoundsHolderCommand(SoundsSO soundsSO)
        {
            _soundsSO = soundsSO;
        }

        void ChangeAndPlaySoundsHolderCommand(SoundsSO soundsSO)
        {

            _soundsSO = soundsSO;

            PlaySoundSequenceCommand();
        }


        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) PlaySoundSequenceCommand();
            if (methodNumb == 1) StopSoundSequenceCommand();

            if (!(passedObj is SoundsSO))
            {
                StopSoundSequenceCommand();
                return;
            }

            if (methodNumb == 2) ChangeSoundsHolderCommand((SoundsSO)passedObj);
            if (methodNumb == 3) ChangeAndPlaySoundsHolderCommand((SoundsSO)passedObj);

        }
    }
}