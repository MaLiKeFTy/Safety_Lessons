using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.Rendering
{
    public sealed class ColorChanger : RendererMonoService
    {
        [SerializeField] Gradient[] _colorToChange;

        float _Speed = 1f;
        float _currentKeyPos;
        IEnumerator _currentCoroutine;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
            ChangeColorCommand(0);

        void ChangeColorCommand(int parameterIndex)
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            _currentCoroutine = GradualColourChange(_colorToChange[parameterIndex], parameterIndex);
            _currentKeyPos = 0;
            StartCoroutine(_currentCoroutine);
        }

        IEnumerator GradualColourChange(Gradient gradient, int parameterIndex)
        {
            while (Mathf.Abs(1 - _currentKeyPos) >= 0.01f)
            {
                _currentKeyPos = Mathf.Lerp(_currentKeyPos, 1, _Speed * Time.deltaTime);
                _ThisRenderer.material.color = _colorToChange[parameterIndex].Evaluate(_currentKeyPos);

                yield return null;
            }
        }
    }
}