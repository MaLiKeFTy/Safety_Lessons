using MonoServices.Core;
using UnityEngine;

namespace MonoServices.MonoUI
{
    public sealed class UiRectPositionSetter : UiMonoService
    {
        [SerializeField] Vector3[] _rectPositios;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) GetRectPositionsCommand();
            if (methodNumb == 1) SetRectPositionCommand((Vector3)passedObj);
        }

        void GetRectPositionsCommand() =>
            InvokeCommand(0, _rectPositios);

        void SetRectPositionCommand(Vector3 currRectPos) =>
            InvokeCommand(1, currRectPos);
    }
}
