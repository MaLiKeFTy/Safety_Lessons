using MonoServices.Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MonoServices.MonoUI
{
    public sealed class UiDragHandler : UiGraphicMonoService,
        IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        bool _canDrag = true;

        public void OnBeginDrag(PointerEventData eventData) =>
            OnBeginDragCommand();

        public void OnDrag(PointerEventData eventData) =>
            OnDragCommand(eventData.delta);

        public void OnEndDrag(PointerEventData eventData) =>
            OnEndDragCommand();

        void OnBeginDragCommand() =>
            InvokeCommand(0);

        void OnDragCommand(Vector3 position)
        {
            if (_canDrag)
                InvokeCommand(1, position);
        }

        void OnEndDragCommand() =>
            InvokeCommand(2);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 3) ChangeCanDragCommand((bool)passedObj);
        }

        void ChangeCanDragCommand(bool toggle) =>
            _canDrag = toggle;

    }
}
