using MonoServices.Core;
using UnityEngine;

namespace MonoServices.GameObjs
{
    public class ParentGameObjectToggler : GameobjectMonoService
    {
        [Space, SerializeField] bool _activeOnStart;

        GameObject _obj;

        protected override void Start()
        {
            base.Start();

            _obj = _ThisGameobject;
            _obj.SetActive(_activeOnStart);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0)
                SetObjectActiveCommand();
            else if (methodNumb == 1)
                SetObjectInActiveCommand();
        }

        void SetObjectActiveCommand() =>
            _obj.SetActive(true);

        void SetObjectInActiveCommand() =>
            _obj.SetActive(false);
    }
}
