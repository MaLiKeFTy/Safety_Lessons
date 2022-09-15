using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Particles
{
    public sealed class ParticlesColorChanger : ParticlesMonoService
    {
        [SerializeField] Color _color = Color.white;
        [SerializeField] float _alphaColor = 0.5f;

        void GetParticlesColorCommand(Color color) =>
            _color = color;

        void SetParticlesColorCommand()
        {
            _color.a = _alphaColor;
            ParticleSystem.MainModule mainPs = _ThisParticleSystem.main;
            mainPs.startColor = _color;

            InvokeCommand(1, null);
        }

        void GetAndSetParticlesColorCommand(Color color)
        {
            GetParticlesColorCommand(color);
            SetParticlesColorCommand();
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) GetParticlesColorCommand((Color)passedObj);
            if (methodNumb == 1) SetParticlesColorCommand();
            if (methodNumb == 2) GetAndSetParticlesColorCommand((Color)passedObj);
        }
    }
}
