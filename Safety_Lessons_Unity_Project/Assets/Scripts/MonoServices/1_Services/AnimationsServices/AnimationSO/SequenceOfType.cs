using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Animations
{
    [CreateAssetMenu(fileName = "Sequence")]
    public class SequenceOfType<T> : ScriptableObject
    {
        [SerializeField] List<T> _objs = new List<T>();

        public List<T> Objs => _objs;
    }
}