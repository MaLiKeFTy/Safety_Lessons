using UnityEngine;
using MonoServices.Core;

namespace MonoServices.GameObjs
{
    public abstract class GameobjectMonoService : MonoService
    {
        protected GameObject _ThisGameobject;

        protected override void Awake()
        {
            base.Awake();

            _ThisGameobject = gameObject;
        }
            
    }
}