using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Transforms
{
    public class TransformGetter : TransformMonoService
    {
        [SerializeField] bool _getTransOnStart;

        public Transform GetTranform => _ThisTransform;

        protected override void Start()
        {
            base.Start();

            if (_getTransOnStart)
                GetTransformCommand();
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) GetTransformCommand();
            if (methodNumb == 1) SetScaleCommand((Transform)passedObj);
        }

        void GetTransformCommand()
        {
            InvokeCommand(0, _ThisTransform);
        }

        void SetScaleCommand(Transform trans)
        {
            transform.localScale = trans.localScale;
        }

    }
}
