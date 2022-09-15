using UnityEngine;
using MonoServices.Core;

namespace MonoServices.Rendering
{
    [RequireComponent(typeof(Renderer))]
    public abstract class RendererMonoService : MonoService
    {
        protected Renderer _ThisRenderer;

        protected override void Awake()
        {
            base.Awake();

            _ThisRenderer = GetComponent<Renderer>();
        }
    }
}