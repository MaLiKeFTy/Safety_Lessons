using MonoServices.Core;

namespace MonoServices.MonoUI
{
    public sealed class UiOriginalRectPostionGetter : UiMonoService
    {
        void GetOriginalRectPositionCommand()
        {
            var originalPos = _ThisRectTransform.anchoredPosition;

            InvokeCommand(0, originalPos);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            GetOriginalRectPositionCommand();
        }
    }
}
