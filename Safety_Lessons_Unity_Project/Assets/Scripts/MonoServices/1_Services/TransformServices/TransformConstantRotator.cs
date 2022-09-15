using MonoServices.Core;
using MonoServices.Vectors;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace MonoServices.Transforms
{
    public class TransformConstantRotator : TransformMonoService
    {
        [SerializeField] bool rotateOnStart;
        [SerializeField, Range(5, 500)] float _rotateSpeed = 5;
        [SerializeField] Space _space = Space.Self;
        [SerializeField] VectorAxisEnum _vector3Axis = VectorAxisEnum.Up;
        [SerializeField] bool _randomAxis;
        [SerializeField] bool _randomSpeed;


        bool _isRotating;

        protected override void Start()
        {
            base.Start();

            if (rotateOnStart)
                StartRotationCommand();
        }

        void StartRotationCommand()
        {
            _isRotating = true;

            ActivateCoroutine(Rotating());
        }

        void StopRotationCommand() =>
            _isRotating = false;

        IEnumerator Rotating()
        {
            if (_randomAxis)
                _vector3Axis = (VectorAxisEnum)UnityEngine.Random.Range(0, (float)Enum.GetValues(typeof(VectorAxisEnum)).Cast<VectorAxisEnum>().Max());

            if (_randomSpeed)
                _rotateSpeed = UnityEngine.Random.Range(5, 25);

            while (_isRotating)
            {
                _ThisTransform.Rotate(VectorAxisProcessor.GetAxisTarget(_vector3Axis) * _rotateSpeed * Time.deltaTime, _space);
                yield return null;
            }
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) StartRotationCommand();
            if (methodNumb == 1) StopRotationCommand();
        }
    }
}
