using MonoServices.Core;
using UnityEngine;
using UnityEngine.UI;

namespace MonoServices.MonoUI
{
    [RequireComponent(typeof(Graphic))]
    public abstract class UiGraphicMonoService : MonoService
    {
        protected Graphic _ThisGraphic;

        protected override void Awake()
        {
            base.Awake();

            _ThisGraphic = GetComponent<Graphic>();
        }

    }
}
