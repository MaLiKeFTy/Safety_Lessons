using UnityEngine;

namespace MonoServices.Components
{
    public class ColliderToggler : ComponentToggler
    {
        [SerializeField] Collider[] _colliders;

        public override void GetComponentsOfTogglerType(GameObject comp) =>
            _colliders = comp.GetComponentsInChildren<Collider>();

        public override void DisableComponents(GameObject comp)
        {
            foreach (var child in _colliders)
                child.enabled = false;
        }

        public override void EnableComponents(GameObject comp)
        {
            foreach (var child in _colliders)
                child.enabled = true;
        }
    }
}