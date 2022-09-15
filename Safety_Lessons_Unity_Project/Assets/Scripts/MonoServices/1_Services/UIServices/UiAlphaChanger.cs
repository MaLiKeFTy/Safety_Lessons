using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.MonoUI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UiAlphaChanger : UiMonoService
    {
        [SerializeField] float[] _alphaValues;
        [SerializeField] float _moveSpeed = 8;
        [SerializeField] bool _activateOnStart;
        [SerializeField] int _startingAlpha = 1;
        [SerializeField] float _deplay;
        CanvasGroup _canvasGroup;

        protected override void Awake()
        {
            base.Awake();

            _canvasGroup = GetComponent<CanvasGroup>();
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
            ChangeAlphaCommand(0);

        protected override void Start()
        {
            base.Start();

            _canvasGroup.alpha = _startingAlpha;

            if (_activateOnStart)
                ChangeAlphaCommand(0);
        }

        void ChangeAlphaCommand(int alphavalueIndex)
        {
            InvokeCommand(0, null);

            ActivateCoroutine(ChangingAlpha(_alphaValues[alphavalueIndex]));
        }

        IEnumerator ChangingAlpha(float alphaValueTarget)
        {

            yield return new WaitForSeconds(_deplay);

            while (Mathf.Abs(_canvasGroup.alpha - alphaValueTarget) > 0.01f)
            {
                _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, alphaValueTarget, _moveSpeed * Time.deltaTime);
                yield return null;
            }

            FinishedChangingAlphaCommand();
            _canvasGroup.alpha = alphaValueTarget;
            _canvasGroup.blocksRaycasts = false;
            yield return null;
        }

        void FinishedChangingAlphaCommand() =>
            InvokeCommand(1, null);
    }
}
