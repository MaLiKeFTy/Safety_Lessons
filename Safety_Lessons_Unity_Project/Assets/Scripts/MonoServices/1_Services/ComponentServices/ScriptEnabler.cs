using MonoServices.Core;
using UnityEngine;

namespace MonoServices.Components
{
    public class ScriptEnabler : MonoService
    {
        [SerializeField] MonoBehaviour _script;

        [SerializeField] MonoService[] _monoServices;
        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0)
                EnableScriptCommand();
            else
                DisableScriptCommand();
        }

        void EnableScriptCommand()
        {
            _script.enabled = true;

            foreach (var monoservice in _monoServices)
                monoservice.enabled = true;
        }

        void DisableScriptCommand()
        {
            _script.enabled = false;

            foreach (var monoservice in _monoServices)
                monoservice.enabled = false;
        }
    }
}