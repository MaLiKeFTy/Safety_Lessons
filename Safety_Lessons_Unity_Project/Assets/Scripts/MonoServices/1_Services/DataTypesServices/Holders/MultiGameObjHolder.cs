using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Holders
{
    public class MultiGameObjHolder : MonoService
    {

        [SerializeField] GameObject[] _gameObjToHold;

        public GameObject[] GameObjToHold { get => _gameObjToHold; set => _gameObjToHold = value; }

        void GetGameObjToHoldCommand(GameObject[] gameObject) =>
            _gameObjToHold = gameObject;


        void SendGameObjCommand()
        {
            InvokeCommand(1, _gameObjToHold);
        }

        void GetAndSendObjCommand(GameObject[] gameObject)
        {
            GetGameObjToHoldCommand(gameObject);
            InvokeCommand(2, gameObject);
        }


        void SendNumbOfGameObjsCommand()
        {
            InvokeCommand(3, _gameObjToHold.Length);
        }


        void SendObjsWithNumbCommand()
        {
            SendGameObjCommand();
            SendNumbOfGameObjsCommand();
        }


        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            GameObject[] objs = { };

            if (passedObj is GameObject passedObjHold)
                objs = new GameObject[] { passedObjHold };

            if (passedObj is GameObject[] passedObjs)
                passedObjs = objs;


            if (methodNumb == 0) GetGameObjToHoldCommand(objs);
            if (methodNumb == 1) Invoke(nameof(SendGameObjCommand), 0.01f);
            if (methodNumb == 2) GetAndSendObjCommand(objs);
            if (methodNumb == 3) SendNumbOfGameObjsCommand();
            if (methodNumb == 4) SendObjsWithNumbCommand();

        }

    }

}
