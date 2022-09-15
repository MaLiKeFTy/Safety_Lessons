using UnityEngine;

namespace MonoServices.Core
{
    [System.Serializable]
    public class InvokerAlreadyCalledResetter
    {
        [SerializeField] InvokerCommandParams _invokerCommand;

        public InvokerCommandParams Params { get => _invokerCommand; set => _invokerCommand = value; }

        public void PopulateInvokerParams(MonoService thisMonoService)
        {
            _invokerCommand.RefreshMonoServiceTags(thisMonoService);
        }
    }
}


