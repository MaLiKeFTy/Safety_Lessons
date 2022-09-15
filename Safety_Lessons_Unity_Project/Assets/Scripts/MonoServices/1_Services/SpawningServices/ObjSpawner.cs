using MonoServices.Core;
using System;
using System.Collections;
using UnityEngine;

namespace MonoServices.Spawnning
{
    public sealed class ObjSpawner : SpawnningMonoService
    {
        [SerializeField] bool _applyScale;
        [SerializeField] bool parentToSpawner;
        [SerializeField] int _maxSpawningCount = int.MaxValue;

        int currSpawnCount;

        public Action OnFinishedSpawnning { get; set; }

        IEnumerator SpawnObjCommand(Transform trans)
        {
            GameObject spawnedObj;

            yield return spawnedObj = Instantiate(_ObjToSpawn, trans.position, trans.rotation);

            if (_applyScale)
                spawnedObj.transform.localScale = trans.localScale;

            InvokeCommand(0, trans);
        }

        IEnumerator SpawnObjAtSpawnerTransformCommand()
        {
            var parent = parentToSpawner ? transform : null;

            GameObject spawnedObj;

            yield return spawnedObj = Instantiate(_ObjToSpawn, transform.position, transform.rotation, parent);

            GetSpawnedTransCommand(spawnedObj.transform);
            currSpawnCount++;

            if (currSpawnCount >= _maxSpawningCount)
                FinishedSpawnningCommand();

            OnFinishedSpawnning?.Invoke();

            InvokeCommand(1, spawnedObj);
        }

        IEnumerator SpawnObjAtPoseCommand(Pose pose)
        {
            GameObject spawnedObj;

            yield return spawnedObj = Instantiate(_ObjToSpawn, pose.position, pose.rotation);

            InvokeCommand(2, spawnedObj);
        }

        void ChangeObjSpawnCommand(GameObject gameObject)
        {
            _ObjToSpawn = gameObject;
        }

        public void GetAndSpawnObjCommand(GameObject gameObject)
        {
            _ObjToSpawn = gameObject;

            StartCoroutine(SpawnObjAtSpawnerTransformCommand());
        }

        void GetGameObjToSpawnCommand()
        {
            InvokeCommand(5, _ObjToSpawn);
        }

        void GetSpawnedTransCommand(Transform spawnedTrans)
        {
            InvokeCommand(6, spawnedTrans);
        }

        void ChangeMaxSpawnValueCommand(int maxSpawnValue)
        {
            _maxSpawningCount = maxSpawnValue;
        }

        void FinishedSpawnningCommand()
        {
            InvokeCommand(8);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) StartCoroutine(SpawnObjCommand((Transform)passedObj));
            if (methodNumb == 1) StartCoroutine(SpawnObjAtSpawnerTransformCommand());
            if (methodNumb == 2) StartCoroutine(SpawnObjAtPoseCommand((Pose)passedObj));
            if (methodNumb == 3) ChangeObjSpawnCommand((GameObject)passedObj);
            if (methodNumb == 4) GetAndSpawnObjCommand((GameObject)passedObj);
            if (methodNumb == 5) GetGameObjToSpawnCommand();
            if (methodNumb == 7) ChangeMaxSpawnValueCommand((int)passedObj);
        }
    }
}
