using MonoServices.Core;
using MonoServices.Transforms;
using UnityEngine;


namespace MonoServices.Children
{
    public class ChildTransformGetter : MonoService
    {
        [SerializeField] Transform _child;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            GetChildTransformCommand();
        }

        void GetChildTransformCommand()
        {
            var childrenTransformGetters = GetComponentsInChildren<TransformGetter>();

            if (childrenTransformGetters.Length == 0)
            {
                OnEmptyChildrenCommand();
                return;
            }


            TransformGetter child = childrenTransformGetters[0];

            _child = child.GetTranform;

            InvokeCommand(0, child.GetTranform);
        }

        void OnEmptyChildrenCommand()
        {
            InvokeCommand(1);
        }

    }
}