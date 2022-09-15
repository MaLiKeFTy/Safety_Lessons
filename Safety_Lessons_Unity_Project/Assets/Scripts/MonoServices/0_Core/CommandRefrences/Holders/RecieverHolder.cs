using UnityEngine;


namespace MonoServices.Core
{
    [System.Serializable]
    public class RecieverHolder : CommandsHolder
    {
        [SerializeField] MonoServiceCommand _monoServiceCommand;

        public RecieverHolder(MonoServiceCommand monoServiceCommand, MonoService mainMonoService, string monoServiceTag, string monoServiceName, string monoServiceType, int commandIndex, int invokerIndex) : 
            base(mainMonoService, monoServiceTag, monoServiceName, monoServiceType, commandIndex, invokerIndex)
        {
            _monoServiceCommand = monoServiceCommand;
        }

        public MonoServiceCommand MonoServiceCommand => _monoServiceCommand;
    }
}


