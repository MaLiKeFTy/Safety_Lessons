using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Vectors
{
    public class VectorAcceleratorMultipliyer : VectorThreeParameterHolder
    {
        [SerializeField] float _multiplicationRate = 0.1f;

        float _originalMultiplicationRate;

        protected override void Awake()
        {
            base.Awake();

            _originalMultiplicationRate = _multiplicationRate;
        }

        void MultiplyVectorCommand(Vector3 vectorToMultiply)
        {
            _multiplicationRate += _originalMultiplicationRate;
            vectorToMultiply *= _multiplicationRate;

            InvokeCommand(_Vectors.Length, vectorToMultiply);
        }

        void ResetMultiplicationRateCommand()
        {
            _multiplicationRate = _originalMultiplicationRate;
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            base.ReceiveCommands(invokedMonoService, methodNumb, passedObj);

            if (methodNumb == _Vectors.Length) ResetMultiplicationRateCommand();
        }

        protected override void GetVetorPrameter(int vectorParameterIndex)
        {
            MultiplyVectorCommand(_Vectors[vectorParameterIndex].VectorToSet);
        }
    }
}