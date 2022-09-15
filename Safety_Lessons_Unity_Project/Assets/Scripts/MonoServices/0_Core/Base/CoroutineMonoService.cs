using System.Collections;
using UnityEngine;

namespace MonoServices.Core
{
    public abstract class CoroutineMonoService : MonoBehaviour
    {
        IEnumerator _coroutine;

        protected virtual void OnEnable()
        {
            ResumeCoroutine();
        }

        protected void ActivateCoroutine(IEnumerator coroutineToActivate)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            StartCoroutine(_coroutine = coroutineToActivate);
        }

        void ResumeCoroutine()
        {
            if (_coroutine != null)
                StartCoroutine(_coroutine);
        }

        protected void DisableCoroutine()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

    }

}
