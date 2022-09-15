using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Particles
{
    public sealed class ParticlesPlayer : ParticlesMonoService
    {
        [SerializeField] bool _playOnStart;

        [ContextMenu("Start Particles")]
        void StartParticlesCommand()
        {
            if (!_ThisParticleSystem.isPlaying)
                _ThisParticleSystem.Play();

            InvokeCommand(0);
        }

        [ContextMenu("Stop Particles")]
        void StopParticlesCommand()
        {
            _ThisParticleSystem.Stop();

            InvokeCommand(1);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) StartParticlesCommand();
            if (methodNumb == 1) StopParticlesCommand();
        }

        protected override void Start()
        {
            base.Start();

            if (_playOnStart)
                StartParticlesCommand();
        }
    }
}
