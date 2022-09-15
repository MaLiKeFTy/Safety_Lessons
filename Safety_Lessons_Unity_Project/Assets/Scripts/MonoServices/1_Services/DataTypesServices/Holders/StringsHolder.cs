using MonoServices.Core;
using MonoServices.ObjectsSO;
using UnityEngine;

namespace MonoServices.Holders
{
    public class StringsHolder : MonoService
    {
        [SerializeField] bool _getStringsOnStart;
        [SerializeField] StringsSO _stringsSO;

        protected override void Start()
        {
            base.Start();

            if (_getStringsOnStart)
                GetStringsCommand();
        }

        void GetStringsCommand()
        {
            GetStringArrayLenghCommand();
            InvokeCommand(0, _stringsSO);
        }

        void GetStringByIndexCommand(int stringIndex, MonoService invokedMonoService)
        {
        }

        void GetIndexStringCommand(string stringValue)
        {
            for (int i = 0; i < _stringsSO.Objs.Length; i++)
            {
                if (stringValue == _stringsSO.Objs[i])
                {
                    InvokeCommand(2, i);
                    break;
                }
            }
        }

        void GetStringArrayLenghCommand() =>
            InvokeCommand(3, _stringsSO.Objs.Length);

        void SetStringsCommand(StringsSO stringsSO)
        {
            InvokeCommand(4);
            _stringsSO = stringsSO;

            GetStringsCommand();
        }
        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) GetStringsCommand();
            if (methodNumb == 1) GetStringByIndexCommand((int)passedObj, invokedMonoService);
            if (methodNumb == 2) GetIndexStringCommand((string)passedObj);
            if (methodNumb == 4) SetStringsCommand((StringsSO)passedObj);
        }
    }
}