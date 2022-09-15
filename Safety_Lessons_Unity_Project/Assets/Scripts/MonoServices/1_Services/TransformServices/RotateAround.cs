using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Transforms
{
    public class RotateAround : MonoService
    {

        [SerializeField] Transform _target;
        [SerializeField] float angle = 2;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
        }

        void FixedUpdate()
        {
            transform.RotateAround(_target.position, Vector3.up, angle * 10 * Time.deltaTime);
        }



    }

}
