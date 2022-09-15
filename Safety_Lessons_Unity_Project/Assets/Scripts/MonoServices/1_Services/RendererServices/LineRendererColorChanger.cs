using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Rendering
{
    [RequireComponent(typeof(LineRenderer))]
    public sealed class LineRendererColorChanger : MonoService
    {
        LineRenderer _thisLineRenderer;

        protected override void Awake()
        {
            base.Awake();

            _thisLineRenderer = GetComponent<LineRenderer>();
        }

        [SerializeField] Color colorToChange;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {

            if (methodNumb == 2) GetAndSetColorCommand((Color)passedObj);
        }

        void GetColorCommand(Color colorToChange)
        {
            this.colorToChange = colorToChange;
        }

        void SetColorCommand()
        {
            _thisLineRenderer.startColor = colorToChange;
            _thisLineRenderer.endColor = colorToChange;
        }

        void GetAndSetColorCommand(Color colorToChange)
        {
            GetColorCommand(colorToChange);
            SetColorCommand();
        }

    }
}