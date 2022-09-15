using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.Particles
{
    public sealed class ParticlesMover : ParticlesMonoService
    {
        [SerializeField] float _moveSpeed;
        [SerializeField] Transform _transToMoveTo;

        ParticleSystem.Particle[] _particles;

        bool _particlesAreMoving;

        int _numParticlesAlive;

        protected override void Awake()
        {
            base.Awake();

            _particles = new ParticleSystem.Particle[_ThisParticleSystem.main.maxParticles];
        }

        void GetTransToMoveToCommand(Transform transToMoveTo)
        {
            _transToMoveTo = transToMoveTo;
        }

        void MoveParticlesToTargetCommand()
        {
            _particlesAreMoving = true;
            ActivateCoroutine(MovingParticles());
        }

        void StopParticlesMovementCommand()
        {
            _particlesAreMoving = false;
        }

        void GetAndMoveToTransCommand(Transform transToMoveTo)
        {
            GetTransToMoveToCommand(transToMoveTo);
            MoveParticlesToTargetCommand();
        }

        IEnumerator MovingParticles()
        {
            while (_particlesAreMoving)
            {
                _numParticlesAlive = _ThisParticleSystem.GetParticles(_particles);

                for (int i = 0; i < _numParticlesAlive; i++)
                {
                    _particles[i].position = Vector3.Lerp(
                       _particles[i].position,
                       _transToMoveTo.position,
                       _moveSpeed * Time.deltaTime);
                }

                _ThisParticleSystem.SetParticles(_particles, _numParticlesAlive);
                yield return null;
            }

        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) GetTransToMoveToCommand((Transform)passedObj);
            if (methodNumb == 1) MoveParticlesToTargetCommand();
            if (methodNumb == 2) StopParticlesMovementCommand();
            if (methodNumb == 3) GetAndMoveToTransCommand((Transform)passedObj);
        }
    }
}