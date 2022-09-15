using MonoServices.Core;
using UnityEngine;

namespace MonoServices.GameObjs
{
    public sealed class GameObjectDestroyer : GameobjectMonoService
    {

        [Space, SerializeField] int parentLevelIndexToDestroy = 0;


        protected override void Start()
        {
            base.Start();

            _ThisGameobject = TransformParentFinder.TranformParent(transform, parentLevelIndexToDestroy).gameObject;
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
            DestroyGameObjectCommand();

        void DestroyGameObjectCommand()
        {
            InvokeCommand(0);
            Destroy(_ThisGameobject);
        }
           
    }
}