using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.States
{
    public class StateSequenceSelector : ParametersHandler
    {
        [SerializeField] StateSequence _stateSequence;
        [SerializeField] bool _setInitialStateOnStart;

        public override bool isInvoker => false;

        int currStateIndex = 99;

        public override List<string> ParameterNames()
        {
            List<string> stateNames = new List<string>();

            foreach (var state in _stateSequence.StateSequences)
                stateNames.Add($"Select State: {state.State}");

            return stateNames;

        }

        protected override void Start()
        {
            base.Start();

            if (_setInitialStateOnStart)
                SelectState(0);
        }

        public void SelectState(int stateIndex)
        {
            currStateIndex = stateIndex;
            InvokeCommand(stateIndex);
            SendInstanceCommand();

        }

        void GetCurrentStateCommand()
        {
            SelectState(currStateIndex);
            InvokeCommand(_stateSequence.StateSequences.Length, currStateIndex);
        }

        void SendInstanceCommand()
        {
            InvokeCommand(_stateSequence.StateSequences.Length + 1, this);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb < _stateSequence.StateSequences.Length) SelectState(methodNumb);
            if (methodNumb == _stateSequence.StateSequences.Length) GetCurrentStateCommand();
        }
    }

}