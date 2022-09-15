using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Children
{
    public sealed class GameobjectChildrenToggler : MonoService
    {
        [SerializeField] bool _affectAlreadyDisabledChildren;
        [SerializeField] bool _startDisabled;
        readonly List<Transform> _alreadyDisableChildren = new List<Transform>();

        protected override void Awake()
        {
            base.Awake();

            if (!_affectAlreadyDisabledChildren)
                GetAlreadyDisableChildren();

            if (_startDisabled)
                DisableChildrenCommand();
        }

        void EnableChildrenCommand() =>
            ToggleChildrenComponents(true);

        void DisableChildrenCommand() =>
            ToggleChildrenComponents(false);

        void ToggleChildrenComponents(bool toggle)
        {
            foreach (var childTranform in GetComponentsInChildren<Transform>(true))
                if (childTranform != transform)
                    childTranform.gameObject.SetActive(toggle);

            foreach (var alreadyDisableChild in _alreadyDisableChildren)
                alreadyDisableChild.gameObject.SetActive(false);

        }

        void GetAlreadyDisableChildren()
        {
            foreach (var childTranform in GetComponentsInChildren<Transform>(true))
                if (!childTranform.gameObject.activeSelf)
                    _alreadyDisableChildren.Add(childTranform);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) EnableChildrenCommand();
            if (methodNumb == 1) DisableChildrenCommand();
        }
    }
}