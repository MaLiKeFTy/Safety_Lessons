using MonoServices.Core;
using UnityEngine;

using UnityEngine.SceneManagement;

namespace MonoServices.Scenes
{
    public class SceneReloader : MonoService
    {
        [SerializeField] string _sceneNameToLoad;

        static bool _canLoad = true;

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
        }

        protected override void Start()
        {
            if (_canLoad)
                SceneManager.LoadScene(_sceneNameToLoad);
            _canLoad = false;
        }
        
    }
}


