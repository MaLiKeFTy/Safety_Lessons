using MonoServices.Core;
using MonoServices.MeshBounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Spawnning
{
    public sealed class AreaSpawner : SpawnningMonoService
    {
        [SerializeField] Vector3 _areaSize = Vector3.one;
        [SerializeField] float _spawnRate = 1;
        [SerializeField] bool _parentObjs = true;
        [SerializeField] bool _spawnOnStart;
        [SerializeField] int _spawnCount = int.MaxValue;
        [SerializeField] bool _clearChildren;

        Bounds _spawnAreabounds;
        readonly List<GameObject> _previousSpawnedObjs = new List<GameObject>();
        bool _isSpawnning;

        protected override void Start()
        {
            base.Start();
            _spawnAreabounds = new Bounds(transform.position, Vector3.Scale(_areaSize, transform.root.localScale));

            if (_clearChildren)
                foreach (Transform child in transform)
                    Destroy(child.gameObject);


            if (_spawnOnStart)
                StartSpawningCommand();
        }

        void StartSpawningCommand()
        {
            _isSpawnning = true;
            ActivateCoroutine(Spawning());
        }


        IEnumerator Spawning()
        {
            var waitForSeconds = new WaitForSeconds(_spawnRate);

            int currentSpawnCount = 0;

            while (currentSpawnCount < _spawnCount && _isSpawnning)
            {
                if (isActiveAndEnabled)
                {
                    _spawnAreabounds.center = transform.position;

                    var ranPosX = Random.Range(_spawnAreabounds.min.x, _spawnAreabounds.max.x);
                    var ranPosY = Random.Range(_spawnAreabounds.min.y, _spawnAreabounds.max.y);
                    var ranPosZ = Random.Range(_spawnAreabounds.min.z, _spawnAreabounds.max.z);

                    var ranPos = new Vector3(ranPosX, ranPosY, ranPosZ);

                    Bounds objToSpawnBounds = new Bounds(ranPos, BoundsHelper.GetFullObjBounds(_ObjToSpawn.GetComponentsInChildren<MeshRenderer>()).size);

                    bool canSpawn = true;

                    for (int i = 0; i < _previousSpawnedObjs.Count; i++)
                    {

                        var previousSpawnedObj = _previousSpawnedObjs[i];

                        if (!previousSpawnedObj)
                        {
                            _previousSpawnedObjs.RemoveAt(i);
                            continue;
                        }


                        if (objToSpawnBounds.Intersects(BoundsHelper.GetFullObjBounds(previousSpawnedObj.GetComponentsInChildren<MeshRenderer>())))
                        {
                            canSpawn = false;
                            break;
                        }
                    }

                    if (canSpawn)
                    {
                        Transform parent = _parentObjs ? transform : null;
                        GameObject spawnedObj = Instantiate(_ObjToSpawn, ranPos, Quaternion.identity, parent);
                        OnSpawnedCommand(currentSpawnCount);
                        currentSpawnCount++;
                        _previousSpawnedObjs.Add(spawnedObj);
                    }

                    yield return waitForSeconds;
                }

                yield return waitForSeconds;
            }
        }

        void RemoveAndDestroyObjFromListCommand(GameObject objToRemove)
        {
            _previousSpawnedObjs.Remove(objToRemove);
            Destroy(objToRemove);

            InvokeCommand(1);
        }

        void SetSpawnCountCommand(int spawnCount)
        {
            _spawnCount = spawnCount;
            InvokeCommand(2);
        }

        void OnSpawnedCommand(int siblingIndex)
        {
            InvokeCommand(3, siblingIndex);
        }

        void StopSpwanningCommand()
        {
            _isSpawnning = false;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(transform.position, Vector3.Scale(_areaSize, transform.root.localScale));

            for (int i = 0; i < _previousSpawnedObjs.Count; i++)
            {
                var previousSpawnedObj = _previousSpawnedObjs[i];

                if (!previousSpawnedObj)
                {
                    _previousSpawnedObjs.RemoveAt(i);
                    continue;
                }

                Bounds boundsToDraw = BoundsHelper.GetFullObjBounds(previousSpawnedObj.GetComponentsInChildren<MeshRenderer>());
                Gizmos.DrawWireCube(boundsToDraw.center, boundsToDraw.size);
            }
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) StartSpawningCommand();
            if (methodNumb == 1) RemoveAndDestroyObjFromListCommand((GameObject)passedObj);
            if (methodNumb == 2) SetSpawnCountCommand((int)passedObj);
            if (methodNumb == 4) StopSpwanningCommand();
        }
    }
}

