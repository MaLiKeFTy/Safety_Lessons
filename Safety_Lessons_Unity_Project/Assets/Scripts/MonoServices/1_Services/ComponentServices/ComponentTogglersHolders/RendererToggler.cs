using UnityEngine;

namespace MonoServices.Components
{
    public class RendererObjToggler : ComponentToggler
    {
        [SerializeField] Renderer[] _renderers;

        public override void GetComponentsOfTogglerType(GameObject comp) =>
            _renderers = comp.GetComponentsInChildren<Renderer>();

        public override void DisableComponents(GameObject comp)
        {
            foreach (var child in _renderers)
                child.enabled = false;
        }

        public override void EnableComponents(GameObject comp)
        {
            foreach (var child in _renderers)
                child.enabled = true;
        }
    }
}
