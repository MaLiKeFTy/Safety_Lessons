using MonoServices.Sound;
using UnityEngine;

namespace MonoServices.Animations
{
    [CreateAssetMenu(fileName = "Sequence")]
    public class AnimationSequence : SequenceOfType<AnimationWithSoundsHolder>
    {

    }


    [System.Serializable]
    public class AnimationWithSoundsHolder
    {

        [SerializeField] string _name;

        [SerializeField] bool _loopThisSequence;

        [Header("Audio Settings")]

        [SerializeField] AnimationWithSounds[] _animationWithSounds;

        public string Name => _name;

        public bool LoopThisSequence { get => _loopThisSequence; set => _loopThisSequence = value; }

        public AnimationClip CurrAnim { get; set; }

        public void PlayAudioClip(AudioSource audioSource, int holderIndex)
        {
            for (int i = 0; i < _animationWithSounds.Length; i++)
            {
                if (CurrAnim == _animationWithSounds[i].AnimClip)
                    _animationWithSounds[i].PlayAudioClip(audioSource);
            }
        }


        public AnimationClip AnimClipToPlay()
        {
            if (_animationWithSounds.Length == 0)
                return null;

            int animClipIndex = Random.Range(0, _animationWithSounds.Length);

            CurrAnim = _animationWithSounds[animClipIndex].AnimClip;

            return _animationWithSounds[animClipIndex].AnimClip;
        }


    }

    [System.Serializable]
    public class AnimationWithSounds
    {

        [SerializeField] AnimationClip _animClip;

        [Header("Audio Settings")]

        [SerializeField] bool _pickRandomAudioClip;
        [SerializeField] AudioClipHolder[] _audioClips = { new AudioClipHolder() };

        public AnimationClip AnimClip => _animClip;

        public void PlayAudioClip(AudioSource audioSource)
        {
            if (_audioClips.Length == 0)
                return;

            int audioClipIndex = 0;

            if (_pickRandomAudioClip)
                audioClipIndex = Random.Range(0, _audioClips.Length);

            AudioClip audioClip = _audioClips[audioClipIndex].AudioClip;
            audioSource.clip = audioClip;
            audioSource.Play();
        }

    }


}