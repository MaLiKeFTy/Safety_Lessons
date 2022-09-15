using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Children
{
    public class GameObjectInOrderChildrenToggler : MonoService
    {
        [SerializeField] Vector3 _originalScale = Vector3.one;

        int _currentChild;

        void ToggleNextChildCommand()
        {

            if (_currentChild > transform.childCount)
                return;

            for (int i = 0; i < transform.childCount; i++)
            {

                Vector3 scaleValue = i <= _currentChild ? _originalScale : Vector3.zero;

                transform.GetChild(i).localScale = scaleValue;

            }

            _currentChild++;

        }

        void ChangeOriginalScaleAndToggleNextChildCommand(Transform trans)
        {
            _originalScale = trans.localScale;

            ToggleNextChildCommand();
        }


        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) ToggleNextChildCommand();
            if (methodNumb == 1) ChangeOriginalScaleAndToggleNextChildCommand((Transform)passedObj);
        }
    }
}
