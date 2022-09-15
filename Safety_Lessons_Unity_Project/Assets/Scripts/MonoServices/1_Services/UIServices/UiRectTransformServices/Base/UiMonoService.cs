using UnityEngine;
using MonoServices.Core;

namespace MonoServices.MonoUI
{

    [RequireComponent(typeof(RectTransform))]
    public abstract class UiMonoService : MonoService
    {
        protected RectTransform _ThisRectTransform;

        protected override void Awake()
        {
            base.Awake();

            _ThisRectTransform = GetComponent<RectTransform>();
        }
    }
}
