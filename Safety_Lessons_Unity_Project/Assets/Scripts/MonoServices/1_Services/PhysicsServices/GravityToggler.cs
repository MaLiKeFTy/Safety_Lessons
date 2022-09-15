using MonoServices.Core;
using UnityEngine;

namespace MonoServices.MonoPhysics
{
    [RequireComponent(typeof(Rigidbody))]
    public class GravityToggler : PhysicsMonoService
    {
        Rigidbody _thisRigidbody;
        protected override void Awake()
        {
            base.Awake();
            _thisRigidbody = GetComponent<Rigidbody>();
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0)
                ApplyGravityCommand();
            else
                RemoveGravityCommand();
        }

        void ApplyGravityCommand() =>
            _thisRigidbody.useGravity = true;

        void RemoveGravityCommand() =>
            _thisRigidbody.useGravity = false;
    }
}
