using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Holders
{
    public class MaterialHolder : MonoService
    {

        [SerializeField] Material _matToHold;

        void GetMaterialCommand()
        {
            InvokeCommand(0, _matToHold);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            GetMaterialCommand();
        }
    }
}
