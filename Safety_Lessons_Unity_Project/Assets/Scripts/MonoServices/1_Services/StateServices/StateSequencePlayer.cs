using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.States
{
    public class StateSequencePlayer : ParametersHandler
    {
        [SerializeField] StateSequence _stateSequence;
        [SerializeField] bool changeStateOnStart = true;
        [SerializeField] int _currentStateIndex;

        public override bool isInvoker => false;

        protected override void Start()
        {
            base.Start();

            if (changeStateOnStart)
                MoveToNextStateCommand();
        }


        void MoveToNextStateCommand()
        {
            if (_currentStateIndex >= _stateSequence.StateSequences.Length)
            {
                ChangeSequenceStartingPointCommand();
                return;
            }

            InvokeCommand(_currentStateIndex);

            _currentStateIndex++;

        }


        void ChangeSequenceStartingPointCommand()
        {
            int startingPoint = 0;

            for (int i = 0; i < _stateSequence.StateSequences.Length; i++)
            {
                var state = _stateSequence.StateSequences[i];
                if (state.LoopFromHere)
                    startingPoint = i;
            }

            _currentStateIndex = startingPoint;

            MoveToNextStateCommand();
        }

        void MoveToSubsequentStateCommand()
        {
            _currentStateIndex++;
            InvokeCommand(_currentStateIndex);
        }


        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == _stateSequence.StateSequences.Length) MoveToNextStateCommand();
            if (methodNumb == _stateSequence.StateSequences.Length + 1) ChangeSequenceStartingPointCommand();
            if (methodNumb == _stateSequence.StateSequences.Length + 2) MoveToSubsequentStateCommand();
        }

        public override List<string> ParameterNames()
        {
            List<string> stateNames = new List<string>();

            foreach (var state in _stateSequence.StateSequences)
                stateNames.Add($"On State: {state.State}");

            return stateNames;
        }


    }

}