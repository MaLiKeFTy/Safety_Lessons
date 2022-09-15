using UnityEngine;

namespace MonoServices.Core
{
    [System.Serializable]
    public class MonoServiceCommand
    {
        [Tooltip("Commands from other services.")]
        [SerializeField] InvokerCommand[] _invokerCommands = { new InvokerCommand() };

        [Tooltip("Commands from this service.")]
        [SerializeField] RecieverCommand _recieverCommand;

        [Tooltip("Turnning all already called commands off.")]
        [SerializeField] bool _resetAllAlreadyCalledCommands;

        public InvokerCommand[] InvokerCommands => _invokerCommands;
        public RecieverCommand RecieverCommand => _recieverCommand;
        public object PassedObj { get; set; }
        public bool ResetAllAlreadyCalledCommands => _resetAllAlreadyCalledCommands;

        public void RefreshCommandNames(MonoService monoService)
        {
            foreach (var invokerCommand in _invokerCommands)
                invokerCommand.RefreshMonoServiceTags(monoService);
            _recieverCommand.RefreshRecieverCommandNames(monoService);
        }
    }
}
