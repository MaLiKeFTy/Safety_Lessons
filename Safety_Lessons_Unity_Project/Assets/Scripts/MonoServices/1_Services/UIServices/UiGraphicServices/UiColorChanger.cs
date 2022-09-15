using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.MonoUI
{
    public sealed class UiColorChanger : UiGraphicMonoService
    {
        [SerializeField] Color _color = Color.white;
        [SerializeField] float _colorChangeSpeed = 0.5f;
        [SerializeField] bool _keepOriginAlpha;

        float _originColorAlpha;
        Color _initialColor;
        Color _changingColor;

        protected override void Awake()
        {
            base.Awake();

            _originColorAlpha = _ThisGraphic.color.a;

            _initialColor = _ThisGraphic.color;
        }

        void GetColorCommand(Color color) =>
            _color = color;

        void SetColorCommand()
        {
            if (_changingColor == _color)
                return;

            InvokeCommand(1, _color);
            ActivateCoroutine(ChangingImageColor(_color));

            _changingColor = _color;
        }

        void GetAndSetColorCommand(Color color)
        {
            GetColorCommand(color);
            SetColorCommand();
        }

        void SetToInitialColorCommand()
        {
            ActivateCoroutine(ChangingImageColor(_initialColor));
        }

        IEnumerator ChangingImageColor(Color colorTochange)
        {
            float elapsedTime = 0;

            while (elapsedTime < _colorChangeSpeed)
            {
                _ThisGraphic.color = Color.Lerp(_ThisGraphic.color, colorTochange, (elapsedTime / _colorChangeSpeed));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _ThisGraphic.color = colorTochange;
            yield return null;
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            Color colorToChange = passedObj is Color ? (Color)passedObj : Color.white;

            if (_keepOriginAlpha)
                colorToChange.a = _originColorAlpha;

            if (methodNumb == 0) GetColorCommand(colorToChange);
            if (methodNumb == 1) SetColorCommand();
            if (methodNumb == 2) GetAndSetColorCommand(colorToChange);
            if (methodNumb == 3) SetToInitialColorCommand();
        }
    }
}
