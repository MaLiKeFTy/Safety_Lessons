using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Transforms
{
    [RequireComponent(typeof(Transform))]
    public class TransformAxisValueGetter : TransformMonoService
    {
        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
            GetTransfromAxisValueCommand();

        void GetTransfromAxisValueCommand() =>
            InvokeCommand(0, _ThisTransform.eulerAngles.y);
    }
}
