using UnityEngine;

namespace MonoServices.Components
{
    public class AnimatorToggler : ComponentToggler
    {
        [SerializeField] Animator[] _animators;

        public override void GetComponentsOfTogglerType(GameObject comp) =>
            _animators = comp.GetComponentsInChildren<Animator>();

        public override void DisableComponents(GameObject comp)
        {
            foreach (var child in _animators)
                child.enabled = false;
        }

        public override void EnableComponents(GameObject comp)
        {
            foreach (var child in _animators)
                child.enabled = true;
        }
    }
}
