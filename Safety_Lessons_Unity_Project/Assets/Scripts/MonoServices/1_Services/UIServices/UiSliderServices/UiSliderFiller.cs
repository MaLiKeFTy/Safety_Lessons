using MonoServices.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MonoServices.MonoUI
{
    [RequireComponent(typeof(Slider))]
    public sealed class UiSliderFiller : UiMonoService
    {
        [SerializeField] float _fillSpeed = 2;

        float _currValue;
        Slider _slider;
        bool _canFillSlider = true;

        protected override void Awake()
        {
            base.Awake();

            _slider = GetComponent<Slider>();

            _currValue = _slider.value;
            ActivateCoroutine(FillingSlider());
        }

        void FillSliderCommand(float value)
        {
            if (!_canFillSlider)
                return;

            _currValue = value;
        }

        IEnumerator FillingSlider()
        {
            var previousValue = _slider.value;

            bool alreadyCalled = false;

            while (true)
            {
                _slider.value = Mathf.MoveTowards(_slider.value, _currValue, _fillSpeed * Time.deltaTime);

                if (previousValue != _slider.value)
                {
                    previousValue = _slider.value;
                    OnSliderMoveCommand();
                    alreadyCalled = false;
                }
                else
                {
                    if (!alreadyCalled)
                    {
                        OnSliderStopCommand();
                        alreadyCalled = true;
                    }
                }


                yield return null;
            }
        }

        void ActivateCanFillSliderCommand() =>
            _canFillSlider = true;

        void OnSliderMoveCommand() =>
            InvokeCommand(2);

        void OnSliderStopCommand() =>
            InvokeCommand(3);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            var filllAmount = passedObj is float ? (float)passedObj : 1;

            if (methodNumb == 0) FillSliderCommand(filllAmount);
            if (methodNumb == 2) ActivateCanFillSliderCommand();
        }
    }
}