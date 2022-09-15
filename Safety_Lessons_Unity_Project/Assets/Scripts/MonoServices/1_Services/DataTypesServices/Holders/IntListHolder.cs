using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Holders
{
    public class IntListHolder : MonoService
    {


        [SerializeField] int[] _intsToHold;


        void SendIntListCommand()
        {
            InvokeCommand(0, _intsToHold);
        }



        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            SendIntListCommand();
        }

    }
}
