using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Holders
{
    public class ColorsHolder : MonoService
    {
        [SerializeField] Color[] _colors = { Color.white };
        [SerializeField] bool _getColorsOnStart;

        protected override void Start()
        {
            base.Start();

            if (_getColorsOnStart)
                GetAllColorsCommand();
        }

        void GetAllColorsCommand()
        {
            GetColorArrayLengthCommand();

            InvokeCommand(0, _colors);
        }

        void GetColorByIndexCommand(int colorIndex)
        {
            InvokeCommand(1, _colors[colorIndex]);
        }

        void GetIndexColorCommand(Color color)
        {
            for (int i = 0; i < _colors.Length; i++)
            {
                if (color == _colors[i])
                {
                    InvokeCommand(2, i);
                    break;
                }
            }
        }

        void GetColorArrayLengthCommand() =>
            InvokeCommand(3, _colors.Length);

        void PopulateColorsCommand(Color[] colors)
        {
            _colors = colors;
            InvokeCommand(4);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) GetAllColorsCommand();
            if (methodNumb == 1) GetColorByIndexCommand((int)passedObj);
            if (methodNumb == 4) PopulateColorsCommand((Color[])passedObj);

        }
    }
}