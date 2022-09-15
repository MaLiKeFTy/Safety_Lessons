using TMPro;
using UnityEngine;

namespace MonoServices.Components
{
    public class MeshTextToggler : ComponentToggler
    {
        [SerializeField] TextMeshProUGUI[] _textList;

        public override void GetComponentsOfTogglerType(GameObject comp) =>
            _textList = comp.GetComponentsInChildren<TextMeshProUGUI>();

        public override void DisableComponents(GameObject comp)
        {
            foreach (var child in _textList)
                child.enabled = false;
        }

        public override void EnableComponents(GameObject comp)
        {
            foreach (var child in _textList)
                child.enabled = true;
        }
    }
}
