using MonoServices.Animations;
using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Randomising
{
    public class RandomAnimFromSequence : ObjectRandomizer<AnimationWithSoundsHolder>
    {
        [SerializeField] AnimationSequence _animsequence;

        protected override void Awake()
        {
            base.Awake();

            SetObjsInList();
        }


        protected override void ReceiveCommands(MonoService monoService, int methodNumb, object passedObj)
        {
            base.ReceiveCommands(monoService, methodNumb, passedObj);

            if (methodNumb == 2) GetSequenceCommand((AnimationSequence)passedObj);
        }

        void GetSequenceCommand(AnimationSequence passedObj)
        {
            _animsequence = passedObj;
            SetObjsInList();
        }

        void SetObjsInList()
        {
            _objs = _animsequence.Objs;
        }
    }
}