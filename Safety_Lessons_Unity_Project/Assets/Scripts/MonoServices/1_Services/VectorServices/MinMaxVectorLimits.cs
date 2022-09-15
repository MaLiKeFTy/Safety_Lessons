using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Vectors
{
    public class MinMaxVectorLimits : MonoService
    {

        [SerializeField] Vector3 _minVector;
        [SerializeField] Vector3 _maxVector;

        void CheckPosMinMaxCommand(Vector3 pos)
        {
            if (pos.y > _minVector.y) OnMinPosCommand();
            if (pos.y < _maxVector.y) OnMaxPosCommand();
        }

        void OnMinPosCommand()
        {
            Debug.Log("min");
        }

        void OnMaxPosCommand()
        {
            Debug.Log("max");
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(_minVector, 1);
            Gizmos.DrawWireSphere(_maxVector, 1);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            CheckPosMinMaxCommand((Vector3)passedObj);
        }
    }

}
