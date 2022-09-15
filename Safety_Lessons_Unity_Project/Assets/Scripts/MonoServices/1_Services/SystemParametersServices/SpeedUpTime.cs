using UnityEngine;

using MonoServices.Core;

namespace MonoServices.SystemParameters
{
    public class SpeedUpTime : MonoService
    {
        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0)
                SpeedUpTimeCommand();
            else if (methodNumb == 1)
                ResetTimeScaleCommand();
        }

        void SpeedUpTimeCommand() =>
                Time.timeScale = 40;

        void ResetTimeScaleCommand() =>
                Time.timeScale = 1;
    }

}