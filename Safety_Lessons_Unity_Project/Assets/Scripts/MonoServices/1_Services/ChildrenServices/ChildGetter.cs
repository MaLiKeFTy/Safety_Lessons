using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Children
{
    public class ChildGetter : MonoService
    {
        [SerializeField] int _childNumb;

        void GetChildGameObjCommand()
        {
            InvokeCommand(0, transform.GetChild(_childNumb).gameObject);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            GetChildGameObjCommand();
        }
    }

}