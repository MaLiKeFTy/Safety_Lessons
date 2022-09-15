using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SimpleAudioSourcePlayer : MonoService
    {
        AudioSource _audioSource;
        [SerializeField] AudioClip _audioClip;
        protected override void Awake()
        {
            base.Awake();

            _audioSource = GetComponent<AudioSource>();
        }

        void PlaySoundCommand()
        {
            _audioSource.clip = _audioClip;
            _audioSource.Play();
            InvokeCommand(0);
        }

        void StopSoundCommand()
        {
            _audioSource.Stop();
            InvokeCommand(1);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) PlaySoundCommand();
            if (methodNumb == 1) StopSoundCommand();
        }
    }

}