using UnityEngine;
using MonoServices.Core;

namespace MonoServices.Transforms
{
    public class ClosestAngleFinder : MonoService
    {
        [Range(0, 360)]
        [SerializeField] float[] _anglesToCompare;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj) =>
            GetClosestAngleCommand((float)passedObj);

        void GetClosestAngleCommand(float angleToCompare)
        {
            RevertAnchorAngle(angleToCompare);

            float closestAngleValue = float.MaxValue;
            int angleIndex = int.MaxValue;

            for (int i = 0; i < _anglesToCompare.Length; i++)
            {
                if (Mathf.Abs(_anglesToCompare[i] - angleToCompare) < closestAngleValue)
                {
                    closestAngleValue = Mathf.Abs(_anglesToCompare[i] - angleToCompare);
                    angleIndex = i;
                }
            }

            if (angleIndex != int.MaxValue)
                InvokeCommand(0, _anglesToCompare[angleIndex]);
        }

        void RevertAnchorAngle(float angleToCompare)
        {
            for (int i = 0; i < _anglesToCompare.Length; i++)
            {
                if (_anglesToCompare[i] == 360 || _anglesToCompare[i] == 0)
                {
                    _anglesToCompare[i] = angleToCompare >= 180 ? 360 : 0;
                    return;
                }
            }
        }
    }
}
