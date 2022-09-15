using MonoServices.Core;
using UnityEngine;
using UnityEngine.UI;

namespace MonoServices.MonoUI
{

    [RequireComponent(typeof(Toggle))]
    public class UiToggleHandler : MonoService
    {
        Toggle switcher;

        protected override void Awake()
        {
            base.Awake();

            if (TryGetComponent(out switcher))
                switcher.onValueChanged.AddListener(ToggleCheck);
        }

        void ToggleCheck(bool check)
        {
            if (check)
                OnCheckCommand();

            else
                OnUncheckCommand();
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            switch (methodNumb)
            {
                case 0:
                    OnUncheckCommand();
                    break;

                case 1:
                    OnCheckCommand();
                    break;
            }
        }

        void OnUncheckCommand()
        {
            if (switcher)
            {
                var check = switcher.isOn;
                if (check)
                    switcher.isOn = false;
            }

            InvokeCommand(0);
        }

        void OnCheckCommand()
        {
            if (switcher)
            {
                var check = switcher.isOn;
                if (!check)
                    switcher.isOn = true;
            }

            InvokeCommand(1);
        }
    }
}
