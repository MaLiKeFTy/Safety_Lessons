using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Components
{
    public class ClothToggler : ComponentToggler
    {
        [SerializeField] Cloth[] _cloths;

        public override void GetComponentsOfTogglerType(GameObject comp)
        {

            List<Cloth> tempComps = new List<Cloth>();

            foreach (var compToToggle in comp.GetComponentsInChildren<Cloth>())
            {
                if (_JustChildren && compToToggle.transform.gameObject == comp)
                    continue;

                tempComps.Add(compToToggle);
            }

            _cloths = tempComps.ToArray();
        }


        public override void DisableComponents(GameObject comp)
        {
            foreach (var child in _cloths)
                child.enabled = false;
        }

        public override void EnableComponents(GameObject comp)
        {
            foreach (var child in _cloths)
                child.enabled = true;
        }
    }
}
