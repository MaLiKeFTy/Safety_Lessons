using UnityEngine;
using UnityEngine.UI;

namespace MonoServices.Components
{
    public class ImageToggler : ComponentToggler
    {
        [SerializeField] Image[] _imageList;

        public override void GetComponentsOfTogglerType(GameObject comp) =>
            _imageList = comp.GetComponentsInChildren<Image>();

        public override void DisableComponents(GameObject comp)
        {
            foreach (var child in _imageList)
                child.enabled = false;
        }

        public override void EnableComponents(GameObject comp)
        {
            foreach (var child in _imageList)
                child.enabled = true;
        }
    }
}
