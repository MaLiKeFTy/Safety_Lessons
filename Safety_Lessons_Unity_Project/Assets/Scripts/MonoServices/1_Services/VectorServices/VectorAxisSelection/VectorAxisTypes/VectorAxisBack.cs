using UnityEngine;

namespace MonoServices.Vectors
{
    public class VectorAxisBack : VectorAxis
    {
        public override VectorAxisEnum VectorAxisEnum => VectorAxisEnum.Back;

        public override Vector3 SelectedVectorAxis() => Vector3.back;
    }
}