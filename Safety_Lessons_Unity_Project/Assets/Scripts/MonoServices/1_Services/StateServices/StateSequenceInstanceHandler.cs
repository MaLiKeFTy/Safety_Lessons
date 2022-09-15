using MonoServices.Core;
using UnityEngine;

namespace MonoServices.States
{
    public class StateSequenceInstanceHandler : MonoService
    {
        [SerializeField] StateSequenceSelector _stateSequence;

        void ChangeStateSequenceInstanceCommand(StateSequenceSelector stateSequence)
        {
            _stateSequence = stateSequence;
        }

        void SelectStateCommand()
        {
            if (_stateSequence)
                _stateSequence.SelectState(0);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) ChangeStateSequenceInstanceCommand((StateSequenceSelector)passedObj);
            if (methodNumb == 1) SelectStateCommand();
        }

    }

}