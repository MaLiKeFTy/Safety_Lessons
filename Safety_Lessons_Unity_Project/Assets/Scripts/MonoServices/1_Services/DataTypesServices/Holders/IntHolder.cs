using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Holders
{
    public class IntHolder : MonoService
    {
        [SerializeField] int _intToHold;

        void SetIntToHoldCommand(int intHold)
        {
            _intToHold = intHold;
        }

        void SendIntToHoldCommand()
        {
            InvokeCommand(1, _intToHold);
        }

        void SetAndSendIntToHoldCommand(int intHold)
        {
            SetIntToHoldCommand(intHold);
            SendIntToHoldCommand();
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) SetIntToHoldCommand((int)passedObj);
            if (methodNumb == 1) SendIntToHoldCommand();
            if (methodNumb == 2) SetAndSendIntToHoldCommand((int)passedObj);
        }
    }
}
