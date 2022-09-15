using UnityEngine;

namespace MonoServices.Core
{
    [System.Serializable]
    public class InvokerCommandParams
    {
        [SerializeField] int _selectedMonoServiceTag;
        [SerializeField] string[] _monoSerciveTagNames;
        [SerializeField] int _previousMonoSeriveTagsLength = 99;

        [Tooltip("If the other service is a child or a parent of this service.")]
        [SerializeField] bool _monoServiceIsAFamRelative;
        [SerializeField] int _parentNumber;

        [Tooltip("Command's name from the other service.")]
        [SerializeField] int _selectedInvokerCommandIndex;
        [SerializeField] string[] _invokerCommandNames;
        [SerializeField, HideInInspector] string _currInvokerCommandName;

        MonoService[] _currSelectedMonoServices;

        [SerializeField] string _currSelectedMonoServiceTag;
        [SerializeField] bool _alreadyCalled;


        public string CurrSelectedMonoServiceTag => _currSelectedMonoServiceTag;

        public int SelectedInvokerCommandIndex => _selectedInvokerCommandIndex;

        public bool MonoServiceIsAFamRelative => _monoServiceIsAFamRelative;

        public string[] InvokerCommandNames => _invokerCommandNames;

        public bool AlreadyCalled { get => _alreadyCalled; set => _alreadyCalled = value; }
        public int ParentNumber { get => _parentNumber; set => _parentNumber = value; }

        public void RefreshMonoServiceTags(MonoService thisMonoService)
        {
            if (_monoServiceIsAFamRelative)
                RefreshGameObjMonoServiceTags(thisMonoService.gameObject);
            else
                RefreshSceneMonoServiceTags();

            RefreshInvokerCommandNames();
        }

        void RefreshSceneMonoServiceTags()
        {

            SelectedMonoServiceTagName();

            _monoSerciveTagNames = SceneMonoServicesFinder.GetMonoServiceTags();


            RestoreMonoServiceTagSelection();

            _currSelectedMonoServices = SceneMonoServicesFinder.FindMonoServices(SelectedMonoServiceTagName());


        }

        void RefreshGameObjMonoServiceTags(GameObject gameObject)
        {
            SelectedMonoServiceTagName();

            _monoSerciveTagNames = GameobjectMonoServiceFinder.GetListenerTags(gameObject, _parentNumber);

            RestoreMonoServiceTagSelection();

            _currSelectedMonoServices = GameobjectMonoServiceFinder.FindMonoServices(
               SelectedMonoServiceTagName(),
               gameObject,
               _parentNumber
               );
        }

        string SelectedMonoServiceTagName()
        {
            for (int i = 0; i < _monoSerciveTagNames.Length; i++)
            {
                var monoServiceTag = _monoSerciveTagNames[i];

                if (_selectedMonoServiceTag == i)
                {
                    _currSelectedMonoServiceTag = monoServiceTag;
                    return monoServiceTag;
                }
            }

            return "";
        }

        void RestoreMonoServiceTagSelection()
        {
            if (_currSelectedMonoServiceTag == "" || (!MonoserviceTagsIsUpdated() && _selectedMonoServiceTag != 99))
                return;

            for (int i = 0; i < _monoSerciveTagNames.Length; i++)
            {
                var monoServiceTag = _monoSerciveTagNames[i];

                if (monoServiceTag == _currSelectedMonoServiceTag)
                {
                    _selectedMonoServiceTag = i;
                    return;
                }

            }

            _selectedMonoServiceTag = 99;
            Debug.LogError($"MonoService Tag: '{_currSelectedMonoServiceTag}' is derefrenced, please re-refrence it or select another tag.");
        }


        bool MonoserviceTagsIsUpdated()
        {
            var isUpdated = _monoSerciveTagNames.Length != _previousMonoSeriveTagsLength;
            _previousMonoSeriveTagsLength = _monoSerciveTagNames.Length;

            return isUpdated;
        }

        void RefreshInvokerCommandNames()
        {

            if (_currSelectedMonoServices.Length == 0)
            {
                _invokerCommandNames = new string[] { };
                return;
            }

            SaveCurrInvokerName();

            _invokerCommandNames = CommandsFinder.MonoServiceCommandNames(_currSelectedMonoServices[0]);

            RestoreCurrInvokerName();
        }


        void SaveCurrInvokerName()
        {
            for (int i = 0; i < _invokerCommandNames.Length; i++)
            {
                var invokerCommandName = _invokerCommandNames[i];

                if (_selectedInvokerCommandIndex == i)
                {
                    _currInvokerCommandName = invokerCommandName;
                    break;
                }
            }
        }


        void RestoreCurrInvokerName()
        {
            for (int i = 0; i < _invokerCommandNames.Length; i++)
            {
                var invokerCommandName = _invokerCommandNames[i];

                if (_currInvokerCommandName == invokerCommandName)
                {
                    _selectedInvokerCommandIndex = i;
                    break;
                }
            }
        }

    }
}
