using MonoServices.Core;
using UnityEngine;

namespace MonoServices.MonoPhysics
{
    public class RaycastInteractionObject : MonoService
    {
        [SerializeField] bool _callOnEnter = true;
        [SerializeField] bool _callOnStay;
        [SerializeField] bool _callOnExit;

        bool _alreadyEntered;

        public void OnRaycastEnterCommand()
        {
            if (!_callOnEnter || _alreadyEntered)
                return;

            InvokeCommand(0);
            _alreadyEntered = true;
        }


        public void OnRaycastStayCommand()
        {
            if (_callOnStay)
                InvokeCommand(1);
        }


        public void OnRaycastExitCommand()
        {
            if (!_callOnExit)
                return;
            InvokeCommand(2);
            _alreadyEntered = false;
        }

        void ResetAlreadyCalledCommand()
        {
            InvokeCommand(3);
            _alreadyEntered = false;
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 3) ResetAlreadyCalledCommand();
        }

    }
}
