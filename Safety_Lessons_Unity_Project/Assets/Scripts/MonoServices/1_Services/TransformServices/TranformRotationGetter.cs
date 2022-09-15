using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Transforms
{
    public class TranformRotationGetter : TransformMonoService
    {
        [SerializeField] bool _getRotationOnStart;

        protected override void Start()
        {
            if (_getRotationOnStart)
                GetRotationCommand();
        }

        void GetRotationCommand() =>
            InvokeCommand(0, _ThisTransform.rotation);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
            GetRotationCommand();
    }
}
