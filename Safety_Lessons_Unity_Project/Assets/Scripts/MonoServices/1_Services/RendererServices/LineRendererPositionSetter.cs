using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Rendering
{
    [RequireComponent(typeof(LineRenderer))]
    public sealed class LineRendererPositionSetter : MonoService
    {
        [SerializeField] bool _canSetPos = true;

        LineRenderer _thisLineRenderer;

        protected override void Awake()
        {
            base.Awake();

            _thisLineRenderer = GetComponent<LineRenderer>();
        }

        void UpdateStartPositionCommand(Vector3 position)
        {
            var pos = _canSetPos ? position : Vector3.zero;
            _thisLineRenderer.SetPosition(0, pos);
        }

        void UpdateEndPositionCommand(Vector3 position)
        {
            var pos = _canSetPos ? position : Vector3.zero;
            _thisLineRenderer.SetPosition(1, pos);
        }

        void ResetPostionsCommand()
        {
            for (int i = 0; i < _thisLineRenderer.positionCount; i++)
                _thisLineRenderer.SetPosition(i, Vector3.zero);
        }

        void CanSetPosToggleCommand(bool toggle) =>
            _canSetPos = toggle;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) UpdateStartPositionCommand((Vector3)passedObj);
            if (methodNumb == 1) UpdateEndPositionCommand((Vector3)passedObj);
            if (methodNumb == 2) ResetPostionsCommand();
            if (methodNumb == 3) CanSetPosToggleCommand((bool)passedObj);
        }
    }
}