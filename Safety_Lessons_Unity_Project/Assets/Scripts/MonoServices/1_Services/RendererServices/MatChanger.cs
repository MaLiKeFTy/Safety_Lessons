using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Rendering
{
    public class MatChanger : ParametersHandler
    {
        [SerializeField] Material[] _matsToChange;
        [SerializeField] bool _changeOnStart;
        [SerializeField] bool _applyToChilren;

        Material[] _originalMats;

        public override bool isInvoker => false;

        public override List<string> ParameterNames()
        {
            List<string> parameterNames = new List<string>();

            foreach (var matToChange in _matsToChange)
                parameterNames.Add($"Change Mat to: {matToChange.name}");

            return parameterNames;
        }

        void ChangeMaterialCommand(Material matChange)
        {

            GetOriginalMats();

            if (TryGetComponent(out Renderer renderer))
                renderer.material = matChange;

            ApplyToChildren(matChange);
            InvokeCommand(0);
        }

        void ChangeMaterialToOriginalCommand()
        {
            var childrenRenderers = GetComponentsInChildren<Renderer>();

            for (int i = 0; i < childrenRenderers.Length; i++)
            {
                var childRenderer = childrenRenderers[i];
                childRenderer.material = _originalMats[i];
            }

            InvokeCommand(1);
        }


        void GetOriginalMats()
        {
            if (_originalMats != null)
                return;

            List<Material> tempMats = new List<Material>();

            foreach (var childRenderer in GetComponentsInChildren<Renderer>())
            {
                tempMats.Add(childRenderer.material);
            }


            _originalMats = tempMats.ToArray();

            InvokeCommand(3);
        }

        void ApplyToChildren(Material matToChange)
        {
            if (!_applyToChilren)
                return;

            foreach (var childRenderer in GetComponentsInChildren<Renderer>())
                childRenderer.material = matToChange;
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb < _matsToChange.Length) ChangeMaterialCommand(_matsToChange[methodNumb]);
            if (methodNumb == _matsToChange.Length) ChangeMaterialCommand((Material)passedObj);
            if (methodNumb == _matsToChange.Length + 1) ChangeMaterialToOriginalCommand();
        }

    }

}