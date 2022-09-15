using MonoServices.Core;
using UnityEngine;
using UnityEngine.UI;

namespace MonoServices.MonoUI
{
    [RequireComponent(typeof(Graphic))]
    public sealed class UiColorGetter : UiGraphicMonoService
    {
        void GetColorCommand() =>
            InvokeCommand(0, _ThisGraphic.color);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) GetColorCommand();
        }
    }
}
