using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MonoServices.Core
{
    [System.Serializable]
    public class InvokerHolder : CommandsHolder
    {
        public InvokerHolder(MonoService mainMonoService, string monoServiceTag, string monoServiceName, string monoServiceType, int commandIndex, int invokerIndex) :
            base(mainMonoService, monoServiceTag, monoServiceName, monoServiceType, commandIndex, invokerIndex)
        {
        }
    }
}


