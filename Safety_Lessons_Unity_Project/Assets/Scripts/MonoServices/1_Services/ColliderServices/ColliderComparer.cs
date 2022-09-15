using MonoServices.Core;
using UnityEngine;


namespace MonoServices.Colliders
{
    [RequireComponent(typeof(Collider))]
    public class ColliderComparer : ColliderMonoService
    {

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            CompareColliderCommand((Collider)passedObj);
        }

        void CompareColliderCommand(Collider colliderToCompare)
        {
            if (_ThisCollider == colliderToCompare)
                OnSameColliderCommand();
            else
                OnDifferentColliderCommand();
        }

        void OnSameColliderCommand()
        {
            InvokeCommand(1);
        }

        void OnDifferentColliderCommand()
        {
            InvokeCommand(2);
        }

    }
}


