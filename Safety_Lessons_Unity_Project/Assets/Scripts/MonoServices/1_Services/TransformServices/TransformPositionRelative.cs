using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Transforms
{
    public class TransformPositionRelative : MonoService
    {
        [SerializeField] Transform target;
        [SerializeField] Vector3 _offset;

        void GetInFrontCommand()
        {
            transform.position = target.position;
            transform.position = new Vector3(target.position.x + _offset.x, target.position.y + _offset.y, target.position.z + _offset.z);
            transform.RotateAround(target.position, transform.up, target.eulerAngles.y);

            var lookPos = target.position - transform.position;

            lookPos.y = 0;
            transform.rotation = Quaternion.LookRotation(-lookPos);

        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            GetInFrontCommand();
        }
    }
}
