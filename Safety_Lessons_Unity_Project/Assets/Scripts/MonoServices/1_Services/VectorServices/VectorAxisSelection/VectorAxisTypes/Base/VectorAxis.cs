using UnityEngine;

namespace MonoServices.Vectors
{
    public abstract class VectorAxis
    {
        public abstract VectorAxisEnum VectorAxisEnum { get; }

        public abstract Vector3 SelectedVectorAxis();
    }
}