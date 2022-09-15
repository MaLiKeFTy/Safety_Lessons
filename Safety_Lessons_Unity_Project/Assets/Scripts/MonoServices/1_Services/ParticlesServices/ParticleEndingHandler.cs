using MonoServices.Core;
using System.Collections;

namespace MonoServices.Particles
{
    public sealed class ParticleEndingHandler : ParticlesMonoService
    {
        protected override void Awake()
        {
            base.Awake();

            ActivateCoroutine(CheckingParticleEnding());
        }

        IEnumerator CheckingParticleEnding()
        {
            while (_ThisParticleSystem.IsAlive())
                yield return null;

            ParticleEndedCommand();
            yield return null;
        }

        void ParticleEndedCommand() =>
            InvokeCommand(0);

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) { }
    }
}

