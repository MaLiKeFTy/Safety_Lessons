using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.MonoUI
{
    public sealed class UiShaker : UiMonoService
    {
        [SerializeField, Range(-10, 0)] float _minDistance = -1;
        [SerializeField, Range(0, 10)] float _maxDistance = 1;
        [SerializeField] float _moveSpeed = 60;
        [SerializeField] bool _shakeOnStart;

        bool _isShaking;


        protected override void Start()
        {
            base.Start();

            if (_shakeOnStart)
                StartShakingCommand();
        }

        void StartShakingCommand()
        {
            if (_isShaking)
                return;

            _isShaking = true;

            ActivateCoroutine(Shaking());
        }

        void StopShakingCommand()
        {
            _isShaking = false;
        }

        IEnumerator Shaking()
        {
            var targetPos = RandomPos();

            while (_isShaking)
            {

                if (Vector2.Distance(_ThisRectTransform.anchoredPosition, targetPos) > 0)
                {
                    _ThisRectTransform.anchoredPosition =
                        Vector2.MoveTowards(_ThisRectTransform.anchoredPosition, targetPos, _moveSpeed * Time.deltaTime);
                }
                else
                {
                    targetPos = _ThisRectTransform.anchoredPosition == Vector2.zero ? RandomPos() : Vector2.zero;
                }

                yield return null;
            }

            while (Vector2.Distance(_ThisRectTransform.anchoredPosition, Vector2.zero) > 0)
            {
                _ThisRectTransform.anchoredPosition =
                        Vector2.MoveTowards(_ThisRectTransform.anchoredPosition, Vector2.zero, _moveSpeed * Time.deltaTime);
                yield return null;
            }

            _ThisRectTransform.anchoredPosition = Vector2.zero;
        }


        Vector2 RandomPos() =>
            new Vector2(Random.Range(_minDistance, _maxDistance), Random.Range(_minDistance, _maxDistance));

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) StartShakingCommand();
            if (methodNumb == 1) StopShakingCommand();
        }


    }
}