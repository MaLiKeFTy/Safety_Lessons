using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Components
{
    public class MonoServiceToggler : ComponentToggler
    {
        [SerializeField] MonoService[] _monoServices;

        public override void GetComponentsOfTogglerType(GameObject comp)
        {
            List<MonoService> tempComps = new List<MonoService>();

            foreach (var compToToggle in comp.GetComponentsInChildren<MonoService>())
            {
                if (_JustChildren && compToToggle.transform.gameObject == comp)
                    continue;

                if (compToToggle is ToggleAllComps)
                    continue;


                tempComps.Add(compToToggle);
            }

            _monoServices = tempComps.ToArray();
        }

        public override void DisableComponents(GameObject comp)
        {
            foreach (var child in _monoServices)
                child.enabled = false;
        }

        public override void EnableComponents(GameObject comp)
        {
            foreach (var child in _monoServices)
                child.enabled = true;
        }
    }
}
