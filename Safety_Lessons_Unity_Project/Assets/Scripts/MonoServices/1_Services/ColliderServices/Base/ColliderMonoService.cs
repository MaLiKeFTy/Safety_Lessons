using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Colliders
{
    [RequireComponent(typeof(Collider))]
    public abstract class ColliderMonoService : MonoService
    {
        protected Collider _ThisCollider;

        protected override void Awake()
        {
            base.Awake();

            _ThisCollider = GetComponent<Collider>();
        }
    }
}

