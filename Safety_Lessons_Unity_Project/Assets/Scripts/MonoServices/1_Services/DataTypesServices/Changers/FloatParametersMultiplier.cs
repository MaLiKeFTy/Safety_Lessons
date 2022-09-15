using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.DataTypes.Changers
{
    public class FloatParametersMultiplier : ParametersHandler
    {

        [SerializeField] FloatMultiplierHolder[] _floatMultipliers;


        public override bool isInvoker => false;

        public override List<string> ParameterNames()
        {
            List<string> parameterNames = new List<string>();


            foreach (var floatMultiplier in _floatMultipliers)
                parameterNames.Add(floatMultiplier.Name);

            return parameterNames;
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            var multipliedValue = (float)passedObj * _floatMultipliers[methodNumb].MultiplierValue;

            InvokeCommand(methodNumb, multipliedValue);
        }

    }
}