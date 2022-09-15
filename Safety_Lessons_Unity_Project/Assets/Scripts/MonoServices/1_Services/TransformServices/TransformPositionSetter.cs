using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Transforms
{
    public class TransformPositionSetter : MonoService
    {
        [SerializeField] Vector3 _offset;

        void SetPositionCommand(Vector3 posToSet)
        {
            transform.position = posToSet;
            var lookPos = Camera.main.transform.position - transform.position;

            lookPos.y = 0;

            transform.rotation = Quaternion.LookRotation(-lookPos);

            InvokeCommand(0);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            SetPositionCommand((Vector3)passedObj);
        }
    }
}
