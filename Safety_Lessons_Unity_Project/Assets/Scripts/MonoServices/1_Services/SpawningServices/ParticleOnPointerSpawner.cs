using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Spawnning
{
    public class ParticleOnPointerSpawner : MonoService
    {
        [SerializeField] GameObject _particleEffectPref;

        void SpawnParticleCommand()
        {
            GameObject newEffect = ObjPoolerProccesor.GetObject(_particleEffectPref);

            PlaceParticleOnPoniter(newEffect);
        }

        void PlaceParticleOnPoniter(GameObject newEffect)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100.0f))
                newEffect.transform.position = hit.point;
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
            SpawnParticleCommand();
    }
}