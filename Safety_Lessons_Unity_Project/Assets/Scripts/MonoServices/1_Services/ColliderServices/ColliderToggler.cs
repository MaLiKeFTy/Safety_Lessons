using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Colliders
{
    public class ColliderToggler : ColliderMonoService
    {
        void EnableColliderCommand()
        {
            _ThisCollider.enabled = true;
            InvokeCommand(0);
        }

        void DisableColliderCommand()
        {
            _ThisCollider.enabled = false; 
            InvokeCommand(1);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) EnableColliderCommand();
            if (methodNumb == 1) DisableColliderCommand();
        }

    }
}
