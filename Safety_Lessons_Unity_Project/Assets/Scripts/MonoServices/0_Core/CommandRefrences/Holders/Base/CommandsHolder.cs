using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Core
{
    [System.Serializable]
    public abstract class CommandsHolder
    {

        [SerializeField] MonoService _mainMonoService;
        [SerializeField] string _monoServiceTag;
        [SerializeField] string _monoServiceName;
        [SerializeField] string _monoServiceType;
        [SerializeField] int _commandIndex;
        [SerializeField] int _invokerIndex;

        public CommandsHolder(MonoService mainMonoService, string monoServiceTag, string monoServiceName,  string monoServiceType, int commandIndex, int invokerIndex)
        {
            _mainMonoService = mainMonoService;
            _monoServiceName = monoServiceName;
            _monoServiceTag = monoServiceTag;
            _invokerIndex = invokerIndex;
            _commandIndex = commandIndex;
            _monoServiceType = monoServiceType;
        }

        public MonoService MainMonoService { get => _mainMonoService; set => _mainMonoService = value; }
        public string MonoServiceName => _monoServiceName;
        public string MonoServiceTag => _monoServiceTag;
        public int CommandIndex => _commandIndex;
        public string MonoServiceType => _monoServiceType;
        public int InvokerIndex => _invokerIndex;
    }
}


