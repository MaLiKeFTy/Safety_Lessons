using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Randomising
{
    public class ObjectRandomizer<T> : MonoService
    {
        [SerializeField] protected List<T> _objs;
        [SerializeField] T randomObj;

        protected override void ReceiveCommands(MonoService monoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) RandomObjCommand();
        }

        protected virtual void RandomObjCommand()
        {
            var randomnum = Random.Range(0, _objs.Count);
            randomObj = _objs[randomnum];

            GetRandomObjCommand();
        }

        protected virtual void GetRandomObjCommand()
        {
            if (randomObj == null) return;
            InvokeCommand(1, randomObj);
        }
    }
}