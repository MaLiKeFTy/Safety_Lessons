using MonoServices.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MonoServices.MonoUI
{

    [RequireComponent(typeof(Slider))]
    public sealed class UISliderHandler : UiMonoService
    {
        Slider _timerSlider;
        float _currSliderValue;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
            SetSliderValueCommand((float)passedObj);

        protected override void Awake()
        {
            base.Awake();

            _timerSlider = GetComponent<Slider>();
        }

        protected override void Start() =>
            StartCoroutine(UpdateTimerSlider());

        void SetSliderValueCommand(float value) =>
            _currSliderValue = value;

        IEnumerator UpdateTimerSlider()
        {
            while (true)
            {
                _timerSlider.value = _currSliderValue;

                yield return null;
            }
        }
    }
}