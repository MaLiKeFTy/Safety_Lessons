using MonoServices.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MonoServices.MonoUI
{
    public class UiClickerHandler : UiGraphicMonoService,
        IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] bool _canCall = true;

        bool _isBeingHeld;

        public void OnPointerDown(PointerEventData eventData) =>
            OnMouseDownCommand();

        public void OnPointerUp(PointerEventData eventData) =>
            OnMouseUpCommand();

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 2) ChangeCanCallCommand((bool)passedObj);
        }

        void OnMouseDownCommand()
        {
            _isBeingHeld = true;
            ActivateCoroutine(OnClickHold());

            InvokeCommand(0);
        }

        void OnMouseUpCommand()
        {
            _isBeingHeld = false;
            if (_canCall)
                InvokeCommand(1);
        }

        void ChangeCanCallCommand(bool toggle) =>
            _canCall = toggle;

        void OnMouseHoldCommand()
        {
            InvokeCommand(3);
        }

        IEnumerator OnClickHold()
        {
            while (_isBeingHeld)
            {
                OnMouseHoldCommand();
                yield return null;
            }
        }

    }
}
