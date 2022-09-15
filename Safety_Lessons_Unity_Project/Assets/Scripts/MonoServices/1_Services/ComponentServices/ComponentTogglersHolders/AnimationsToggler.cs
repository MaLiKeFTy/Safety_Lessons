using UnityEngine;

namespace MonoServices.Components
{
    public class AnimationsToggler : ComponentToggler
    {
        [SerializeField] Animation[] _animations;

        public override void GetComponentsOfTogglerType(GameObject comp) =>
            _animations = comp.GetComponentsInChildren<Animation>();

        public override void DisableComponents(GameObject comp)
        {
            foreach (var child in _animations)
                child.enabled = false;
        }

        public override void EnableComponents(GameObject comp)
        {
            foreach (var child in _animations)
                child.enabled = true;
        }
    }
}
