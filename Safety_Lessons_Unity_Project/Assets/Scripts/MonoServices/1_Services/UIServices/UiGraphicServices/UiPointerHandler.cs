using MonoServices.Core;
using UnityEngine.EventSystems;

namespace MonoServices.MonoUI
{
    public sealed class UiPointerHandler : UiGraphicMonoService, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData) =>
            OnPointerEnterCommand();

        public void OnPointerExit(PointerEventData eventData) =>
            OnPointerExitCommand();

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
        }

        void OnPointerEnterCommand() =>
            InvokeCommand(0);

        void OnPointerExitCommand() =>
            InvokeCommand(1);

    }
}
