using UnityEngine;
using MonoServices.Core;

namespace MonoServices.Particles
{
    public sealed class ParticleSpawner : MonoService
    {
        [SerializeField] ParticleSystem _particlePref;
        [SerializeField] bool _applyToChildren = true;

        ParticleSystem _particleInstance;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0)
                StartParticlesCommand();
            else
                StopParticlesCommand();
        }

        void StartParticlesCommand()
        {
            if (!_particleInstance)
                _particleInstance = Instantiate(_particlePref, transform);

            _particleInstance.transform.SetPositionAndRotation(transform.position, Quaternion.identity);

            _particleInstance.Play(_applyToChildren);
        }

        void StopParticlesCommand()
        {
            if (_particleInstance)
                _particleInstance.Stop(_applyToChildren, ParticleSystemStopBehavior.StopEmitting);
        }

    }
}