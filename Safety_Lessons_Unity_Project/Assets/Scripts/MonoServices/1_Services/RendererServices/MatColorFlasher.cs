using MonoServices.Core;
using MonoServices.DataTypes.Converter;
using System.Collections;
using UnityEngine;

namespace MonoServices.Rendering
{
    public sealed class MatColorFlasher : RendererMonoService
    {
        [SerializeField] int _iterationsCount = 5;
        [SerializeField] Color _fromColor = Color.white;
        [SerializeField] Color _toColor = Color.red;
        [SerializeField] string _materialColorParamater = "_BaseColor";
        [SerializeField, Range(1, 10)] float _moveSpeed = 8;
        [SerializeField] bool _flashOnStart;

        int _currInterations;

        protected override void Awake()
        {
            base.Awake();

            if (_flashOnStart)
                StartColorFlashingCommand();
        }

        void StartColorFlashingCommand() =>
            ActivateCoroutine(ColorFlashing(_toColor));

        IEnumerator ColorFlashing(Color colorToChange)
        {
            _currInterations++;

            Vector4 colorToChangeQ = DataTypeConverter.FromColorToVec4(colorToChange, true);
            Color currColor = _ThisRenderer.material.GetColor(_materialColorParamater);
            Vector4 currColorQ = DataTypeConverter.FromColorToVec4(currColor, true);

            while (Vector4.Distance(currColorQ, colorToChangeQ) > 0)
            {
                currColor = Vector4.MoveTowards(currColor, colorToChange, _moveSpeed * Time.deltaTime);
                currColorQ = DataTypeConverter.FromColorToVec4(currColor, true);

                _ThisRenderer.material.SetColor(_materialColorParamater, currColor);

                yield return null;
            }

            _ThisRenderer.material.SetColor(_materialColorParamater, currColor);

            if (_currInterations <= _iterationsCount)
            {
                var nextColor = colorToChange == _toColor ? _fromColor : _toColor;
                StartCoroutine(ColorFlashing(nextColor));
            }
            else
            {
                FinishFlashingCommand();
            }

            yield return null;
        }

        void FinishFlashingCommand() =>
            InvokeCommand(1);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) StartColorFlashingCommand();
        }
    }
}