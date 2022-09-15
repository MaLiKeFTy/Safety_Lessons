using UnityEngine;

namespace MonoServices.Vectors
{
    [System.Serializable]
    public class VectorThreeHolder
    {
        [SerializeField] string _name;
        [SerializeField] Vector3 _vectorToSet;

        public VectorThreeHolder(string name, Vector3 vectorToSet)
        {
            _name = name;
            _vectorToSet = vectorToSet;
        }

        public Vector3 VectorToSet => _vectorToSet;
        public string Name => _name;
    }

}