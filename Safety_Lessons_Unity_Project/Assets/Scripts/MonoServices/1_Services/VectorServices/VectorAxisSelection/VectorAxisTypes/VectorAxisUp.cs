using UnityEngine;

namespace MonoServices.Vectors
{
    public class VectorAxisUp : VectorAxis
    {
        public override VectorAxisEnum VectorAxisEnum => VectorAxisEnum.Up;

        public override Vector3 SelectedVectorAxis() => Vector3.up;
    }
}