using UnityEngine;

namespace MonoServices.Core
{
    [System.Serializable]
    public class InvokerCommand
    {
        [SerializeField] InvokerCommandParams _invokerCommand;
        [SerializeField] InvokerAlreadyCalledResetter[] _invokerResetter;

        public InvokerAlreadyCalledResetter[] InvokerResetter => _invokerResetter;
        public InvokerCommandParams Params { get => _invokerCommand; set => _invokerCommand = value; }

        public void RefreshMonoServiceTags(MonoService thisMonoService)
        {
            _invokerCommand.RefreshMonoServiceTags(thisMonoService);

            foreach (var invokerResetter in _invokerResetter)
                invokerResetter.PopulateInvokerParams(thisMonoService);
        }
    }
}