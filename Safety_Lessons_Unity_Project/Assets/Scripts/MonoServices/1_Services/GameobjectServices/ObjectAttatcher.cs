using MonoServices.Core;
using UnityEngine;

namespace MonoServices.GameObjs
{
    public sealed class ObjectAttatcher : GameobjectMonoService
    {
        [SerializeField] GameObject _parentObj;
        [SerializeField] GameObject _childObj;
        [SerializeField] Vector3 _positionOffset;
        [SerializeField] Vector3 _rotationOffset;
        [SerializeField] bool _attatchOnStart = true;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0)
                SetChildObjCommand((GameObject)passedObj);
            else if (methodNumb == 1)
                SetParentObjCommand((GameObject)passedObj);
            else if (methodNumb == 2)
                DetatchObjectCommand();
            else
                AttatchObjectCommand();
        }

        protected override void Start()
        {
            if (!_childObj)
                return;

            if (_attatchOnStart)
                SetUpObj();
        }

        void SetChildObjCommand(GameObject childobj)
        {
            _childObj = childobj;

            SetUpObj();
        }

        void SetParentObjCommand(GameObject parentobj)
        {
            _parentObj = parentobj;

            SetUpObj();
        }

        void DetatchObjectCommand() =>
            _childObj.transform.parent = null;

        void AttatchObjectCommand() =>
            SetUpObj();

        void SetUpObj()
        {
            if (!_childObj || !_parentObj)
                return;

            _childObj.transform.SetParent(_parentObj.transform);
            _childObj.transform.localPosition = _positionOffset;
            _childObj.transform.localRotation = Quaternion.Euler(_rotationOffset);
        }
    }
}