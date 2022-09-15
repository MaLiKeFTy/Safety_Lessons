using UnityEngine;

namespace MonoServices.Components
{
    [System.Serializable]
    public class ComponentToggler
    {
        [SerializeField] string _togglerType;
        [SerializeField] bool _shouldToggle = false;

        [SerializeField] protected bool _JustChildren;

        public string TogglerType { get => _togglerType; set => _togglerType = value; }
        public bool ShouldToggle => _shouldToggle;

        public virtual void GetComponentsOfTogglerType(GameObject comp) { }

        public virtual void DisableComponents(GameObject comp) { }

        public virtual void EnableComponents(GameObject comp) { }
    }
}