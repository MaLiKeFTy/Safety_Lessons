using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Transforms
{
    public sealed class TransformsHolder : TransformMonoService
    {
        [SerializeField] List<Transform> _tranforms = new List<Transform>();

        Transform _currTranformToSet;

        void SetTranformListCountCommand(int listCount)
        {
            _tranforms.Clear();

            for (int i = 0; i < listCount; i++)
                _tranforms.Add(_ThisTransform);

            InvokeCommand(0);
        }

        void SetCurrTransformValueCommand(Transform trans)
        {
            _currTranformToSet = trans;
            InvokeCommand(1);
        }

        void AddTransformToListCommand(int indexPlacement)
        {
            if (!_currTranformToSet)
                return;

            _tranforms[indexPlacement] = _currTranformToSet;

            InvokeCommand(2);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) SetTranformListCountCommand((int)passedObj);
            if (methodNumb == 1) SetCurrTransformValueCommand((Transform)passedObj);
            if (methodNumb == 2) AddTransformToListCommand((int)passedObj);
        }
    }
}
