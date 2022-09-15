using MonoServices.Core;
using System.Collections;
using UnityEngine;

using UnityEngine.UI;

namespace MonoServices.MonoUI
{
    [RequireComponent(typeof(Image))]
    public sealed class UiImageFillHandler : UiMonoService
    {
        [SerializeField] float[] _valueTargets;
        [SerializeField] float _moveSpeed = 6;
        [SerializeField] bool _dontAllowPreviousIndex;

        Image _thisImg;
        int _previousIndex = 99;

        protected override void Awake()
        {
            base.Awake();

            _thisImg = GetComponent<Image>();
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
            FillImageCommand(0);

        void FillImageCommand(int parameterIndex)
        {
            if (parameterIndex == _previousIndex && _dontAllowPreviousIndex)
                return;

            ActivateCoroutine(FillingImage(parameterIndex));

            InvokeCommand(0, null);
        }

        void FinishedFillingCommand(int parameterIndex) =>
            InvokeCommand(1, null);

        IEnumerator FillingImage(int parameterIndex)
        {
            float fillTarget = _valueTargets[parameterIndex];
            _previousIndex = parameterIndex;

            while (Mathf.Abs(_thisImg.fillAmount - fillTarget) > 0)
            {
                _thisImg.fillAmount = Mathf.MoveTowards(_thisImg.fillAmount, fillTarget, _moveSpeed * Time.deltaTime);
                yield return null;
            }

            FinishedFillingCommand(parameterIndex);
            yield return null;
        }
    }
}