using MonoServices.Core;
using UnityEngine;
using System.Collections.Generic;

namespace MonoServices.Transforms
{
    public class TransformParentSetter : TransformMonoService
    {
        [SerializeField] List<Transform> _transformToSet = new List<Transform>();
        [SerializeField] List<Transform> _parentToSet = new List<Transform>();
        [SerializeField] Vector3 _scaleOffset = Vector3.one;

        void GetParentCommand(Transform parent)
        {
            _parentToSet.Add(parent);
            InvokeCommand(0);
        }


        void GetTransformToSetCommand(Transform trans)
        {
            _transformToSet.Add(trans);
            InvokeCommand(1);
        }

        void SetParentsCommand()
        {
            for (int i = 0; i < _transformToSet.Count; i++)
                for (int j = 0; j < _parentToSet.Count; j++)
                    if (j == i)
                    {
                        _transformToSet[i].parent = _parentToSet[j];
                        _transformToSet[i].SetPositionAndRotation(_parentToSet[j].position, _parentToSet[j].rotation);
                        _transformToSet[i].localScale = Vector3.Scale(Vector3.one, _scaleOffset);
                    }

            InvokeCommand(2);

            OnFinishedSettingUpParentsCommand();
        }

        void OnFinishedSettingUpParentsCommand()
        {
            InvokeCommand(3);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) GetParentCommand((Transform)passedObj);
            if (methodNumb == 1) GetTransformToSetCommand((Transform)passedObj);
            if (methodNumb == 2) SetParentsCommand();
        }
    }
}
