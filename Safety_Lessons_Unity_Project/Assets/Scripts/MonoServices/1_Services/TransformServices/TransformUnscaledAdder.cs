using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Transforms
{
    public class TransformUnscaledAdder : MonoService
    {

        [SerializeField] Transform _transToMove;


        protected override void OnValidate()
        {
            base.OnValidate();

            if (!_transToMove)
                _transToMove = transform;
        }


        void AddToPositionCommand(Vector3 vectorToAdd)
        {
            _transToMove.position += vectorToAdd;
        }

        void AddToRightPositionCommand(float valueToAdd)
        {
            var posToAdd = Vector3.right * valueToAdd;

            _transToMove.position += posToAdd;
        }

        void AddToUpPositionCommand(float valueToAdd)
        {
            var posToAdd = Vector3.up * valueToAdd;

            _transToMove.position += posToAdd;
        }

        void AddToForwardPositionCommand(float valueToAdd)
        {
            var posToAdd = Vector3.forward * valueToAdd;

            _transToMove.position += posToAdd;
        }

        void ChangeTransToMoveCommand(Transform trans)
        {
            _transToMove = trans;
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) AddToPositionCommand((Vector3)passedObj);
            if (methodNumb == 1) AddToRightPositionCommand((float)passedObj);
            if (methodNumb == 2) AddToUpPositionCommand((float)passedObj);
            if (methodNumb == 3) AddToForwardPositionCommand((float)passedObj);
            if (methodNumb == 4) ChangeTransToMoveCommand((Transform)passedObj);
        }


    }
}