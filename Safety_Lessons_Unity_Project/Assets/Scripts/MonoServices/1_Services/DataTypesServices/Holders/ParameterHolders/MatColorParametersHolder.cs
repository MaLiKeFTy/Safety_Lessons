using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Holders
{
    public abstract class MatColorParametersHolder : ParametersHandler
    {

        [SerializeField]
        protected MatColorParamHolder[] _MatColorParams = { new MatColorParamHolder(
        "White",
        Color.white,
        new string[] { "_BaseColor" }) };

        public override bool isInvoker => false;

        public override List<string> ParameterNames()
        {
            List<string> colorNames = new List<string>();

            foreach (var matcolorParam in _MatColorParams)
                colorNames.Add($"Change mat color to: {matcolorParam.Name}");

            return colorNames;
        }

        void GetColor(int colorIndex)
        {
            GetMatColorParam(_MatColorParams[colorIndex]);

            InvokeCommand(colorIndex, _MatColorParams[colorIndex]);
        }

        protected abstract void GetMatColorParam(MatColorParamHolder matColorParam);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb < _MatColorParams.Length) GetColor(methodNumb);
        }
    }
}