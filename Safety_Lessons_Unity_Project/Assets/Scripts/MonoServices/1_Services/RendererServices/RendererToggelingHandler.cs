using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Rendering
{
    [RequireComponent(typeof(Renderer))]
    public class RendererToggelingHandler : RendererMonoService
    {
        void EnableRendererCommand()
        {
            _ThisRenderer.enabled = true;
        }

        void DisableRendererCommand()
        {
            _ThisRenderer.enabled = false;
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) EnableRendererCommand();
            if (methodNumb == 1) DisableRendererCommand();
        }

    }
}


