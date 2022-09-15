using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MonoServices.MonoUI
{
    public sealed class UIHandleButtonState : UiMonoService
    {
        [SerializeField] GameObject _passedButton;

        List<GameObject> _buttons = new List<GameObject>();

        void SetButtonsActiveCommand(object obj, int paramIndex)
        {
            if (_passedButton == null)
            {
                try
                {
                    _passedButton = (GameObject)obj;
                }
                catch
                {
                    _passedButton = null;
                }
            }

            if (_passedButton != null)
                SingleButton(paramIndex);

            if (_buttons.Count > 0)
                MultipleButtons(paramIndex);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0)
                SetButtonsActiveCommand(passedObj, 0);
            else
                PopulateListCommand((List<GameObject>)passedObj);
        }

        void PopulateListCommand(List<GameObject> buttons)
        {
            _buttons = buttons;
        }

        void SingleButton(int paramIndex)
        {
            _passedButton.GetComponent<Button>().enabled = paramIndex != 0;
        }

        void MultipleButtons(int paramIndex)
        {
            foreach (GameObject button in _buttons)
                button.GetComponent<Button>().enabled = paramIndex != 0;
        }
    }
}