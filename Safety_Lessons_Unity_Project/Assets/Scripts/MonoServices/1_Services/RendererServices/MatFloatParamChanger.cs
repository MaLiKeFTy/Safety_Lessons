using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.Rendering
{
    public sealed class MatFloatParamChanger : RendererMonoService
    {
        [SerializeField] string _sliderParam;
        [SerializeField, Range(0, 1)] int[] _to = { 0, 1 };
        [SerializeField] float _moveSpeed = 0.5f;
        [SerializeField] AnimationCurve _speedCurve;
        [SerializeField] bool _changeParamOnStart;


        protected override void Start()
        {
            base.Start();

            if (_changeParamOnStart)
                ChangeValueCommand(0);
        }

        void ChangeValueCommand(int parameterIndex)
        {
            ActivateCoroutine(MoveSlider(_to[parameterIndex]));
        }

        IEnumerator MoveSlider(int to)
        {
            float from = to == 1 ? 0 : 1;


            while (from != to)
            {
                from = Mathf.MoveTowards(from, to, _moveSpeed * Time.deltaTime);

                _ThisRenderer.material.SetFloat(_sliderParam, _speedCurve.Evaluate(from));

                yield return null;
            }
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            ChangeValueCommand(0);
        }
    }
}