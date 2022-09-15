using MonoServices.Core;
using UnityEngine;

namespace MonoServices.MonoPhysics
{
    [RequireComponent(typeof(Rigidbody))]
    public class VelocityApplier : PhysicsMonoService
    {
        [SerializeField] Vector3 _velocity;

        Rigidbody _thisRigidbody;

        Vector3 _currentVelocity;
        Vector3 _initialPos;
        Vector3 _finalPos;

        float _time;

        bool _startVelocityCalculation;
        bool _getInitialVelocity = true;


        protected override void Awake()
        {
            base.Awake();

            _thisRigidbody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            _time = Time.deltaTime;

            if (_startVelocityCalculation)
                VelocityCalculator();
        }

        void VelocityCalculator()
        {
            if (_getInitialVelocity)
                _initialPos = gameObject.transform.position;

            _getInitialVelocity = false;

            _finalPos = gameObject.transform.position;

            _currentVelocity = PhysicsHelper.VelocityVectorCalculator(_initialPos, _finalPos, _time);

            _initialPos = _finalPos;

            UpdateMaxVelocity();
        }

        void UpdateMaxVelocity()
        {
            if (_currentVelocity.magnitude > _velocity.magnitude)
                _velocity = _currentVelocity;
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0)
                ApplyVelocityCommand();
            else if (methodNumb == 1)
                StartVelocityCalculationCommand();
            else if (methodNumb == 2)
                StopVelocityCalculationCommand();
            else if (methodNumb == 3)
                NoVelocityCommand();
            else if (methodNumb == 4)
                GetVelocityCommand((Vector3)passedObj);
        }

        void ApplyVelocityCommand() =>
            _thisRigidbody.velocity = _velocity;

        void StartVelocityCalculationCommand() =>
            _startVelocityCalculation = true;

        void StopVelocityCalculationCommand() =>
            _startVelocityCalculation = false;

        void NoVelocityCommand() =>
            _thisRigidbody.velocity = Vector3.zero;

        void GetVelocityCommand(Vector3 velocity) =>
            _velocity = velocity;


    }
}