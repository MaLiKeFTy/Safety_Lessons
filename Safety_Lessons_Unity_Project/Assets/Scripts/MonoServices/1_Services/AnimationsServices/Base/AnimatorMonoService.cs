using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Animations
{
    [RequireComponent(typeof(Animator))]
    public abstract class AnimatorMonoService : MonoService
    {
        protected Animator _ThisAnimator;

        bool _animatorIsDisabled;

        protected override void Awake()
        {
            base.Awake();

            _ThisAnimator = GetComponent<Animator>();
        }

        protected virtual void OnAnimatorEnable() { }

        void Update()
        {

            if (_ThisAnimator.enabled && _animatorIsDisabled)
            {
                OnAnimatorEnable();

                _animatorIsDisabled = false;
            }

            if (!_ThisAnimator.enabled)
            {
                _animatorIsDisabled = true;
            }

        }

    }

}