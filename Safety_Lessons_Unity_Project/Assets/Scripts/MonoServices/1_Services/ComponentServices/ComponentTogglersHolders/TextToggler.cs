using UnityEngine;
using UnityEngine.UI;

namespace MonoServices.Components
{
    public class TextToggler : ComponentToggler
    {
        [SerializeField] Text[] _textList;

        public override void GetComponentsOfTogglerType(GameObject comp) =>
            _textList = comp.GetComponentsInChildren<Text>();

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
