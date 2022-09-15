using MonoServices.Core;
using UnityEngine;

namespace MonoServices.GameObjs
{
    public sealed class ObjectInstantiator : GameobjectMonoService
    {
        [SerializeField] GameObject _objPrefab;
        [SerializeField] bool _instantiateOnStart;
        [SerializeField] bool _instantiateOnce;
        [SerializeField] bool _activeOnSpawn;

        bool _instantiated;
        GameObject _instantiatedObj;

        protected override void Start()
        {
            base.Start();

            if (_instantiateOnStart)
                InstantiateObjectCommand();
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
                InstantiateObjectCommand();

        void InstantiateObjectCommand()
        {
            if (_instantiateOnce)
            {
                if (!_instantiated)
                {
                    GetCreatedObjectCommand(_instantiatedObj = Instantiate(_objPrefab));
                    _instantiated = true;
                }
            }
            else
                GetCreatedObjectCommand(_instantiatedObj = Instantiate(_objPrefab));

            _instantiatedObj.SetActive(_activeOnSpawn);
        }

        void GetCreatedObjectCommand(GameObject createdobj) =>
            InvokeCommand(1, createdobj);
    }
}