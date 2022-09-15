using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;


namespace MonoServices.MeshBounds
{
    public class BoundsGetter : MonoService
    {
        [SerializeField] bool _getBoundsOnStart;
        [SerializeField] int boundsParentLevel;
        [SerializeField] Bounds bounds;
        [SerializeField] bool _debugBounds;

        protected override void Start()
        {
            base.Start();

            if (_getBoundsOnStart)
                SetAndSendBoundsCommand();
        }

        void SetBoundsCommand()
        {
            bounds = BoundsHelper.GetFullObjBounds(Renderers());
            InvokeCommand(0, bounds);
        }

        void SendBoundsCommand()
        {
            SendTopBoundsCommand();
            SendBottomBoundsCommand();
            InvokeCommand(1, bounds);
        }

        void SetAndSendBoundsCommand()
        {
            bounds = BoundsHelper.GetFullObjBounds(Renderers());

            SendTopBoundsCommand();
            SendBottomBoundsCommand();
            InvokeCommand(2, bounds);
        }

        void SendTopBoundsCommand()
        {
            InvokeCommand(3, bounds.size.y);
        }

        void SendBottomBoundsCommand()
        {
            InvokeCommand(4, -bounds.size.y);
        }

        Renderer[] Renderers()
        {
            List<Renderer> renderers = new List<Renderer>();

            var parentLevel = TransformParentFinder.TranformParent(transform, boundsParentLevel);
            var meshRenderers = parentLevel.GetComponentsInChildren<MeshRenderer>();
            var SkinnedMeshRenderer = parentLevel.GetComponentsInChildren<SkinnedMeshRenderer>();

            renderers.AddRange(meshRenderers);
            renderers.AddRange(SkinnedMeshRenderer);

            return renderers.ToArray();
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) SetBoundsCommand();
            if (methodNumb == 1) SendBoundsCommand();
            if (methodNumb == 2) SetAndSendBoundsCommand();
        }

        void OnDrawGizmosSelected()
        {
            if (!_debugBounds)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }

    }

}
