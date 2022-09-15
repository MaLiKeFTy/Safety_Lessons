using MonoServices.Core;
using UnityEngine;


namespace MonoServices.Spawnning
{
    public class ObjSpawnerChildrenHandler : MonoService
    {
        [SerializeField] GameObject[] _objsToSpawn;

        void SetObjsToSpawnCommand(GameObject[] objsToSpawn)
        {
            _objsToSpawn = objsToSpawn;
        }

        void SpawnObjsInChildrenCommand()
        {

            var spawners = GetComponentsInChildren<ObjSpawner>();

            for (int i = 0; i < spawners.Length; i++)
            {
                var spawner = spawners[i];

                if (spawners.Length > i)
                    spawner.GetAndSpawnObjCommand(_objsToSpawn[i]);
            }

            Invoke(nameof(FinishedSpawning), 0.5f);
        }

        void FinishedSpawning()
        {
            InvokeCommand(1);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) SetObjsToSpawnCommand((GameObject[])passedObj);
            if (methodNumb == 1) SpawnObjsInChildrenCommand();
        }

    }

}

