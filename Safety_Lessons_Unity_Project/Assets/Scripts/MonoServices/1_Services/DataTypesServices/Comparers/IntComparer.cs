using MonoServices.Core;
using UnityEngine;

namespace MonoServices.DataTypes.Comparers
{
    public class IntComparer : MonoService
    {
        [SerializeField] int _intToCompare;

        void SetIntToCompareCommand(int intToCompare)
        {
            _intToCompare = intToCompare;

            InvokeCommand(0, _intToCompare);
        }

        void ValidateIntCommand(int passedInt)
        {
            if (passedInt == _intToCompare) OnSameIntCommand();
            if (passedInt > _intToCompare) OnBiggerIntCommand();
            if (passedInt < _intToCompare) OnSmallerIntCommand();

            InvokeCommand(1);
        }

        void OnSameIntCommand()
        {
            InvokeCommand(2);
        }
        void OnBiggerIntCommand()
        {
            InvokeCommand(3);
        }

        void OnSmallerIntCommand()
        {
            InvokeCommand(4);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) SetIntToCompareCommand((int)passedObj);
            if (methodNumb == 1) ValidateIntCommand((int)passedObj);
        }

    }
}
