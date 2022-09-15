using UnityEngine;

namespace MonoServices.Vectors
{
    public class VectorAxisLeft : VectorAxis
    {
        public override VectorAxisEnum VectorAxisEnum => VectorAxisEnum.Left;

        public override Vector3 SelectedVectorAxis() => Vector3.left;
    }
}