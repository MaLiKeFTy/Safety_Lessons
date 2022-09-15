using UnityEngine;

namespace MonoServices.Vectors
{
    public class VectorAxisForward : VectorAxis
    {
        public override VectorAxisEnum VectorAxisEnum => VectorAxisEnum.Forward;

        public override Vector3 SelectedVectorAxis() => Vector3.forward;
    }
}