using UnityEngine;

namespace MonoServices.Core
{
    [System.Serializable]
    public class MonoServiceParams
    {
        [Tooltip("Bindind other services command with this service.")]
        [SerializeField] MonoServiceCommand[] _monoServiceCommands;
        [SerializeField] string _monoServiceTag;

        [Tooltip("To find commands from other services.")]
        [Space, SerializeField] CommandRefrences _commandRefrences;


        public string MonoServiceTag => _monoServiceTag;
        public MonoServiceCommand[] MonoServiceCommands => _monoServiceCommands;
        public CommandRefrences CommandRefrences { get => _commandRefrences; set => _commandRefrences = value; }
    }
}