using UnityEngine;
using MonoServices.Core;

namespace MonoServices.Spawnning
{
    public abstract class SpawnningMonoService : MonoService
    {
        [SerializeField] protected GameObject _ObjToSpawn;
    }
}
