using MonoServices.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MonoServices.Colliders
{
    public class ColliderClicker : ColliderMonoService
    {
        [SerializeField] bool _canClick = true;

        [SerializeField] bool _activateOnHold = true;


        bool _isBeingHeld;

        void OnMouseDown() =>
            OnMouseDownCommand();

        void OnMouseUp() =>
            OnMouseUpCommand();

        void OnMouseDownCommand()
        {
            if (IsClickedOnUi() || !_canClick)
                return;

            _isBeingHeld = true;
            ActivateCoroutine(OnClickHold());
            InvokeCommand(0);
        }

        void OnMouseUpCommand()
        {
            if (IsClickedOnUi() || !_canClick)
                return;


            _isBeingHeld = false;
            InvokeCommand(1);
        }


        void OnMouseHoldCommand() =>
            InvokeCommand(2);

        bool IsClickedOnUi()
        {
            if (!EventSystem.current)
                return false;

            bool isEditorClick = EventSystem.current.IsPointerOverGameObject();
            bool isMobileClick = EventSystem.current.IsPointerOverGameObject(0);

            bool isCLickedOnUI = isMobileClick || isEditorClick;

            return isCLickedOnUI;
        }

        void TurnOnCanClickCommand() =>
            _canClick = true;

        void TurnOffCanClickCommand() =>
            _canClick = false;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 3) TurnOnCanClickCommand();
            if (methodNumb == 4) TurnOffCanClickCommand();
        }

        IEnumerator OnClickHold()
        {
            while (_isBeingHeld & _activateOnHold)
            {
                OnMouseHoldCommand();
                yield return null;
            }
        }

    }

}
