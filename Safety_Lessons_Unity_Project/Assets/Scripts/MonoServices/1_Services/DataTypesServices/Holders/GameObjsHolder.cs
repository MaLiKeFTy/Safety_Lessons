using MonoServices.Core;
using MonoServices.ObjectsSO;
using UnityEngine;

namespace MonoServices.Holders
{
    public class GameObjsHolder : MonoService
    {
        [SerializeField] bool _sendGameObjsOnStart;

        [SerializeField] bool _getLengthOnStart;

        [SerializeField] GameobjsSO _gameobjsSO;

        protected override void Start()
        {
            base.Start();

            if (_sendGameObjsOnStart)
                SendGameObjsHolderCommand();

            if (_getLengthOnStart)
                GetGameObjsArrayLengthCommand();
        }

        void SendGameObjsHolderCommand()
        {
            InvokeCommand(0, _gameobjsSO);
        }

        void GetGameObjWithIndexCommand(int gameObjIndex)
        {
            if (gameObjIndex < _gameobjsSO.Objs.Length)
                InvokeCommand(1, _gameobjsSO.Objs[gameObjIndex]);
        }

        void GetIndexObjCommand(GameObject obj)
        {
            for (int i = 0; i < _gameobjsSO.Objs.Length; i++)
            {
                foreach (var gameObj in _gameobjsSO.Objs[i].GameoObjs)
                {
                    if (gameObj == obj)
                    {
                        InvokeCommand(2, i);
                        break;
                    }
                }
            }
        }

        void GetGameObjsArrayLengthCommand() =>
            InvokeCommand(3, _gameobjsSO.Objs.Length);

        void SetObjsCommand(GameobjsSO objs)
        {
            InvokeCommand(4);
            _gameobjsSO = objs;

            SendGameObjsHolderCommand();
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) SendGameObjsHolderCommand();
            if (methodNumb == 1) GetGameObjWithIndexCommand((int)passedObj);
            if (methodNumb == 2) GetIndexObjCommand((GameObject)passedObj);
            if (methodNumb == 3) GetGameObjsArrayLengthCommand();
            if (methodNumb == 4) SetObjsCommand((GameobjsSO)passedObj);

        }
    }
}