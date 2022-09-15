using MonoServices.Core;
using UnityEngine;

namespace MonoServices.MonoPhysics
{
    public class PhysicsCollisionHandler : PhysicsMonoService
    {
        void OnCollisionEnter(Collision collision)
        {
            if (IsRightTag(collision.transform))
                GetTransCollisionEnterCommand(collision);

        }

        void OnCollisionStay(Collision collision)
        {
            if (IsRightTag(collision.transform))
                GetTransCollisionStayCommand(collision);
        }


        void OnCollisionExit(Collision collision)
        {
            if (IsRightTag(collision.transform))
                GetTransCollisionExitCommand(collision);
        }

        void GetTransCollisionEnterCommand(Collision collision) =>
            InvokeCommand(0, collision.transform);

        void GetTransCollisionStayCommand(Collision collision) =>
            InvokeCommand(1, collision.transform);

        void GetTransCollisionExitCommand(Collision collision) =>
            InvokeCommand(2, collision.transform);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
        }
    }
}
