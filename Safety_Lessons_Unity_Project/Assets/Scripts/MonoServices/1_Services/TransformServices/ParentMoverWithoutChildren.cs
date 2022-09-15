using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Transforms
{
    public class ParentMoverWithoutChildren : MonoService
    {
        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) MoveParentCommand((Transform)passedObj);
            if (methodNumb == 1) SendThisTransCommand();
        }

        void MoveParentCommand(Transform transToMoveTo)
        {
            transToMoveTo =
            transToMoveTo != null ? transToMoveTo : transform;

            List<Transform> children = new List<Transform>();

            for (int i = 0; i < transform.childCount; i++)
                children.Add(transform.GetChild(i));

            foreach (var child in children)
                child.transform.parent = null;

            transform.SetPositionAndRotation(
                transToMoveTo.position,
                transToMoveTo.rotation);

            foreach (var child in children)
                child.transform.parent = transform;

            InvokeCommand(0, transToMoveTo);
            SendThisTransCommand();

        }

        void SendThisTransCommand()
        {
            InvokeCommand(1, transform);
        }
    }

}
