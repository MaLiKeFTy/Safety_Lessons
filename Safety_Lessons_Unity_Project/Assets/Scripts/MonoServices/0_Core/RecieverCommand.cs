using UnityEngine;

namespace MonoServices.Core
{
    [System.Serializable]
    public class RecieverCommand
    {

        [SerializeField] int _selectedReciverCommandIndex;
        [SerializeField] string[] _recieverCommandsNames;

        public int SelectedReciverCommandIndex => _selectedReciverCommandIndex;

        string _currRecieverCommandName;

        public void RefreshRecieverCommandNames(MonoService monoService)
        {
            SaveCurrRecieverName();
            _recieverCommandsNames = CommandsFinder.MonoServiceCommandNames(monoService);
            RestoreCurrRecieverName();
        }

        void SaveCurrRecieverName()
        {
            for (int i = 0; i < _recieverCommandsNames.Length; i++)
            {
                var reciverCommandName = _recieverCommandsNames[i];

                if (_selectedReciverCommandIndex == i)
                {
                    _currRecieverCommandName = reciverCommandName;
                    break;
                }
            }
        }

        void RestoreCurrRecieverName()
        {
            for (int i = 0; i < _recieverCommandsNames.Length; i++)
            {
                var reciverCommandName = _recieverCommandsNames[i];

                if (_currRecieverCommandName == reciverCommandName)
                {
                    _selectedReciverCommandIndex = i;
                    break;
                }
            }
        }

    }

}