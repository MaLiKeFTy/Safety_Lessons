using MonoServices.Core;
using UnityEngine;

namespace MonoServices.GameObjs
{
    public sealed class GameObjectToggler : GameobjectMonoService
    {
        [SerializeField] bool _isSelfObj;

        void TurnOnGameobjectCommand()
        {
            _ThisGameobject.SetActive(true);
        }

        void TurnOffGameobjectCommand()
        {
            _ThisGameobject.SetActive(false);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            SetGameOjectToToggle(passedObj);

            if (methodNumb == 0) TurnOnGameobjectCommand();
            if (methodNumb == 1) TurnOffGameobjectCommand();
        }

        void SetGameOjectToToggle(object passedObject)
        {
            var passedObjISGameObject = passedObject is GameObject;

            if (!passedObjISGameObject)
                return;

            _ThisGameobject = _isSelfObj ? (GameObject)passedObject : _ThisGameobject;
        }

    }
}