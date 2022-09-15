using UnityEngine;

namespace MonoServices.States
{
    [CreateAssetMenu(fileName = "StateSequence", menuName = "ScriptableObjects/Sequences/StateSequence")]
    public class StateSequence : ScriptableObject
    {
        [SerializeField] StateSequenceInfoHolder[] _stateSequences;

        internal StateSequenceInfoHolder[] StateSequences => _stateSequences;
    }

    [System.Serializable]
    class StateSequenceInfoHolder
    {
        [SerializeField] string _state;
        [SerializeField] bool _loopFromHere;

        public string State => _state;
        public bool LoopFromHere => _loopFromHere;
    }
}



