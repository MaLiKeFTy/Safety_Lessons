using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Vectors
{
    public class Vector3PositionSetter : MonoService
    {
        [SerializeField] Vector3 _positionToSet;

        void GetPositionCommand(Vector3 positionToSet) =>
            _positionToSet = positionToSet;

        void SetPositionCommand() =>
            transform.position = _positionToSet;

        void GetAndSetPositionCommand(Vector3 positionToSet)
        {
            GetPositionCommand(positionToSet);
            SetPositionCommand();
        }


        void AddToPositionCommand(Vector3 positionValueToAdd)
        {
            transform.position += positionValueToAdd;
        }

        void AddToPosYCommand(float yToAdd)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + yToAdd, transform.position.z);
        }

        void SendPositionCommand()
        {
            InvokeCommand(5, transform.position);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) GetPositionCommand((Vector3)passedObj);
            if (methodNumb == 1) SetPositionCommand();
            if (methodNumb == 2) GetAndSetPositionCommand((Vector3)passedObj);
            if (methodNumb == 3) AddToPositionCommand((Vector3)passedObj);
            if (methodNumb == 4) AddToPosYCommand((float)passedObj);
            if (methodNumb == 5) SendPositionCommand();
        }
    }
}