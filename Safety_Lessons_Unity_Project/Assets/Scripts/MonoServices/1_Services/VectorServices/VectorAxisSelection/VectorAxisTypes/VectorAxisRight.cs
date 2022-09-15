using UnityEngine;

namespace MonoServices.Vectors
{
    public class VectorAxisRight : VectorAxis
    {
        public override VectorAxisEnum VectorAxisEnum => VectorAxisEnum.Right;

        public override Vector3 SelectedVectorAxis() => Vector3.right;
    }
}