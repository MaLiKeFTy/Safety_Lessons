using MonoServices.Core;
using System.Collections;
using UnityEngine;

namespace MonoServices.Spawnning
{
    public sealed class ChildrenPopulator : SpawnningMonoService
    {
        [SerializeField] int _childrenPopulationCount;
        [SerializeField] bool _clearPreviousChildren = true;
        [SerializeField] bool _spawnOnStart;

        protected override void Start()
        {
            base.Start();

            if (_spawnOnStart)
                PopulateChildrenCommand(_childrenPopulationCount);
        }

        void PopulateChildrenCommand(int populationCount)
        {
            _childrenPopulationCount = populationCount;

            ActivateCoroutine(PopulatingChildren(_childrenPopulationCount));
        }

        void OnChildSpawnCommand(int childIndex)
        {
            InvokeCommand(1, childIndex);
        }

        void OnChildSpawnObjCommand(GameObject spawnedChild)
        {
            InvokeCommand(2, spawnedChild);
        }

        void OnChildSpawnTransCommand(Transform spawnedChildTrans)
        {
            InvokeCommand(3, spawnedChildTrans);
        }

        void OnFinishedPopulatingCommand(int populationCount)
        {
            InvokeCommand(4, populationCount);
        }

        void ChangeAndSpawnObjCommand(GameObject gameObject)
        {
            _ObjToSpawn = gameObject;

            PopulateChildrenCommand(_childrenPopulationCount);

            InvokeCommand(5, gameObject);
        }

        void SendObjtToSpawnCommand()
        {
            InvokeCommand(6, _ObjToSpawn);
        }

        void OnChildSpawnBeforeFinishingPopulatingCommand(GameObject spawnedChild)
        {
            InvokeCommand(7, spawnedChild);
        }

        void OnBeforeSpawinngNextChildCommand()
        {
            InvokeCommand(8);
        }

        void ClearChildren()
        {
            if (!_clearPreviousChildren)
                return;

            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }

        IEnumerator PopulatingChildren(int populationCount)
        {

            ClearChildren();

            for (int i = 0; i < populationCount; i++)
            {

                OnBeforeSpawinngNextChildCommand();
                GameObject spawnedChild;

                yield return spawnedChild = Instantiate(_ObjToSpawn, transform);


                OnChildSpawnTransCommand(spawnedChild.transform);
                OnChildSpawnObjCommand(spawnedChild);
                OnChildSpawnCommand(i);

                if (i != populationCount - 1)
                    OnChildSpawnBeforeFinishingPopulatingCommand(spawnedChild);
            }

            OnFinishedPopulatingCommand(populationCount);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) PopulateChildrenCommand((int)passedObj);
            if (methodNumb == 2) OnChildSpawnObjCommand((GameObject)passedObj);
            if (methodNumb == 4) ChangeAndSpawnObjCommand((GameObject)passedObj);
            if (methodNumb == 5) SendObjtToSpawnCommand();
        }
    }
}

