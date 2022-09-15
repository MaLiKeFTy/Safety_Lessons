using MonoServices.Core;
using UnityEngine;

namespace MonoServices.MonoPhysics
{

    [ExecuteInEditMode]
    public class PhysicsInteractionObject : MonoService
    {
        [SerializeField] string _objectTag;

        public string ObjectTag { get => _objectTag; set => _objectTag = value; }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {

        }

        public void OnInteractedObjCommand()
        {
            InvokeCommand(0);
        }

    }
}
