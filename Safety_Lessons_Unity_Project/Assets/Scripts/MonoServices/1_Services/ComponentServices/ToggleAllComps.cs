using MonoServices.Core;
using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Components
{
    public class ToggleAllComps : MonoService
    {
        [SerializeField] GameObject _originGameObj;
        [SerializeReference] List<ComponentToggler> _componentTogglerList = new List<ComponentToggler>();

        protected override void Awake()
        {
            base.Awake();

            _originGameObj = gameObject;
        }


        protected override void OnValidate()
        {
            base.OnValidate();

            if (_componentTogglerList.Count != ComponentTogglerFactory.DisableAllCompsCommand().Count)
            {
                _componentTogglerList = ComponentTogglerFactory.DisableAllCompsCommand();

                foreach (var togglers in _componentTogglerList)
                    togglers.GetComponentsOfTogglerType(gameObject);
            }
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) EnablerCommand();
            if (methodNumb == 1) DisablerCommand();
            if (methodNumb == 2) RefreshComponentsCommand();
            if (methodNumb == 3) RefreshAndDisableComponentsCommand();
            if (methodNumb == 4) RefreshAndEnableComponentsCommand();
            if (methodNumb == 5) ChangeOriginGameObjAndEnableCompsCommand((GameObject)passedObj);
        }

        void EnablerCommand()
        {
            InvokeCommand(0);

            foreach (var componentToggler in _componentTogglerList)
            {
                if (componentToggler.ShouldToggle)
                    componentToggler.EnableComponents(_originGameObj);
            }
        }

        void DisablerCommand()
        {
            InvokeCommand(1);
            foreach (var componentToggler in _componentTogglerList)
            {
                if (componentToggler.ShouldToggle)
                    componentToggler.DisableComponents(_originGameObj);
            }
        }

        void RefreshComponentsCommand()
        {
            foreach (var togglers in _componentTogglerList)
                togglers.GetComponentsOfTogglerType(_originGameObj);
        }

        void RefreshAndDisableComponentsCommand()
        {
            RefreshComponentsCommand();
            DisablerCommand();
        }

        void RefreshAndEnableComponentsCommand()
        {
            RefreshComponentsCommand();
            EnablerCommand();
        }

        void ChangeOriginGameObjAndEnableCompsCommand(GameObject gameObj)
        {
            _originGameObj = gameObj;
            RefreshAndDisableComponentsCommand();
            EnablerCommand();
        }
    }
}