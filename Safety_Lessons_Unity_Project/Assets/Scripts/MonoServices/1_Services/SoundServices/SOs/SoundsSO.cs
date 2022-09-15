using UnityEngine;

namespace MonoServices.Sound
{
    [CreateAssetMenu(fileName = "SoundSO", menuName = "ScriptableObjects/ObjsSO/SoundsSO")]

    public class SoundsSO : ScriptableObject
    {
        [SerializeField] AudioClipHolder[] _audioClipHolders = { new AudioClipHolder() };

        public AudioClipHolder[] AudioClipHolders => _audioClipHolders;
    }
}