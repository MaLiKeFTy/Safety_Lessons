using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Transforms
{
    public class TransformPositionGetter : MonoService
    {

        [SerializeField] Vector3 _offset;

        void GetWorldPosCommand()
        {
            InvokeCommand(0, transform.TransformPoint(_offset));
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            GetWorldPosCommand();
        }

    }
}
