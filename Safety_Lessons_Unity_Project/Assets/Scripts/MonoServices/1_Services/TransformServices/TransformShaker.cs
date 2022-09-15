using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.Transforms
{
    public class TransformShaker : TransformMonoService
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

            var intialPos = _ThisTransform.localPosition;

            var targetPos = RandomPos(intialPos);


            while (_isShaking)
            {

                if (Vector3.Distance(_ThisTransform.localPosition, targetPos) > 0)
                {
                    _ThisTransform.localPosition =
                        Vector3.MoveTowards(_ThisTransform.localPosition, targetPos, _moveSpeed * Time.deltaTime);
                }
                else
                {
                    targetPos = _ThisTransform.localPosition == intialPos ? RandomPos(intialPos) : intialPos;
                }

                yield return null;
            }

            while (Vector3.Distance(_ThisTransform.localPosition, intialPos) > 0)
            {
                _ThisTransform.localPosition =
                        Vector3.MoveTowards(_ThisTransform.localPosition, intialPos, _moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.localPosition = intialPos;
        }


        Vector3 RandomPos(Vector3 startingPos) =>
            new Vector3(startingPos.x + Random.Range(_minDistance, _maxDistance), startingPos.y + Random.Range(_minDistance, _maxDistance), startingPos.z + Random.Range(_minDistance, _maxDistance));

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) StartShakingCommand();
            if (methodNumb == 1) StopShakingCommand();
        }
    }
}