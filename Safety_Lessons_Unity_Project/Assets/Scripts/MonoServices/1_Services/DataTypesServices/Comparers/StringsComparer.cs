using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.DataTypes.Comparers
{
    public class StringsComparer : ParametersHandler
    {

        [SerializeField] string[] _stringsToCompare;

        string _previousComparedString;


        public override bool isInvoker => false;

        public override List<string> ParameterNames()
        {
            List<string> strings = new List<string>();

            foreach (var stringToCompare in _stringsToCompare)
                strings.Add($"On same: {stringToCompare}");

            return strings;
        }

        void CompareTextsCommand(string textToCompare)
        {
            for (int i = 0; i < _stringsToCompare.Length; i++)
            {
                if (_stringsToCompare[i] == textToCompare)
                {
                    if (_previousComparedString == textToCompare)
                        continue;

                    _previousComparedString = textToCompare;
                    InvokeCommand(i);
                    break;
                }
                else
                {
                    _previousComparedString = "";
                }
            }
        }


        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            CompareTextsCommand((string)passedObj);
        }
    }
}
