using MonoServices.Core;
using UnityEngine;

namespace MonoServices.MonoPhysics
{
    public class PhysicsTriggerer : PhysicsMonoService
    {

        [SerializeField] bool _callOnEnter;
        [SerializeField] bool _callOnStay;
        [SerializeField] bool _callOnExit;

        void OnTriggerEnter(Collider other)
        {
            if (!_callOnEnter)
                return;

            if (IsRightTag(other.transform))
                GetTransTriggerEnterCommand(other);
        }

        void OnTriggerStay(Collider other)
        {
            if (!_callOnStay)
                return;

            if (IsRightTag(other.transform))
                GetTransTriggerStayCommand(other);
        }

        void OnTriggerExit(Collider other)
        {
            if (!_callOnExit)
                return;

            if (IsRightTag(other.transform))
                GetTransTriggerExitCommand(other);
        }

        void GetTransTriggerEnterCommand(Collider other)
        {
            InvokeCommand(0, other.transform);
        }


        void GetTransTriggerStayCommand(Collider other) =>
            InvokeCommand(1, other.transform);

        void GetTransTriggerExitCommand(Collider other)
        {
            InvokeCommand(2, other.transform);
        }


        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
        }
    }
}
