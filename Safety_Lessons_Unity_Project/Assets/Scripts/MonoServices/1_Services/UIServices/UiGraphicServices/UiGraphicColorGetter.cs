using MonoServices.Core;

namespace MonoServices.MonoUI
{
    public sealed class UiGraphicColorGetter : UiGraphicMonoService
    {
        void GetGraphicColorCommand() =>
            InvokeCommand(0, _ThisGraphic.color);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
            GetGraphicColorCommand();
    }
}
