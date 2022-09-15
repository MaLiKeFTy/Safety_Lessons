using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Rendering
{
    public sealed class RendererGetter : RendererMonoService
    {
        [Space, SerializeField] bool _getOnStart;
        [SerializeField] string _matColorParameter = "_BaseColor";

        protected override void Start()
        {
            base.Start();

            if (_getOnStart)
                GetRendererCommand();
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) GetRendererCommand();
            if (methodNumb == 1) GetRendererMatCommand();
            if (methodNumb == 4) GetRendererByTanformCommand((Transform)passedObj);
        }

        void GetRendererCommand()
        {
            InvokeCommand(0, _ThisRenderer);
            GetRendererMatCommand();
            GetRendererMatColorCommand();
        }

        void GetRendererMatCommand()
        {
            InvokeCommand(1, _ThisRenderer.material);
        }

        void GetRendererMatColorCommand()
        {
            var thisRendereColor = _ThisRenderer.material.GetColor(_matColorParameter);

            InvokeCommand(2, thisRendereColor);

            GetRendererMatColorHexCommand(thisRendereColor);
        }

        void GetRendererMatColorHexCommand(Color color)
        {
            InvokeCommand(3, ColorUtility.ToHtmlStringRGB(color));
        }

        void GetRendererByTanformCommand(Transform trans)
        {
            _ThisRenderer = trans.GetComponent<Renderer>();
            GetRendererCommand();
        }
    }
}