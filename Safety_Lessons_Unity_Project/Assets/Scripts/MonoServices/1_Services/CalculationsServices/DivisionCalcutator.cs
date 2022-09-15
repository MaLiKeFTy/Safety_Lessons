using MonoServices.Core;
using UnityEngine;


namespace MonoServices.Maths
{
    public class DivisionCalcutator : MonoService
    {

        [SerializeField] float _divisionValue = 2;

        void DivideWholeCommand(int value)
        {
            float result = value / _divisionValue;

            GetCeiledResultCommand(result);
            GetFlooredResultCommand(result);
        }

        void DivideDecimalCommand(float value)
        {
            float result = value / _divisionValue;

            GetCeiledResultCommand(result);
            GetFlooredResultCommand(result);
        }

        void GetCeiledResultCommand(float result)
        {
            InvokeCommand(2, (int)Mathf.Ceil(result));
        }

        void GetFlooredResultCommand(float result)
        {
            InvokeCommand(3, (int)Mathf.Floor(result));
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) DivideWholeCommand((int)passedObj);
            if (methodNumb == 1) DivideDecimalCommand((float)passedObj);
        }


    }

}

