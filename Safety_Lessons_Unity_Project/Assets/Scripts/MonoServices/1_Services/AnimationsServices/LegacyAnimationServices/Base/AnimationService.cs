using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Animations
{
    [RequireComponent(typeof(Animation))]
    public abstract class AnimationService : MonoService
    {
        protected Animation _ThisAnimation;

        protected override void Awake()
        {
            base.Awake();

            _ThisAnimation = GetComponent<Animation>();
        }

    }
}
