using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Sound
{
    [CreateAssetMenu(fileName = "SoundSO", menuName = "SoundSO")]
    public class SoundScriptableObject : ScriptableObject
    {
        public List<SoundGroup> soundGroups;
    }
}