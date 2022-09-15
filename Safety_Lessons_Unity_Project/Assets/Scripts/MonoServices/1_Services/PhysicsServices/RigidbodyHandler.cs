using MonoServices.Core;
using UnityEngine;

namespace MonoServices.MonoPhysics
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyHandler : MonoService
    {
        Rigidbody _thisRigidbody;

        protected override void Awake()
        {
            base.Awake();

            _thisRigidbody = GetComponent<Rigidbody>();
        }

        void ApplyGravityCommand() =>
            _thisRigidbody.useGravity = true;

        void RemoveGravityCommand() =>
            _thisRigidbody.useGravity = false;


        void TurnOnIsKinematicCommand() =>
            _thisRigidbody.isKinematic = true;


        void TurnOffIsKinematicCommand() =>
            _thisRigidbody.isKinematic = false;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) ApplyGravityCommand();
            if (methodNumb == 1) RemoveGravityCommand();
            if (methodNumb == 2) TurnOnIsKinematicCommand();
            if (methodNumb == 3) TurnOffIsKinematicCommand();
        }
    }
}
