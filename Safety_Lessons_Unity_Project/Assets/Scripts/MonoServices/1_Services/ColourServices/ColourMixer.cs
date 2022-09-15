using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Colours
{
    public class ColourMixer : MonoService
    {
        [SerializeField] Color[] _colors = { Color.red, Color.blue };

        [Space]

        [SerializeField] Color _outputColor = Color.white;

        protected override void OnValidate()
        {
            base.OnValidate();

            Color sumColors = Color.black;

            for (int i = 0; i < _colors.Length; i++)
            {
                var color = _colors[i];

                if (i == 0)
                    sumColors = color;
                else
                    sumColors += color;
            }

            _outputColor = sumColors / 2;
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
        }
    }

}
