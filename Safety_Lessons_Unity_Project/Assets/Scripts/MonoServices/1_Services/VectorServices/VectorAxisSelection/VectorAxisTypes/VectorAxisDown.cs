using UnityEngine;

namespace MonoServices.Vectors
{
    public class VectorAxisDown : VectorAxis
    {
        public override VectorAxisEnum VectorAxisEnum => VectorAxisEnum.Down;

        public override Vector3 SelectedVectorAxis() => Vector3.down;
    }
}
