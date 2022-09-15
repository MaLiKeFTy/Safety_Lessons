using UnityEngine;
using MonoServices.Core;

namespace MonoServices.Particles
{
    [RequireComponent(typeof(ParticleSystem))]
    public abstract class ParticlesMonoService : MonoService
    {
        protected ParticleSystem _ThisParticleSystem;

        protected override void Awake()
        {
            base.Awake();

            _ThisParticleSystem = GetComponent<ParticleSystem>();
        }
    }
}

