using UnityEngine;

namespace MonoServices.Sound
{
    [System.Serializable]

    public class AudioClipHolder
    {
        [SerializeField] AudioClip _audioClip;
        [SerializeField] bool _loop;
        [SerializeField] bool _useAudioClipVolume = true;
        [SerializeField, Range(0, 1)] float _volume = 1;

        public AudioClip AudioClip => _audioClip;
        public bool Loop => _loop;
        public float Volume => _volume;
        public bool UseAudioClipVolume => _useAudioClipVolume;
    }

}
