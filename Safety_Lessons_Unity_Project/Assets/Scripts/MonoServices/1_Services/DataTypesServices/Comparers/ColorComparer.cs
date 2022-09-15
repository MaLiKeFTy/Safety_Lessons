using MonoServices.Core;
using UnityEngine;

namespace MonoServices.DataTypes.Comparers
{
    public class ColorComparer : MonoService
    {
        [SerializeField] bool _invokeAfterValidation;
        [SerializeField] bool _compareAlpha;

        Color _colorToValidate;
        bool _colorIsValid;

        void GetColorToValidateCommand(Color colorToValidate)
        {
            _colorToValidate = colorToValidate;

            InvokeCommand(0);
        }

        void ValidateColorCommand(Color passedColor)
        {
            if (!_compareAlpha)
            {
                _colorToValidate.a = 1;
                passedColor.a = 1;
            }

            _colorIsValid = ColorUtility.ToHtmlStringRGB(_colorToValidate) == ColorUtility.ToHtmlStringRGB(passedColor);

            InvokeCommand(1);

            if (_invokeAfterValidation)
                InvokeEventCommand();
        }

        void ValidateColorHexCommand(string passedColorHex)
        {
            _colorIsValid = ColorUtility.ToHtmlStringRGB(_colorToValidate) == passedColorHex;

            InvokeCommand(2);

            if (_invokeAfterValidation)
                InvokeEventCommand();
        }

        void InvokeEventCommand()
        {
            if (_colorIsValid)
                InvokeCommand(3, _colorToValidate);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) GetColorToValidateCommand((Color)passedObj);
            if (methodNumb == 1) ValidateColorCommand((Color)passedObj);
            if (methodNumb == 2) ValidateColorHexCommand((string)passedObj);
            if (methodNumb == 3) InvokeEventCommand();
        }
    }
}