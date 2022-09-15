using UnityEngine;

namespace MonoServices.MonoPhysics
{
    public class PhysicsHelper : MonoBehaviour
    {
        public static Vector3 VelocityVectorCalculator(Vector3 startpos, Vector3 endpos, float time)
        {
            var distance = endpos - startpos;
            var velocity = distance / time;

            return velocity;
        }
    }
}