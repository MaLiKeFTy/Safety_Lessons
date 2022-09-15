using MonoServices.Core;
using UnityEngine;
using UnityEngine.UI;

namespace MonoServices.MonoUI
{
    [RequireComponent(typeof(RawImage))]
    public sealed class UiRawImageTextureGetter : UiMonoService
    {
        void GetTextureCommand()
        {
            var texture = GetComponent<RawImage>().texture;
            InvokeCommand(0, texture);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
            GetTextureCommand();
    }
}
