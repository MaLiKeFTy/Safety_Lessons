using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Transforms
{
    public class TransformResetter : MonoService
    {

        void ResetPositionCommand()
        {
            transform.localPosition = Vector3.zero;
            InvokeCommand(0);
        }

        void ResetRotationCommand()
        {
            transform.rotation = Quaternion.identity;
            InvokeCommand(1);
        }

        void ResetScaleCommand()
        {
            transform.localScale = Vector3.one;
            InvokeCommand(2);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) ResetPositionCommand();
            if (methodNumb == 1) ResetRotationCommand();
            if (methodNumb == 2) ResetScaleCommand();
        }
    }
}
