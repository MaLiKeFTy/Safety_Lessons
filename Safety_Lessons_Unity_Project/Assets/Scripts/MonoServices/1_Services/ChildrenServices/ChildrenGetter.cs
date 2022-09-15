using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Children
{
    public class ChildrenGetter : MonoService
    {
        List<GameObject> _children = new List<GameObject>();

        protected override void Start()
        {
            base.Start();

            RefreshChildListCommand();
        }

        void GetChildrenOfObjectCommand() =>
            InvokeCommand(0, _children);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
            RefreshChildListCommand();

        void RefreshChildListCommand()
        {
            _children.Clear();

            for (int i = 0; i < transform.childCount; i++)
                _children.Add(transform.GetChild(i).gameObject);

            GetChildrenOfObjectCommand();
        }
    }
}