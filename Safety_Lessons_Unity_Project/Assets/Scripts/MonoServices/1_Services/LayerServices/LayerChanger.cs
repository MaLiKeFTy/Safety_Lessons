using MonoServices.Core;

namespace MonoServices.Layers
{
    public class LayerChanger : MonoService
    {
        void ChangeLayerCommand(int parameterIndex)
        {
            if (gameObject.layer == parameterIndex)
                return;

            gameObject.layer = parameterIndex;
            InvokeCommand(0);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
            ChangeLayerCommand(0);
    }
}