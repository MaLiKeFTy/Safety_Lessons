using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MonoServices.Core
{
    [System.Serializable]
    public class ResetterHolder : CommandsHolder
    {
        [SerializeField] InvokerCommandParams _resetter;

        public ResetterHolder(InvokerCommandParams resetter, MonoService mainMonoService, string monoServiceTag, string monoServiceName, string monoServiceType, int commandIndex, int invokerIndex) : 
            base(mainMonoService, monoServiceTag, monoServiceName, monoServiceType, commandIndex, invokerIndex)
        {
            _resetter = resetter;
        }

        public InvokerCommandParams Resetter => _resetter;
    }

}

