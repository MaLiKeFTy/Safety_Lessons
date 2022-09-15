using MonoServices.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Rendering
{
    public sealed class MatColorPulser : RendererMonoService
    {
        [SerializeField] List<Color> _colors = new List<Color>();
        [SerializeField] string _materialColorParamater = "_BaseColor";
        [SerializeField, Range(0.1f, 10)] float _moveSpeed = 8;
        [SerializeField] bool _pulseOnStart;

        [Space]

        [SerializeField] bool _addADividerColor;
        [SerializeField] Color _dividerColor = Color.black;

        int _currColorIndex;
        bool _isPulsing;

        protected override void Awake()
        {
            base.Awake();

            if (_pulseOnStart)
                StartPulsingCommand();
        }

        void StartPulsingCommand()
        {
            _isPulsing = true;
            ActivateCoroutine(Pulsing());
        }

        void StopPulsingCommand()
        {
            _isPulsing = false;
        }

        void AddColorCommand(Color color)
        {
            if (_colors.Contains(color))
                return;

            _colors.Add(color);

            if (_addADividerColor)
                _colors.Add(_dividerColor);
        }

        void RemoveColorCommand(Color color)
        {
            if (!_colors.Contains(color))
                return;

            if (_addADividerColor)
                _colors.RemoveAt(_colors.IndexOf(color) + 1);

            _colors.Remove(color);
        }

        IEnumerator Pulsing()
        {

            while (_isPulsing)
            {

                if (_colors.Count >= 1)
                {
                    Color currColor = _ThisRenderer.material.GetColor(_materialColorParamater);

                    if (Vector4.Distance(currColor, _colors[_currColorIndex]) > 0)
                    {
                        currColor = Vector4.MoveTowards(currColor, _colors[_currColorIndex], _moveSpeed * Time.deltaTime);

                        _ThisRenderer.material.SetColor(_materialColorParamater, currColor);

                        yield return null;
                    }
                    else
                    {
                        _ThisRenderer.material.SetColor(_materialColorParamater, currColor);

                        _currColorIndex = _currColorIndex < _colors.Count - 1 ? _currColorIndex + 1 : 0;

                        yield return null;
                    }
                }

                yield return null;
            }

        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) StartPulsingCommand();
            if (methodNumb == 1) StopPulsingCommand();
            if (methodNumb == 2) AddColorCommand((Color)passedObj);
            if (methodNumb == 3) RemoveColorCommand((Color)passedObj);
        }
    }
}