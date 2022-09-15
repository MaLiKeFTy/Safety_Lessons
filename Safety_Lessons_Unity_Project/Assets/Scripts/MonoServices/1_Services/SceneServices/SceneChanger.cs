using MonoServices.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MonoServices.Scenes
{
    public class SceneChanger : MonoService
    {
        [SerializeField] int _sceneIndex;
        [SerializeField] bool _reloadSameScene;
        [SerializeField] bool _canChangeScene = true;

        void ChangeSceneToggleCommand(bool toggle) =>
            _canChangeScene = toggle;

        void ChangeSceneCommand()
        {
            if (!_canChangeScene)
                return;

            int sceneToLoadIdex = _reloadSameScene ?
                SceneManager.GetActiveScene().buildIndex : _sceneIndex;

            SceneManager.LoadScene(sceneToLoadIdex);
        }

        protected override void ReceiveCommands(MonoService invokedMonoService, int methodNumb, object passedObj)
        {
            if (methodNumb == 0) ChangeSceneToggleCommand((bool)passedObj);
            if (methodNumb == 1) ChangeSceneCommand();
        }

    }

}