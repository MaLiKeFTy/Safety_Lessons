using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Holders
{
    public class MaterialsParameterHolder : ParametersHandler
    {
        [SerializeField] Material[] _mats;
        public override bool isInvoker => false;

        public override List<string> ParameterNames()
        {
            List<string> matNames = new List<string>();

            foreach (var mat in _mats)
                matNames.Add(mat.name);

            return matNames;
        }


        void GetMaterialCommand(int materialIndex)
        {
            InvokeCommand(_mats.Length, _mats[materialIndex]);
        }


        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            GetMaterialCommand(methodNumb);
        }
    }
}