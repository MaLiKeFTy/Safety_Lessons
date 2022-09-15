using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Holders
{
    public class MonoservicesHolder : MonoService
    {

        [SerializeField] List<MonoService> _monoservices = new List<MonoService>();

        void AddMonoserviceCommand(MonoService monoserviceToAdd)
        {
            if (!_monoservices.Contains(monoserviceToAdd))
                _monoservices.Add(monoserviceToAdd);
        }


        void RemoveMonoserviceCommand(MonoService monoserviceToRemove)
        {
            _monoservices.Remove(monoserviceToRemove);

            OnEmptyMonoservicesCommand();
        }

        void OnEmptyMonoservicesCommand()
        {
            if (_monoservices.Count == 0)
                InvokeCommand(2);

        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) AddMonoserviceCommand(invokedMonoService);
            if (methodNumb == 1) RemoveMonoserviceCommand(invokedMonoService);
        }

    }
}
