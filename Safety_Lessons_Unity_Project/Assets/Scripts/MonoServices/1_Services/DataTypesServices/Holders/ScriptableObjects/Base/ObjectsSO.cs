using UnityEngine;

namespace MonoServices.ObjectsSO
{
    public class ObjectsSO<T> : ScriptableObject
    {
        [SerializeField] T[] _objs;

        public T[] Objs => _objs;
    }

}