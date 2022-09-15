using MonoServices.Vectors;
using System.Collections;
using UnityEngine;

namespace MonoServices.Transforms
{
    public class TransformEulerAnglesChanger : VectorThreeParameterHolder
    {
        [SerializeField] float _moveSpeed = 2;
        [SerializeField] AnimationCurve _speedCurve = AnimationCurve.Linear(0, 0, 1, 1);

        [SerializeField] bool _changeStartingAngle;
        [SerializeField] Vector3 _startingAngle;


        protected override void Start()
        {
            base.Start();

            if (_changeStartingAngle)
                ActivateCoroutine(ChangingScale(0));
        }

        void FinishedScaleChanging(int targetVectorIndex) =>
            InvokeCommand(targetVectorIndex + _Vectors.Length);


        protected override void GetVetorPrameter(int vectorParameterIndex) =>
            ActivateCoroutine(ChangingScale(vectorParameterIndex));


        IEnumerator ChangingScale(int targetVectorIndex)
        {
            var startAngle = transform.localEulerAngles;

            float currentLerpTime = 0;
            float targetLerpTime = 1;

            while (currentLerpTime != targetLerpTime)
            {
                currentLerpTime = Mathf.MoveTowards(currentLerpTime, targetLerpTime, _moveSpeed * Time.deltaTime);

                transform.localEulerAngles = Vector3.Lerp(startAngle, _Vectors[targetVectorIndex].VectorToSet, _speedCurve.Evaluate(currentLerpTime));

                yield return null;
            }

            FinishedScaleChanging(targetVectorIndex);

            yield return null;
        }

    }
}
