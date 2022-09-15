using UnityEngine;
using UnityEngine.Serialization;

namespace MonoServices.Vectors
{
    [System.Serializable]
    public sealed class VectorAxisSelector
    {
        [FormerlySerializedAs("vectorAxis")]
        [SerializeField] VectorAxisEnum _vectorAxis;

        public Vector3 SelectedVectorAxis() => VectorAxisProcessor.GetAxisTarget(_vectorAxis);
    }
}