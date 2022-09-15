using MonoServices.Core;
using MonoServices.Vectors;
using System.Collections;
using UnityEngine;

namespace MonoServices.Transforms
{
    public class TransformScaleChanger : VectorThreeParameterHolder
    {
        [SerializeField] float _moveSpeed = 2;
        [SerializeField] AnimationCurve _speedCurve = AnimationCurve.Linear(0, 0, 1, 1);

        [SerializeField] bool _changeStartingScale;
        [SerializeField] Vector3 _startingScale;


        protected override void Start()
        {
            base.Start();

            if (_changeStartingScale)
                ActivateCoroutine(ChangingScale(0));
        }

        void FinishedScaleChanging(int targetVectorIndex) =>
            InvokeCommand(targetVectorIndex + _Vectors.Length);


        protected override void GetVetorPrameter(int vectorParameterIndex) =>
            ActivateCoroutine(ChangingScale(vectorParameterIndex));


        IEnumerator ChangingScale(int targetVectorIndex, Vector3 targetScale = default)
        {
            Vector3 targetVector = targetScale == default ? _Vectors[targetVectorIndex].VectorToSet : targetScale;

            var startScale = transform.localScale;

            float currentLerpTime = 0;
            float targetLerpTime = 1;

            while (currentLerpTime != targetLerpTime)
            {
                currentLerpTime = Mathf.MoveTowards(currentLerpTime, targetLerpTime, _moveSpeed * Time.deltaTime);

                transform.localScale = Vector3.Lerp(startScale, targetVector, _speedCurve.Evaluate(currentLerpTime));

                yield return null;
            }

            FinishedScaleChanging(targetVectorIndex);

            yield return null;
        }


        void ChangeScaleWithVectorCommand(Vector3 passedVector)
        {
            ActivateCoroutine(ChangingScale(99, passedVector));
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            base.ReceiveCommands(invokedMonoService, methodNumb, passedObj);

            if (methodNumb == (_Vectors.Length * 2))
            {
                var vectorToPassed = Vector3.zero;

                if (passedObj is Vector3 vector)
                    vectorToPassed = vector;

                if (passedObj is Transform trans)
                    vectorToPassed = trans.localScale;

                ChangeScaleWithVectorCommand(vectorToPassed);
            }


        }
    }
}