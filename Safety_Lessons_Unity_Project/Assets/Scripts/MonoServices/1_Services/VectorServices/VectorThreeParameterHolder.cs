using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Vectors
{
    public abstract class VectorThreeParameterHolder : ParametersHandler
    {
        [Space, SerializeField]
        protected VectorThreeHolder[] _Vectors = {
        new VectorThreeHolder("vector.zero", Vector3.zero),
        new VectorThreeHolder("vector.one", Vector3.one) };

        public override bool isInvoker => false;

        public override List<string> ParameterNames()
        {
            List<string> vectorNames = new List<string>();

            foreach (var vector in _Vectors)
                vectorNames.Add($"Change vector to: {vector.Name}");

            foreach (var vector in _Vectors)
                vectorNames.Add($"On Finished changing to: {vector.Name}");

            return vectorNames;
        }


        void GetVectorThreeCommand(int vectorIndex)
        {
            InvokeCommand(vectorIndex, _Vectors[vectorIndex].VectorToSet);
            GetVetorPrameter(vectorIndex);

        }

        protected abstract void GetVetorPrameter(int vectorParameterIndex);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb < _Vectors.Length) GetVectorThreeCommand(methodNumb);
        }
    }

}