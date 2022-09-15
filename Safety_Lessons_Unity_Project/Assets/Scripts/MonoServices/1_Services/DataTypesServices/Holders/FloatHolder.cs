using UnityEngine;
using MonoServices.Core;

namespace MonoServices.Holders
{
    public class FloatHolder : MonoService
    {
        [SerializeField] float _floatAmount;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
            GetFloatAmountCommand();

        void GetFloatAmountCommand() =>
            InvokeCommand(0, _floatAmount);

    }
}
