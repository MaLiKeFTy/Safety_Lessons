using MonoServices.Core;
using UnityEngine;

namespace MonoServices.GameObjs
{
    public sealed class GameobjectPasser : GameobjectMonoService
    {
        [SerializeField] bool _selfGameobject;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) PassGameobjectCommand((GameObject)passedObj);
        }

        void PassGameobjectCommand(GameObject objToPass)
        {
            objToPass = _selfGameobject ? _ThisGameobject : objToPass;

            InvokeCommand(0, objToPass);
        }
    }
}

