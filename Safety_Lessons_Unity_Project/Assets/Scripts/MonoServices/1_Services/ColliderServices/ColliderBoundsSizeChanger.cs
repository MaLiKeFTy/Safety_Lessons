using MonoServices.Core;
using MonoServices.MeshBounds;
using System.Collections.Generic;
using UnityEngine;


namespace MonoServices.Colliders
{
    public class ColliderBoundsSizeChanger : MonoService
    {
        [SerializeField] bool _changeOnStart;

        protected override void Start()
        {
            base.Start();

            if (_changeOnStart)
                ChangeColliderSizeCommand();
        }

        void ChangeColliderSizeCommand()
        {
            var childBounds = ChildrenBounds();
            BoxCollider thisBoxCollider = GetComponent<BoxCollider>();

            var distanceDifference = childBounds.center - transform.position;


            thisBoxCollider.center = distanceDifference;
            thisBoxCollider.size = childBounds.size;

            InvokeCommand(0, childBounds);
        }

        void SetColliderSizeWithBoundsCommand(Bounds colliderBounds)
        {
            BoxCollider thisBoxCollider = GetComponent<BoxCollider>();

            var distanceDifference = colliderBounds.center - transform.position;

            thisBoxCollider.center = distanceDifference;
            thisBoxCollider.size = colliderBounds.size;

            InvokeCommand(1, colliderBounds);
        }

        void GetChildrenBoundsCommand()
        {
            InvokeCommand(2, ChildrenBounds());
        }

        Bounds ChildrenBounds()
        {
            List<Renderer> childrenRenderes = new List<Renderer>();

            foreach (var childMeshRenderer in GetComponentsInChildren<MeshRenderer>())
                childrenRenderes.Add(childMeshRenderer);


            foreach (var childSkinnedMeshRenderer in GetComponentsInChildren<SkinnedMeshRenderer>())
                childrenRenderes.Add(childSkinnedMeshRenderer);


            return BoundsHelper.GetFullObjBounds(childrenRenderes.ToArray());
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) ChangeColliderSizeCommand();
            if (methodNumb == 1) SetColliderSizeWithBoundsCommand((Bounds)passedObj);
            if (methodNumb == 2) GetChildrenBoundsCommand();
        }
    }
}

