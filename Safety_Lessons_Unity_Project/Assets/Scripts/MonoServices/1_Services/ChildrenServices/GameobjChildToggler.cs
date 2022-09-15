using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Children
{
    public class GameobjChildToggler : MonoService
    {
        [SerializeField] int[] _childIndex = { 0 };

        void TurnOffChildCommand()
        {
            if (transform.childCount != 0)
                foreach (var childIndex in _childIndex)
                    transform.GetChild(childIndex).gameObject.SetActive(false);

            InvokeCommand(0);
        }

        void TurnOnChildCommand()
        {
            if (transform.childCount != 0)
                foreach (var childIndex in _childIndex)
                    transform.GetChild(childIndex).gameObject.SetActive(true);

            InvokeCommand(1);
        }


        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) TurnOffChildCommand();
            if (methodNumb == 1) TurnOnChildCommand();
        }

    }
}